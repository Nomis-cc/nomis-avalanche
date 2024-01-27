// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text;
using System.Text.Json;

using Ipfs.CoreApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Blockchain.Abstractions.Clients;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Contracts.Models;
using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Blockchain.Abstractions.Requests;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.CacheProviderService.Interfaces;
using Nomis.DefiLlama.Interfaces;
using Nomis.IPFS.Interfaces;
using Nomis.IPFS.Interfaces.Requests;
using Nomis.ScoringService.Interfaces;
using Nomis.SoulboundTokenService.Interfaces.Contracts;
using Nomis.SoulboundTokenService.Interfaces.Requests;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Proxy;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Enums;
using Nomis.Utils.Extensions;
using Nomis.Utils.Wrapper;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get the token data balance.
        /// </summary>
        /// <param name="client"><see cref="IBaseEvmClient"/>.</param>
        /// <param name="owner">Owner wallet address.</param>
        /// <param name="tokenAddress">The token smart-contract address.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <returns>Returns the token data balance or null.</returns>
        public static async Task<TokenDataBalance?> GetTokenDataBalanceAsync(
            this IBaseEvmClient client,
            string owner,
            string tokenAddress,
            ulong chainId)
        {
            var tokenBalance = (await client.GetTokenBalanceAsync(owner, tokenAddress).ConfigureAwait(false)).Balance?.ToBigInteger();
            if (tokenBalance > 0)
            {
                return new TokenDataBalance
                {
                    ChainId = chainId,
                    Balance = (BigInteger)tokenBalance,
                    Id = tokenAddress,
                    Source = "Explorer"
                };
            }

            return null;
        }

        /// <summary>
        /// Get HttpClient proxy.
        /// </summary>
        /// <param name="proxyPool">Pool of proxies.</param>
        /// <param name="provider"><see cref="IServiceProvider"/>.</param>
        /// <returns>Returns HttpClient proxy.</returns>
        public static HttpClientProxy GetHttpClientProxy(
            this IValuePool<HttpClientProxy> proxyPool,
            IServiceProvider provider)
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            HttpClientProxy? httpClientProxy;
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Request.Headers.TryGetValue(nameof(WalletStatsRequest.DisableProxy), out var disableProxy))
            {
                if (disableProxy.FirstOrDefault()?.Equals(false.ToString(), StringComparison.OrdinalIgnoreCase) == true)
                {
                    httpClientProxy = proxyPool.GetNextValue();
                }
                else
                {
                    do
                    {
                        httpClientProxy = proxyPool.GetNextValue();
                    }
                    while (httpClientProxy.ProxyUri != null);
                }
            }
            else if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(nameof(WalletStatsRequest.DisableProxy), out _) == false)
            {
                do
                {
                    httpClientProxy = proxyPool.GetNextValue();
                }
                while (httpClientProxy.ProxyUri != null);
            }
            else
            {
                httpClientProxy = proxyPool.GetNextValue();
            }

            return httpClientProxy;
        }

        /// <summary>
        /// Get the ONFT token metadata IPFS URL.
        /// </summary>
        /// <param name="soulboundTokenService"><see cref="IScoreSoulboundTokenService"/>.</param>
        /// <param name="ipfsService"><see cref="IIPFSService"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="prepareToMint">Is prepare to mint.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="chainName">Blockchain name.</param>
        /// <param name="onftImageIpfsUrl">The ONFT image IPFS URL.</param>
        /// <param name="additionalTraits">Additional traits.</param>
        /// <param name="name">ONFT name.</param>
        /// <param name="description">ONFT description.</param>
        /// <param name="externalUrl">ONFT external URL.</param>
        /// <returns>Returns the token metadata IPFS URL.</returns>
        public static async Task<Result<string?>> ONFTTokenMetadataAsync(
            this IScoreSoulboundTokenService soulboundTokenService,
            IIPFSService ipfsService,
            ICacheProviderService cacheProviderService,
            bool prepareToMint,
            ulong chainId,
            string? chainName,
            string onftImageIpfsUrl,
            IList<NFTTrait>? additionalTraits = default,
            string? name = null,
            string? description = null,
            string? externalUrl = null)
        {
            if (!prepareToMint)
            {
                return await Result<string?>.FailAsync($"Can't get ONFT token metadata: {nameof(prepareToMint)} parameter is false.").ConfigureAwait(false);
            }

            try
            {
                var metadataResult = await soulboundTokenService.GetONFTSoulboundTokenMetadataAsync(new NFTMetadataRequest
                {
                    Image = onftImageIpfsUrl,
                    Name = name,
                    Description = description,
                    ExternalUrl = externalUrl,
                    Attributes = new List<NFTTrait>
                    {
                        new()
                        {
                            TraitType = "Initial Blockchain",
                            Value = chainName
                        },
                        new()
                        {
                            DisplayType = "number",
                            TraitType = "Initial Chain id",
                            Value = chainId
                        },
                        new()
                        {
                            DisplayType = "date",
                            TraitType = "Timestamp",
                            Value = DateTime.UtcNow.ConvertToTimestamp()
                        }
                    }.Union(additionalTraits ?? new List<NFTTrait>()).ToList()
                }).ConfigureAwait(false);

                if (metadataResult.Succeeded)
                {
                    string? tokenMetadata = await cacheProviderService.GetStringFromCacheAsync($"onft_token_metadata_{chainId}_{onftImageIpfsUrl}").ConfigureAwait(false);
                    if (tokenMetadata == null)
                    {
                        using var tokenMetadataStream = new MemoryStream(JsonSerializer.Serialize(metadataResult.Data).ToByteArray(Encoding.UTF8));
                        var uploadMetadataResult = await ipfsService.UploadFileAsync(new IPFSUploadFileRequest
                        {
                            FileContent = tokenMetadataStream,
                            FileName = $"{chainId}_Nomis_ONFT.json",
                            Options = new AddFileOptions
                            {
                                Pin = true
                            }
                        }).ConfigureAwait(false);

                        if (uploadMetadataResult.Succeeded)
                        {
                            tokenMetadata = uploadMetadataResult.Data;
                            await cacheProviderService.SetCacheAsync($"onft_token_metadata_{chainId}_{onftImageIpfsUrl}", tokenMetadata!, new DistributedCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow = new TimeSpan(1, 0, 0, 0)
                            }).ConfigureAwait(false);
                        }
                    }

                    if (tokenMetadata != null)
                    {
                        return await Result<string?>.SuccessAsync(string.Format(ipfsService.Settings.IpfsGatewayUrlTemplate!, tokenMetadata), "Successfully got ONFT token metadata URL.").ConfigureAwait(false);
                    }
                }
            }
            catch (Exception e)
            {
                return await Result<string?>.FailAsync(e.Message).ConfigureAwait(false);
            }

            return await Result<string?>.FailAsync("Cant get ONFT token metadata.").ConfigureAwait(false);
        }

        /// <summary>
        /// Get the token metadata IPFS URL.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="soulboundTokenService"><see cref="IScoreSoulboundTokenService"/>.</param>
        /// <param name="ipfsService"><see cref="IIPFSService"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="chainName">Blockchain name.</param>
        /// <param name="score">The wallet score.</param>
        /// <param name="additionalTraits">Additional traits.</param>
        /// <returns>Returns the token metadata IPFS URL.</returns>
        public static async Task<Result<string?>> TokenMetadataAsync<TWalletRequest>(
            this IScoreSoulboundTokenService soulboundTokenService,
            IIPFSService ipfsService,
            ICacheProviderService cacheProviderService,
            TWalletRequest request,
            ulong chainId,
            string? chainName,
            double score,
            IList<NFTTrait>? additionalTraits = default)
            where TWalletRequest : WalletStatsRequest
        {
            if (!request.PrepareToMint)
            {
                return await Result<string?>.FailAsync($"Can't get token metadata: {nameof(request.PrepareToMint)} parameter is false.").ConfigureAwait(false);
            }

            try
            {
                var tokenImageResult = await soulboundTokenService.GetSoulboundTokenImageAsync(new ScoreSoulboundTokenImageRequest
                {
                    Address = request.Address,
                    Score = (byte)(score * 100),
                    Type = request.CalculationModel.ToString(),
                    Size = 512,
                    ChainId = chainId == 11101011 ? null : chainId
                }).ConfigureAwait(false);

                if (tokenImageResult.Succeeded)
                {
                    string? uploadedImageData = await cacheProviderService.GetStringFromCacheAsync($"image_data_{request.Address}_{chainId}_{(int)request.CalculationModel}_{request.ScoreType.ToString()}_{score}").ConfigureAwait(false);
                    if (uploadedImageData == null)
                    {
                        using var tokenImageStream = new MemoryStream(tokenImageResult.Data.Image!);
                        var uploadImageResult = await ipfsService.UploadFileAsync(new IPFSUploadFileRequest
                        {
                            FileContent = tokenImageStream,
                            FileName = $"{request.Address}_{chainId}_{request.CalculationModel.ToString()}_{request.ScoreType.ToString()}.png",
                            Options = new AddFileOptions
                            {
                                Pin = true
                            }
                        }).ConfigureAwait(false);
                        if (uploadImageResult.Succeeded)
                        {
                            uploadedImageData = uploadImageResult.Data;
                            await cacheProviderService.SetCacheAsync($"image_data_{request.Address}_{chainId}_{(int)request.CalculationModel}_{request.ScoreType.ToString()}_{score}", uploadedImageData!, new DistributedCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow = new TimeSpan(1, 0, 0, 0)
                            }).ConfigureAwait(false);
                        }
                    }

                    if (uploadedImageData != null)
                    {
                        var metadataResult = await soulboundTokenService.GetSoulboundTokenMetadataAsync(new NFTMetadataRequest
                        {
                            Image = string.Format(ipfsService.Settings.IpfsGatewayUrlTemplate!, uploadedImageData),
                            Attributes = new List<NFTTrait>
                            {
                                new()
                                {
                                    TraitType = "Blockchain",
                                    Value = chainName
                                },
                                new()
                                {
                                    DisplayType = "number",
                                    TraitType = "Chain id",
                                    Value = chainId
                                },
                                new()
                                {
                                    DisplayType = "boost_percentage",
                                    TraitType = "Score",
                                    Value = score * 100
                                },
                                new()
                                {
                                    TraitType = "Calculation model",
                                    Value = request.CalculationModel.ToString()
                                },
                                new()
                                {
                                    TraitType = "Score type",
                                    Value = request.ScoreType.ToString()
                                },
                                new()
                                {
                                    DisplayType = "date",
                                    TraitType = "Timestamp",
                                    Value = DateTime.UtcNow.ConvertToTimestamp()
                                }
                            }.Union(additionalTraits ?? new List<NFTTrait>()).ToList()
                        }).ConfigureAwait(false);

                        if (metadataResult.Succeeded)
                        {
                            string? tokenMetadata = await cacheProviderService.GetStringFromCacheAsync($"token_metadata_{request.Address}_{chainId}_{(int)request.CalculationModel}_{request.ScoreType.ToString()}_{score}").ConfigureAwait(false);
                            if (tokenMetadata == null)
                            {
                                using var tokenMetadataStream = new MemoryStream(JsonSerializer.Serialize(metadataResult.Data).ToByteArray(Encoding.UTF8));
                                var uploadMetadataResult = await ipfsService.UploadFileAsync(new IPFSUploadFileRequest
                                {
                                    FileContent = tokenMetadataStream,
                                    FileName = $"{request.Address}_{chainId}_{request.CalculationModel.ToString()}_{request.ScoreType.ToString()}.json",
                                    Options = new AddFileOptions
                                    {
                                        Pin = true
                                    }
                                }).ConfigureAwait(false);

                                if (uploadMetadataResult.Succeeded)
                                {
                                    tokenMetadata = uploadMetadataResult.Data;
                                    await cacheProviderService.SetCacheAsync($"token_metadata_{request.Address}_{chainId}_{(int)request.CalculationModel}_{request.ScoreType.ToString()}_{score}", tokenMetadata!, new DistributedCacheEntryOptions
                                    {
                                        AbsoluteExpirationRelativeToNow = new TimeSpan(1, 0, 0, 0)
                                    }).ConfigureAwait(false);
                                }
                            }

                            if (tokenMetadata != null)
                            {
                                return await Result<string?>.SuccessAsync(string.Format(ipfsService.Settings.IpfsGatewayUrlTemplate!, tokenMetadata), "Successfully got token metadata URL.").ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return await Result<string?>.FailAsync(e.Message).ConfigureAwait(false);
            }

            return await Result<string?>.FailAsync("Cant get token metadata.").ConfigureAwait(false);
        }

        /// <summary>
        /// Get historical median balance in USD.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="scoringService"><see cref="IScoringService"/>.</param>
        /// <param name="wallet">Wallet address.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <param name="settings"><see cref="IUseHistoricalMedianBalanceUSDSettings"/>.</param>
        /// <param name="currentUsdBalance">Current balance in USD.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns historical median balance in USD.</returns>
        public static async Task<decimal> MedianBalanceUsdAsync<TWalletStats>(
            this IScoringService scoringService,
            string wallet,
            ulong chainId,
            ScoringCalculationModel calculationModel,
            IUseHistoricalMedianBalanceUSDSettings settings,
            decimal? currentUsdBalance = null,
            CancellationToken cancellationToken = default)
            where TWalletStats : class, IWalletBalanceStats
        {
            if (!settings.UseHistoricalMedianBalanceUSD.TryGetValue(calculationModel, out bool use) || !use)
            {
                return 0;
            }

            var historicalScoringData = await scoringService.GetScoringStatsDataFromDatabaseAsync(wallet, chainId, calculationModel, settings.MedianBalanceStartFrom, cancellationToken).ConfigureAwait(false);

            // ReSharper disable once InconsistentNaming
            var historicalUSDBalances = historicalScoringData?
                .OrderByDescending(x => x.CreatedOn)
                .Take(settings.MedianBalanceLastCount ?? historicalScoringData.Count)
                .Select(x => JsonSerializer.Deserialize<TWalletStats>(x.StatData))
                .Where(x => x != null)
                .Select(x => x as IWalletBalanceStats)
                .Where(x => x != null)
                .Select(x => (x?.NativeBalanceUSD ?? 0) + (x?.HoldTokensBalanceUSD ?? 0))
                .ToList();

            if (currentUsdBalance != null)
            {
                historicalUSDBalances?.Add((decimal)currentUsdBalance);
            }

            var sortedValues = historicalUSDBalances?.Distinct().OrderBy(val => val).ToList();
            var sortedValuesWithPrecision = sortedValues?
                .Where((val, index) => index == 0 || Math.Abs(val - sortedValues[index - 1]) >= settings.MedianBalancePrecision)
                .ToList();
            if (sortedValuesWithPrecision?.Count == 1)
            {
                return sortedValues?.Median() ?? 0;
            }

            return sortedValuesWithPrecision?.Median() ?? 0;
        }

        /// <summary>
        /// Get extended counterparties data.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <typeparam name="TNormalTransaction">The normal transaction type.</typeparam>
        /// <typeparam name="TInternalTransaction">The internal transaction type.</typeparam>
        /// <typeparam name="TERC20TokenTransfer">The ERC20 token transfer type.</typeparam>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="defiLlamaService"><see cref="IDefiLlamaService"/>.</param>
        /// <param name="counterpartiesRequest"><see cref="CounterpartiesRequest{TNormalTransaction, TInternalTransaction, TERC20TokenTransfer}"/>.</param>
#pragma warning disable MA0016
        public static async Task<ExtendedCounterpartiesDataLists<TNormalTransaction, TInternalTransaction, TERC20TokenTransfer>?> ExtendedCounterpartiesDataAsync<TWalletRequest, TNormalTransaction, TInternalTransaction, TERC20TokenTransfer>(
            this TWalletRequest request,
            IDefiLlamaService defiLlamaService,
            CounterpartiesRequest<TNormalTransaction, TInternalTransaction, TERC20TokenTransfer> counterpartiesRequest)
#pragma warning restore CA1045
#pragma warning restore MA0016
            where TWalletRequest : WalletStatsRequest
            where TNormalTransaction : INormalTransaction, new()
            where TInternalTransaction : IInternalTransaction
            where TERC20TokenTransfer : IERC20TokenTransfer
        {
            if (counterpartiesRequest.Settings.CounterpartiesFilterData.TryGetValue(request.CalculationModel, out var counterpartiesData))
            {
                var extendedCounterpartiesData = new List<ExtendedCounterpartyData>();
                var filteredTransactions = new List<TNormalTransaction>();
                var filteredInternalTransactions = new List<TInternalTransaction>();
                var filteredErc20Transfers = new List<TERC20TokenTransfer>();
                var filteredTokenTransfers = new List<NFTTokenTransfer>();

                foreach (var counterpartyData in counterpartiesData.Where(x => x.UseCounterparty))
                {
                    if (string.IsNullOrWhiteSpace(counterpartyData.ContractAddress))
                    {
                        continue;
                    }

                    var counterpartyTransactions = counterpartiesRequest.Transactions
                        .Where(x =>
                            !counterpartyData.IsSocket &&
                            x.IsError?.Equals("1") != true &&
                            x.FunctionName?.StartsWith("approve", StringComparison.OrdinalIgnoreCase) != true &&
                            (x.ContractAddress?.Equals(counterpartyData.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true ||
                            x.To?.Equals(counterpartyData.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true))
                        .ToList();

                    // check allowed methods if exists
                    if (counterpartyData.Methods.Any())
                    {
                        counterpartyTransactions = counterpartyTransactions
                            .Where(x => string.IsNullOrWhiteSpace(x.FunctionName) || counterpartyData.Methods.Any(m => x.FunctionName?.StartsWith(m, StringComparison.OrdinalIgnoreCase) != false))
                            .ToList();
                    }

                    // get counterparty transactions hashes
                    var counterpartyTransactionHashes = counterpartyTransactions.Select(x => x.Hash?.ToLowerInvariant()).Where(x => x != null).Cast<string>().Distinct().ToList();
                    filteredTransactions.AddRange(counterpartyTransactions);

                    var counterpartyInternalTransactions = counterpartiesRequest.InternalTransactions
                        .Where(x =>
                            x.To?.Equals(counterpartyData.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true)
                        .ToList();
                    filteredInternalTransactions.AddRange(counterpartyInternalTransactions);

                    // get counterparty ERC-20 transfers
                    var counterpartyErc20Transfers = counterpartiesRequest.Erc20Tokens
                        .Where(x =>
                            x.From?.Equals("0x0000000000000000000000000000000000000000") != true &&
                            (x.ContractAddress?.Equals(counterpartyData.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true ||
                            x.To?.Equals(counterpartyData.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true ||
                            counterpartyTransactionHashes.Contains(x.Hash?.ToLowerInvariant() ?? string.Empty)))
                        .ToList();

                    // exclude ERC-20 transfers witch are not allowed by methods
                    counterpartyErc20Transfers = counterpartyErc20Transfers.Where(x =>
                    {
                        var transaction = counterpartiesRequest.Transactions.Find(t => t.Hash?.Equals(x.Hash, StringComparison.OrdinalIgnoreCase) == true);
                        if (transaction != null && counterpartyData.Methods.Any() && !string.IsNullOrWhiteSpace(transaction.FunctionName) && !counterpartyData.Methods.Any(m => transaction.FunctionName?.StartsWith(m, StringComparison.OrdinalIgnoreCase) == true))
                        {
                            return false;
                        }

                        if (transaction != null && !filteredTransactions.Contains(transaction))
                        {
                            counterpartyTransactions.Add(transaction);
                            filteredTransactions.Add(transaction);
                            if (!string.IsNullOrWhiteSpace(transaction.Hash) && !counterpartyTransactionHashes.Contains(transaction.Hash))
                            {
                                counterpartyTransactionHashes.Add(transaction.Hash);
                            }
                        }

                        return true;
                    }).ToList();
                    filteredErc20Transfers.AddRange(counterpartyErc20Transfers);

                    // remove transaction hashes based on allowed counterparty ERC-20 transfers
                    counterpartyTransactionHashes.RemoveAll(x =>
                        counterpartiesRequest.Erc20Tokens.Select(t => t.Hash).Contains(x) &&
                        !filteredErc20Transfers.Select(t => t.Hash).Contains(x));

                    // get counterparty NFT transfers
                    var counterpartyTokenTransfers = counterpartiesRequest.TokenTransfers
                        .Where(x =>
                            x.ContractAddress?.Equals(counterpartyData.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true ||
                            counterpartyTransactionHashes.Contains(x.Hash?.ToLowerInvariant() ?? string.Empty))
                        .ToList();

                    // exclude NFT transfers witch are not allowed by methods
                    counterpartyTokenTransfers = counterpartyTokenTransfers.Where(x =>
                    {
                        var transaction = counterpartiesRequest.Transactions.Find(t => t.Hash?.Equals(x.Hash, StringComparison.OrdinalIgnoreCase) == true);
                        if (transaction != null && counterpartyData.Methods.Any() && !string.IsNullOrWhiteSpace(transaction.FunctionName) && !counterpartyData.Methods.Any(m => transaction.FunctionName?.StartsWith(m, StringComparison.OrdinalIgnoreCase) == true))
                        {
                            return false;
                        }

                        if (transaction != null && !filteredTransactions.Contains(transaction))
                        {
                            counterpartyTransactions.Add(transaction);
                            filteredTransactions.Add(transaction);
                            if (!string.IsNullOrWhiteSpace(transaction.Hash) && !counterpartyTransactionHashes.Contains(transaction.Hash))
                            {
                                counterpartyTransactionHashes.Add(transaction.Hash);
                            }
                        }

                        return true;
                    }).ToList();
                    filteredTokenTransfers.AddRange(counterpartyTokenTransfers);

                    // remove transaction hashes based on allowed counterparty NFT transfers
                    counterpartyTransactionHashes.RemoveAll(x =>
                        counterpartiesRequest.TokenTransfers.Select(t => t.Hash).Contains(x) &&
                        !filteredTokenTransfers.Select(t => t.Hash).Contains(x));

                    if (counterpartyTransactions.Count > 0 || counterpartyErc20Transfers.Count > 0)
                    {
                        var counterpartyTransferBalances = new List<TokenDataBalance>();
                        foreach (var counterpartyErc20Transfer in counterpartyErc20Transfers.Where(x => x.From?.Equals(request.Address, StringComparison.InvariantCultureIgnoreCase) == true))
                        {
                            var erc20TokenData = counterpartiesRequest.TokenDataBalances.Find(x => x.Id?.Equals(counterpartyErc20Transfer.ContractAddress, StringComparison.InvariantCultureIgnoreCase) == true);
                            if (erc20TokenData != null)
                            {
                                if (erc20TokenData.Price == 0 &&
                                    counterpartiesRequest.TokensPricesData.TryGetValue(counterpartyErc20Transfer.ContractAddress?.ToLowerInvariant() ?? string.Empty, out decimal tokenPrice) && tokenPrice > 0)
                                {
                                    erc20TokenData.Price = tokenPrice;
                                }

                                if (request.UseDefillamaForUnknownTokens && !string.IsNullOrWhiteSpace(counterpartiesRequest.DefillamaPlatformId) && erc20TokenData.Price == 0)
                                {
                                    var tokenPrices = await defiLlamaService.TokensPriceAsync(
                                        new List<string> { $"{counterpartiesRequest.DefillamaPlatformId}:{counterpartyErc20Transfer.ContractAddress}" }).ConfigureAwait(false);

                                    if (tokenPrices?.TokensPrices.ContainsKey($"{counterpartiesRequest.DefillamaPlatformId}:{counterpartyErc20Transfer.ContractAddress}") == true)
                                    {
                                        erc20TokenData.Price = tokenPrices.TokensPrices[$"{counterpartiesRequest.DefillamaPlatformId}:{counterpartyErc20Transfer.ContractAddress}"].Price;
                                    }
                                }

                                counterpartyTransferBalances.Add(new TokenDataBalance(erc20TokenData, counterpartyErc20Transfer.Value.ToBigInteger()));
                            }
                            else
                            {
                                if (request.UseDefillamaForUnknownTokens && !string.IsNullOrWhiteSpace(counterpartiesRequest.DefillamaPlatformId))
                                {
                                    var tokenPrices = await defiLlamaService.TokensPriceAsync(
                                        new List<string> { $"{counterpartiesRequest.DefillamaPlatformId}:{counterpartyErc20Transfer.ContractAddress}" }).ConfigureAwait(false);

                                    if (tokenPrices?.TokensPrices.ContainsKey($"{counterpartiesRequest.DefillamaPlatformId}:{counterpartyErc20Transfer.ContractAddress}") == true)
                                    {
                                        var tokenPriceData = tokenPrices.TokensPrices[$"{counterpartiesRequest.DefillamaPlatformId}:{counterpartyErc20Transfer.ContractAddress}"];
                                        if (tokenPriceData.Price == 0 &&
                                            counterpartiesRequest.TokensPricesData.TryGetValue(counterpartyErc20Transfer.ContractAddress?.ToLowerInvariant() ?? string.Empty, out decimal tokenPrice) && tokenPrice > 0)
                                        {
                                            tokenPriceData.Price = tokenPrice;
                                        }

                                        var tokenDataBalance = new TokenDataBalance(
                                            new TokenDataBalance
                                            {
                                                Confidence = tokenPriceData.Confidence,
                                                Decimals = tokenPriceData.Decimals?.ToString(),
                                                Id = counterpartyErc20Transfer.ContractAddress,
                                                LastPriceDateTime = tokenPriceData.LastPriceDateTime,
                                                Price = tokenPriceData.Price,
                                                Symbol = tokenPriceData.Symbol,
                                                ChainId = counterpartiesRequest.ChainId,
                                                LogoUri = tokenPriceData.LogoUri,
                                                Source = nameof(BlockchainPlatform.DefiLLama)
                                            },
                                            counterpartyErc20Transfer.Value.ToBigInteger());

                                        counterpartyTransferBalances.Add(new TokenDataBalance(tokenDataBalance, counterpartyErc20Transfer.Value.ToBigInteger()));
                                    }
                                    else if (counterpartiesRequest.TokensPricesData.TryGetValue(counterpartyErc20Transfer.ContractAddress?.ToLowerInvariant() ?? string.Empty, out decimal tokenPrice) && tokenPrice > 0)
                                    {
                                        var erc20Transfer = counterpartiesRequest.Erc20Tokens.Find(x => x.Hash?.Equals(counterpartyErc20Transfer.Hash, StringComparison.InvariantCultureIgnoreCase) == true);
                                        var tokenDataBalance = new TokenDataBalance(
                                            new TokenDataBalance
                                            {
                                                Confidence = 0.9M,
                                                Id = counterpartyErc20Transfer.ContractAddress,
                                                Price = tokenPrice,
                                                ChainId = counterpartiesRequest.ChainId,
                                                Decimals = (erc20Transfer as BaseEvmERC20TokenTransfer)?.TokenDecimal,
                                                Symbol = (erc20Transfer as BaseEvmERC20TokenTransfer)?.TokenSymbol,
                                                Name = (erc20Transfer as BaseEvmERC20TokenTransfer)?.TokenName,
                                                Source = "Unknown"
                                            },
                                            counterpartyErc20Transfer.Value.ToBigInteger());

                                        counterpartyTransferBalances.Add(new TokenDataBalance(tokenDataBalance, counterpartyErc20Transfer.Value.ToBigInteger()));
                                    }
                                }
                                else if (counterpartiesRequest.TokensPricesData.TryGetValue(counterpartyErc20Transfer.ContractAddress?.ToLowerInvariant() ?? string.Empty, out decimal tokenPrice) && tokenPrice > 0)
                                {
                                    var erc20Transfer = counterpartiesRequest.Erc20Tokens.Find(x => x.Hash?.Equals(counterpartyErc20Transfer.Hash, StringComparison.InvariantCultureIgnoreCase) == true);
                                    var tokenDataBalance = new TokenDataBalance(
                                        new TokenDataBalance
                                        {
                                            Confidence = 0.9M,
                                            Id = counterpartyErc20Transfer.ContractAddress,
                                            Price = tokenPrice,
                                            ChainId = counterpartiesRequest.ChainId,
                                            Decimals = (erc20Transfer as BaseEvmERC20TokenTransfer)?.TokenDecimal,
                                            Symbol = (erc20Transfer as BaseEvmERC20TokenTransfer)?.TokenSymbol,
                                            Name = (erc20Transfer as BaseEvmERC20TokenTransfer)?.TokenName,
                                            Source = "Unknown"
                                        },
                                        counterpartyErc20Transfer.Value.ToBigInteger());

                                    counterpartyTransferBalances.Add(new TokenDataBalance(tokenDataBalance, counterpartyErc20Transfer.Value.ToBigInteger()));
                                }
                            }
                        }

                        if (counterpartyTransactions.Count > 0)
                        {
                            var transactionsValue = new BigInteger(0);
                            foreach (var counterpartyTransaction in counterpartyTransactions)
                            {
                                transactionsValue += counterpartyTransaction.Value.ToBigInteger();
                            }

                            if (!string.IsNullOrWhiteSpace(counterpartiesRequest.DefillamaPlatformId) && counterpartiesRequest.NativeTokenDataBalance?.Price == 0)
                            {
                                var tokenPrices = await defiLlamaService.TokensPriceAsync(
                                    new List<string> { $"{counterpartiesRequest.DefillamaPlatformId}:{counterpartiesRequest.NativeTokenDataBalance.Id}" }).ConfigureAwait(false);

                                if (tokenPrices?.TokensPrices.ContainsKey($"{counterpartiesRequest.DefillamaPlatformId}:{counterpartiesRequest.NativeTokenDataBalance.Id}") == true)
                                {
                                    counterpartiesRequest.NativeTokenDataBalance.Price = tokenPrices.TokensPrices[$"{counterpartiesRequest.DefillamaPlatformId}:{counterpartiesRequest.NativeTokenDataBalance.Id}"].Price;
                                }
                            }

                            counterpartyTransferBalances.Add(new TokenDataBalance(counterpartiesRequest.NativeTokenDataBalance ?? new TokenDataBalance(), transactionsValue));
                        }

                        extendedCounterpartiesData.Add(new ExtendedCounterpartyData(counterpartyData)
                        {
                            CounterpartyTurnoverUSD = counterpartyTransferBalances?.Sum(x => x.TotalAmountPrice),
#pragma warning disable S3358 // Ternary operators should not be nested
                            CounterpartyTransactions = counterpartyTransactions.Count > 0
                                ? counterpartyTransactions.Count
                                : counterpartyTransactionHashes.Count > 0 ? counterpartyTransactionHashes.Count : null,
#pragma warning restore S3358 // Ternary operators should not be nested
                            CounterpartyTransfers = counterpartyErc20Transfers.Count > 0 ? counterpartyErc20Transfers.Count : null,
                            CounterpartyTransactionHashes = counterpartyTransactionHashes.Count > 0 ? counterpartyTransactionHashes : null,
                            CounterpartyNFTTransfers = counterpartyTokenTransfers.Count > 0 ? counterpartyTokenTransfers : null,
                            CounterpartyTransferBalances = counterpartyTransferBalances,
                            ChainId = counterpartiesRequest.ChainId
                        });
                    }
                }

                bool onlyCounterparties = request is IWalletCounterpartiesRequest { CalculateOnlyCounterparties: true };
                return new ExtendedCounterpartiesDataLists<TNormalTransaction, TInternalTransaction, TERC20TokenTransfer>
                {
                    ExtendedCounterpartiesDataList = extendedCounterpartiesData,
                    Transactions = onlyCounterparties ? filteredTransactions : counterpartiesRequest.Transactions,
                    InternalTransactions = onlyCounterparties ? filteredInternalTransactions : counterpartiesRequest.InternalTransactions,
                    Erc20Tokens = onlyCounterparties ? filteredErc20Transfers : counterpartiesRequest.Erc20Tokens,
                    TokenTransfers = onlyCounterparties ? filteredTokenTransfers : counterpartiesRequest.TokenTransfers
                };
            }

            return null;
        }
    }
}