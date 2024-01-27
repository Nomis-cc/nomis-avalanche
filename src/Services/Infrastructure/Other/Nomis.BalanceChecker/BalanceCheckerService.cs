// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceCheckerService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;

using Microsoft.Extensions.Logging;
using Nethereum.JsonRpc.WebSocketClient;
using Nethereum.Util;
using Nethereum.Web3;
using Nomis.BalanceChecker.Contracts;
using Nomis.BalanceChecker.Interfaces;
using Nomis.BalanceChecker.Interfaces.Contracts;
using Nomis.BalanceChecker.Interfaces.Enums;
using Nomis.BalanceChecker.Interfaces.Requests;
using Nomis.BalanceChecker.Settings;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Blockchain.Abstractions.Extensions;
using Nomis.Covalent.Interfaces;
using Nomis.DeFi.Interfaces;
using Nomis.DefiLlama.Interfaces;
using Nomis.DefiLlama.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.BalanceChecker
{
    /// <inheritdoc cref="IBalanceCheckerService"/>
    internal sealed class BalanceCheckerService :
        IBalanceCheckerService,
        ISingletonService
    {
        private readonly ILogger<BalanceCheckerService> _logger;
        private readonly BalanceCheckerSettings _settings;
        private readonly Dictionary<BalanceCheckerChain, Web3> _nethereumClients;
        private readonly ICovalentService _covalentService;
        private readonly IDefiLlamaService _defiLlamaService;
        private readonly IDeFiService _deFiService;

        /// <summary>
        /// Initialize <see cref="BalanceCheckerService"/>.
        /// </summary>
        /// <param name="settings"><see cref="BalanceCheckerSettings"/>.</param>
        /// <param name="covalentService"><see cref="ICovalentService"/>.</param>
        /// <param name="defiLlamaService"><see cref="IDefiLlamaService"/>.</param>
        /// <param name="deFiService"><see cref="IDeFiService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public BalanceCheckerService(
            BalanceCheckerSettings settings,
            ICovalentService covalentService,
            IDefiLlamaService defiLlamaService,
            IDeFiService deFiService,
            ILogger<BalanceCheckerService> logger)
        {
            _logger = logger;
            _settings = settings;
            _covalentService = covalentService;
            _defiLlamaService = defiLlamaService;
            _deFiService = deFiService;
            _nethereumClients = new Dictionary<BalanceCheckerChain, Web3>();
            foreach (var chain in Enum.GetValues<BalanceCheckerChain>())
            {
                string url = _settings.DataFeeds?.Find(a => a.Blockchain == chain)?.RpcUrl ?? "http://localhost:8545";
                if (url.StartsWith("wss", StringComparison.OrdinalIgnoreCase))
                {
                    _nethereumClients.Add(chain, new Web3(new WebSocketClient(url))
                    {
                        TransactionManager =
                        {
                            DefaultGasPrice = new(0x4c4b40),
                            DefaultGas = new(0x4c4b40)
                        }
                    });
                }
                else
                {
                    _nethereumClients.Add(chain, new Web3(url)
                    {
                        TransactionManager =
                        {
                            DefaultGasPrice = new(0x4c4b40),
                            DefaultGas = new(0x4c4b40)
                        }
                    });
                }
            }
        }

        /// <inheritdoc />
        public async Task<Result<IEnumerable<BalanceCheckerTokenInfo>>> TokenBalancesAsync(
            TokenBalancesRequest request,
            Func<string, string, Task<TokenDataBalance?>>? tokenBalanceFunc = null)
        {
            if (request.IsEvmAddress && !new AddressUtil().IsValidAddressLength(request.Owner))
            {
                throw new InvalidAddressException(request.Owner!);
            }

            bool tokensCalculated = false;
            var tokenInfos = new List<BalanceCheckerTokenInfo>();
            var messages = new List<string>();

            var stablecoinsPrices = request.StablecoinsPrices;
            string? defillamaId = null;
            request.BlockchainDescriptor.PlatformIds?.TryGetValue(BlockchainPlatform.DefiLLama, out defillamaId);

            #region Covalent API

            if (_settings.EnableCovalentApi && request.UseCovalentApi)
            {
                try
                {
                    var tokensResult = await _covalentService.TokenDataBalancesAsync(request.BlockchainDescriptor, request.Owner!).ConfigureAwait(false);
                    if (tokensResult.Succeeded)
                    {
                        var defillamaIds = new List<string>();
                        foreach (var token in tokensResult.Data ?? new List<TokenDataBalance>())
                        {
                            decimal price = TokenPrice(token.Id, token.Price, stablecoinsPrices);
                            tokenInfos.Add(new BalanceCheckerTokenInfo
                            {
                                Id = token.Id,
                                Balance = token.Balance,
                                Decimals = int.TryParse(token.Decimals, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." }, out int decimals) ? decimals : 18,
                                Symbol = token.Symbol,
                                Name = token.Name,
                                LogoUri = token.LogoUri,
                                Price = price,
                                Source = nameof(BlockchainPlatform.Covalent)
                            });

                            if (price == 0 && token.Confidence > 0 && !string.IsNullOrWhiteSpace(defillamaId) && token.Id?.StartsWith("0x", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                defillamaIds.Add($"{defillamaId}:{token.Id}");
                            }
                        }

                        if (defillamaIds.Any())
                        {
                            var defillamaTokenPrices = await _defiLlamaService.TokensPriceAsync(defillamaIds, 4, stablecoinsPrices).ConfigureAwait(false);
                            foreach (var tokenInfo in tokenInfos)
                            {
                                if (defillamaTokenPrices?.TokensPrices.ContainsKey($"{defillamaId}:{tokenInfo.Id}") == true && tokenInfo.Price == 0)
                                {
                                    var tokenPrice = defillamaTokenPrices.TokensPrices[$"{defillamaId}:{tokenInfo.Id}"];
                                    tokenInfo.Price = tokenPrice.Price;
                                    tokenInfo.LogoUri ??= tokenPrice.LogoUri;
                                    tokenInfo.Name ??= tokenPrice.Symbol;
                                    tokenInfo.Symbol ??= tokenPrice.Symbol;
                                    tokenInfo.Source = nameof(BlockchainPlatform.DefiLLama);
                                }
                            }
                        }

                        /*if (_settings.EnableCovalentPriceApi && request.UseCovalentPriceApi)
                        {
                            var covalentTokensData = await _covalentService
                                .TokenDataPricesAsync(tokenInfos.Where(x => x.Price == 0 && x.Id?.StartsWith("0x", StringComparison.OrdinalIgnoreCase) == true).Select(x => x.Id).Cast<string>().ToList(), debankId).ConfigureAwait(false);
                            foreach (var covalentTokenData in covalentTokensData)
                            {
                                var tokenInfoWithZeroPrice = tokenInfos.Find(x => x.Id?.Equals(covalentTokenData.Id, StringComparison.OrdinalIgnoreCase) == true);
                                if (tokenInfoWithZeroPrice != null)
                                {
                                    tokenInfoWithZeroPrice.Price = covalentTokenData.Price;
                                }
                            }
                        }*/

                        tokensCalculated = true;
                    }
                    else
                    {
                        messages.AddRange(tokensResult.Messages);
                        tokensCalculated = false;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "There is an error when calling Covalent API for {Blockchain} blockchain and {Owner} wallet", request.BlockchainDescriptor.ChainName, request.Owner);
                    messages.Add($"There is an error when calling Covalent API for {request.BlockchainDescriptor.ChainName} blockchain and {request.Owner} wallet");
                    tokensCalculated = false;
                }
            }

            #endregion Covalent API

            if (!tokensCalculated)
            {
                #region RPC (with func)

                var validTokenAddresses = request.TokenAddresses.Where(x => new AddressUtil().IsValidEthereumAddressHexFormat(x)).ToList();
                if (request.UseRpcApi && Enum.TryParse<BalanceCheckerChain>(request.BlockchainDescriptor.ChainId.ToString(), out var balanceCheckerChain) && balanceCheckerChain != BalanceCheckerChain.None)
                {
                    bool useSmartContract = true;
                    var contractsData = _settings.DataFeeds.Find(a => a.Blockchain == balanceCheckerChain);
                    if (contractsData == null || string.IsNullOrWhiteSpace(contractsData.ContractAbi) || string.IsNullOrWhiteSpace(contractsData.ContractAddress) || !new AddressUtil().IsValidAddressLength(contractsData.ContractAddress))
                    {
                        useSmartContract = false;
                    }

                    if (useSmartContract && contractsData != null)
                    {
                        var nethereumClient = _nethereumClients[balanceCheckerChain];
                        var contract = nethereumClient.Eth.GetContract(contractsData.ContractAbi, contractsData.ContractAddress);
                        var function = contract.GetFunction(contractsData.MethodName);

                        int skip = 0;
                        int batchLimit = contractsData.BatchLimit;
                        string[] tokenAddresses = validTokenAddresses.Take(contractsData.BatchLimit).Skip(skip).ToArray();
                        while (tokenAddresses.Any())
                        {
                            try
                            {
                                var result = await function.CallDeserializingToObjectAsync<BalanceCheckerTokensInfo>(request.Owner, tokenAddresses).ConfigureAwait(false);
                                result.TokenInfos.ForEach(x => x.Source = "Smart-contract");
                                tokenInfos.AddRange(result.TokenInfos);
                                skip += batchLimit;
                                tokenAddresses = validTokenAddresses.Take(batchLimit + skip).Skip(skip).ToArray();
                            }
                            catch
                            {
                                // _logger.LogWarning(ex, "There is an error when calling smart-contract for {Blockchain}", request.Blockchain);
                                foreach (string tokenAddress in tokenAddresses)
                                {
                                    if (tokenBalanceFunc != null)
                                    {
                                        var tokenBalanceData = await tokenBalanceFunc(request.Owner!, tokenAddress).ConfigureAwait(false);
                                        if (tokenBalanceData?.Balance > 0)
                                        {
                                            tokenInfos.Add(new BalanceCheckerTokenInfo
                                            {
                                                Id = tokenAddress,
                                                Balance = tokenBalanceData.Balance,
                                                Decimals = int.TryParse(tokenBalanceData.Decimals, out int decimals) ? decimals : 18,
                                                Symbol = tokenBalanceData.Symbol,
                                                Name = tokenBalanceData.Name,
                                                Source = "Explorer"
                                            });
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var result = await function.CallDeserializingToObjectAsync<BalanceCheckerTokensInfo>(request.Owner, new List<string> { tokenAddress }.ToArray()).ConfigureAwait(false);
                                            tokenInfos.AddRange(result.TokenInfos.Where(x => x.Balance > 0));
                                        }
                                        catch (Exception e)
                                        {
                                            _logger.LogWarning(e, "There is an error when calling smart-contract for {Blockchain} with address {Address} and wallet {Wallet}. Calculated {TokensCount}", request.BlockchainDescriptor.ChainName, tokenAddress, request.Owner, tokenInfos.Count);
                                        }
                                    }
                                }

                                skip += batchLimit;
                                tokenAddresses = validTokenAddresses.Take(batchLimit + skip).Skip(skip).ToArray();
                            }
                        }
                    }
                }
                else
                {
                    foreach (string tokenAddress in validTokenAddresses)
                    {
                        if (tokenBalanceFunc != null)
                        {
                            var tokenBalanceData =
                                await tokenBalanceFunc(request.Owner!, tokenAddress).ConfigureAwait(false);
                            if (tokenBalanceData?.Balance > 0)
                            {
                                tokenInfos.Add(new BalanceCheckerTokenInfo
                                {
                                    Id = tokenAddress,
                                    Balance = tokenBalanceData.Balance,
                                    Decimals = int.TryParse(tokenBalanceData.Decimals, out int decimals) ? decimals : 18,
                                    Symbol = tokenBalanceData.Symbol,
                                    Name = tokenBalanceData.Name,
                                    Source = "Explorer"
                                });
                            }
                        }
                    }
                }

                #endregion RPC  (with func)
            }

            messages.Add("Got token balances by given wallet address and blockchain.");

            return await Result<IEnumerable<BalanceCheckerTokenInfo>>.SuccessAsync(tokenInfos, messages).ConfigureAwait(false);
        }

        private static decimal TokenPrice(
            string? tokenAddress,
            decimal price,
            IEnumerable<TokenPriceData> stablecoinsPrices)
        {
            if (price > 0)
            {
                return price;
            }

            return stablecoinsPrices.FirstOrDefault(p => p.Id?.Equals(tokenAddress, StringComparison.OrdinalIgnoreCase) == true)?.Price ?? 0;
        }
    }
}