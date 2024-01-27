// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenDataBalanceExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Contracts.Models;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Blockchain.Abstractions.Requests;
using Nomis.Covalent.Interfaces;
using Nomis.DefiLlama.Interfaces;
using Nomis.DefiLlama.Interfaces.Contracts;
using Nomis.DefiLlama.Interfaces.Models;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Extensions;
using Nomis.Utils.Wrapper;

namespace Nomis.Blockchain.Abstractions.Extensions
{
    /// <summary>
    /// <see cref="TokenDataBalance"/> extension methods.
    /// </summary>
    public static class TokenDataBalanceExtensions
    {
        /// <summary>
        /// Enrich data by DefiLlama API service.
        /// </summary>
        /// <param name="tokenDataBalances">Source list.</param>
        /// <param name="defiLlamaService"><see cref="IDefiLlamaService"/>.</param>
        /// <param name="request">Request.</param>
        /// <param name="platformId">Platform id.</param>
        /// <param name="pricesData">Prices data.</param>
        /// <returns>Returns enriched data by DefiLlama API service.</returns>
#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
        public static async Task<List<T>> EnrichWithDefiLlamaAsync<T>(
            this List<T>? tokenDataBalances,
            IDefiLlamaService defiLlamaService,
            IWalletTokensBalancesRequest request,
            string platformId,
            List<TokenPriceData>? pricesData)
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation
            where T : TokenDataBalance
        {
            if (tokenDataBalances == null)
            {
                return new List<T>();
            }

            try
            {
                var tokenPrices = await defiLlamaService.TokensPriceAsync(
                    tokenDataBalances.Select(t => $"{platformId}:{t.Id}").ToList(), request.TransfersSearchWidthInHours, pricesData).ConfigureAwait(false);
                foreach (var tokenDataBalance in tokenDataBalances)
                {
                    if (tokenPrices?.TokensPrices.ContainsKey($"{platformId}:{tokenDataBalance.Id}") == true)
                    {
                        tokenDataBalance.Source = tokenDataBalance.Price == 0 ? "DefiLlama" : tokenDataBalance.Source ?? "DefiLlama";
                        var tokenPrice = tokenPrices.TokensPrices[$"{platformId}:{tokenDataBalance.Id}"];
                        tokenDataBalance.Confidence = tokenPrice.Confidence;
                        tokenDataBalance.LastPriceDateTime = tokenPrice.LastPriceDateTime;
                        tokenDataBalance.Price = tokenDataBalance.Price == 0 ? tokenPrice.Price : tokenDataBalance.Price;
                        tokenDataBalance.Decimals ??= tokenPrice.Decimals?.ToString();
                        tokenDataBalance.Symbol ??= tokenPrice.Symbol;
                    }
                }
            }
            catch (Exception)
            {
                // ignore
            }

            return tokenDataBalances;
        }

        /// <summary>
        /// Enrich data by LlamaFolio API service.
        /// </summary>
        /// <param name="tokenDataBalances">Source list.</param>
        /// <param name="defiLlamaService"><see cref="IDefiLlamaService"/>.</param>
        /// <param name="address">Wallet address.</param>
        /// <param name="request">Request.</param>
        /// <param name="chain">LlamaFolio blockchain id.</param>
        /// <returns>Returns enriched data by LlamaFolio API service.</returns>
#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
        public static async Task<List<T>> EnrichWithLlamaFolioAsync<T>(
            this List<T>? tokenDataBalances,
            IDefiLlamaService defiLlamaService,
            string address,
            WalletStatsRequest request,
            string chain)
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation
            where T : TokenDataBalance
        {
            if (tokenDataBalances == null)
            {
                return new List<T>();
            }

            if (request is ILlamaFolioRequest { UseLlamaFolioAPI: false } or not ILlamaFolioRequest)
            {
                return tokenDataBalances;
            }

            try
            {
                var llamaFolioResult = await defiLlamaService.TokenBalancesAsync(address).ConfigureAwait(false);
                if (llamaFolioResult.Succeeded)
                {
                    var tokensData = llamaFolioResult.Data.Protocols
                        .Where(x => x.Chain?.Equals(chain, StringComparison.OrdinalIgnoreCase) == true)
                        .SelectMany(x => x.Groups)
                        .SelectMany(x => x.Balances)
                        .ToList();

                    foreach (var tokenDataBalance in tokenDataBalances)
                    {
                        var token = tokensData.Find(x => x.Address?.Equals(tokenDataBalance.Id, StringComparison.OrdinalIgnoreCase) == true);
                        if (token != null)
                        {
                            tokenDataBalance.Source = tokenDataBalance.Price == 0 ? "LlamaFolio" : tokenDataBalance.Source ?? "LlamaFolio";
                            tokenDataBalance.Confidence = 0.9M;
                            tokenDataBalance.LastPriceDateTime = DateTime.UtcNow;
                            tokenDataBalance.Price = tokenDataBalance.Price == 0 ? token.Price ?? 0 : tokenDataBalance.Price;
                            tokenDataBalance.Decimals ??= token.Decimals?.ToString();
                            tokenDataBalance.Symbol ??= token.Symbol;
                            tokenDataBalance.Name ??= token.Name;
                        }
                        else
                        {
                            var underlyingToken = tokensData
                                .SelectMany(x => x.Underlyings ?? new List<LlamafolioTokenBalancesProtocolGroupBalanceBaseData>()).FirstOrDefault(x => x.Address?.Equals(tokenDataBalance.Id, StringComparison.OrdinalIgnoreCase) == true);
                            if (underlyingToken != null)
                            {
                                tokenDataBalance.Source = tokenDataBalance.Price == 0 ? "LlamaFolio" : tokenDataBalance.Source ?? "LlamaFolio";
                                tokenDataBalance.Confidence = 0.9M;
                                tokenDataBalance.LastPriceDateTime = DateTime.UtcNow;
                                tokenDataBalance.Price = tokenDataBalance.Price == 0 ? underlyingToken.Price ?? 0 : tokenDataBalance.Price;
                                tokenDataBalance.Decimals ??= underlyingToken.Decimals?.ToString();
                                tokenDataBalance.Symbol ??= underlyingToken.Symbol;
                                tokenDataBalance.Name ??= underlyingToken.Name;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignore
            }

            return tokenDataBalances;
        }

        /// <summary>
        /// Enrich data by tokens balances.
        /// </summary>
        /// <param name="tokenDataBalances">Source list.</param>
        /// <param name="tokensBalances">Tokens balances.</param>
        /// <returns>Returns enriched data by tokens balances.</returns>
#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
        public static List<T> EnrichWithTokensBalances<T>(
            this List<T>? tokenDataBalances,
            List<TokenDataBalance> tokensBalances)
        #pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation
            where T : TokenDataBalance
        {
            if (tokenDataBalances == null)
            {
                return new List<T>();
            }

            foreach (var tokenDataBalance in tokenDataBalances)
            {
                var tokenData = tokensBalances.Find(x => x.Id?.Equals(tokenDataBalance.Id, StringComparison.OrdinalIgnoreCase) == true);
                if (tokenData != null)
                {
                    tokenDataBalance.Source = tokenDataBalance.Price == 0 ? tokenData.Source : tokenDataBalance.Source;
                    tokenDataBalance.Confidence = tokenDataBalance.Price == 0 ? tokenData.Confidence : tokenDataBalance.Confidence;
                    tokenDataBalance.LastPriceDateTime = tokenDataBalance.Price == 0 ? tokenData.LastPriceDateTime : tokenDataBalance.LastPriceDateTime;
                    tokenDataBalance.Price = tokenDataBalance.Price == 0 ? tokenData.Price : tokenDataBalance.Price;
                    tokenDataBalance.Decimals ??= tokenData.Decimals;
                    tokenDataBalance.Symbol ??= tokenData.Symbol;
                    tokenDataBalance.Name ??= tokenData.Name;
                    tokenDataBalance.ChainId = tokenData.ChainId;
                    tokenDataBalance.LogoUri ??= tokenData.LogoUri;
                }
            }

            return tokenDataBalances;
        }

        /// <summary>
        /// Convert to <see cref="TransferTokenDataBalance"/> array.
        /// </summary>
        /// <param name="tokenTransfers">ERC-20 token transfer list.</param>
        /// <param name="transactions">Transaction list.</param>
        /// <param name="request">Request.</param>
        /// <param name="chainId">Blockchain id.</param>
#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
        public static List<TransferTokenDataBalance> ToTransferTokenDataBalances(
            this List<BaseEvmERC20TokenTransfer>? tokenTransfers,
            List<BaseEvmNormalTransaction> transactions,
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation
            WalletStatsRequest request,
            ulong chainId)
        {
            if (tokenTransfers == null)
            {
                return new List<TransferTokenDataBalance>();
            }

            var transferTokenDataBalances = new List<TransferTokenDataBalance>();
            foreach (var tokenTransfer in tokenTransfers)
            {
                bool isReverted = transactions.Exists(t => t.Hash?.Equals(tokenTransfer.Hash, StringComparison.OrdinalIgnoreCase) == true && string.Equals(t.IsError, "1", StringComparison.OrdinalIgnoreCase));
                if (!isReverted)
                {
                    var transferAmount = tokenTransfer.Value?.ToBigInteger();
                    if (transferAmount > 0)
                    {
                        transferTokenDataBalances.Add(new TransferTokenDataBalance
                        {
                            ChainId = chainId,
                            Balance = (BigInteger)transferAmount,
                            Id = tokenTransfer.ContractAddress,
                            Source = "Explorer",
                            TransactionHash = tokenTransfer.Hash,
                            IsOutcome = tokenTransfer.From?.Equals(request.Address, StringComparison.OrdinalIgnoreCase) == true,
                            InvocationType = transactions.Find(x => x.Hash?.Equals(tokenTransfer.Hash, StringComparison.OrdinalIgnoreCase) == true)?.FunctionName
                        });
                    }
                }
            }

            return transferTokenDataBalances;
        }

        /// <summary>
        /// Get token data balances.
        /// </summary>
        /// <param name="covalentService"><see cref="ICovalentService"/>.</param>
        /// <param name="blockchainDescriptor"><see cref="IBlockchainDescriptor"/>.</param>
        /// <param name="walletAddress">Wallet address.</param>
        /// <param name="tokenDataBalances">Already fetched token data balances.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns token data balances fetched by Covalent API.</returns>
        public static async Task<Result<IList<TokenDataBalance>?>> TokenDataBalancesAsync(
            this ICovalentService covalentService,
            IBlockchainDescriptor blockchainDescriptor,
            string walletAddress,
            IList<TokenDataBalance>? tokenDataBalances = default,
            CancellationToken cancellationToken = default)
        {
            if (blockchainDescriptor.PlatformIds?.ContainsKey(BlockchainPlatform.Covalent) != true)
            {
                return await Result<IList<TokenDataBalance>?>.FailAsync(tokenDataBalances, $"There is no Covalent chain id for {blockchainDescriptor.ChainName} blockchain.").ConfigureAwait(false);
            }

            string covalentChainId = blockchainDescriptor.PlatformIds[BlockchainPlatform.Covalent];
            var covalentTokenBalancesResponse = await covalentService.WalletBalancesAsync(covalentChainId, walletAddress, false, cancellationToken).ConfigureAwait(false);
            if (covalentTokenBalancesResponse.Error)
            {
                return await Result<IList<TokenDataBalance>?>.FailAsync(tokenDataBalances, covalentTokenBalancesResponse.ErrorMessage ?? $"Error occurred while tried to fetch token balances by Covalent API for {blockchainDescriptor.ChainName} blockchain.").ConfigureAwait(false);
            }

            if (covalentTokenBalancesResponse.Data?.Tokens.Any() != true)
            {
                return await Result<IList<TokenDataBalance>?>.FailAsync(tokenDataBalances, covalentTokenBalancesResponse.ErrorMessage ?? $"There is no token balances fetched by Covalent API for given wallet address for {blockchainDescriptor.ChainName} blockchain.").ConfigureAwait(false);
            }

            foreach (var tokenData in covalentTokenBalancesResponse.Data.Tokens.Where(x => !x.NativeToken))
            {
                var tokenDataBalance = tokenDataBalances?.FirstOrDefault(x => x.Id?.Equals(tokenData.ContractAddress, StringComparison.OrdinalIgnoreCase) == true);
                if (tokenDataBalance is { Price: 0 })
                {
                    tokenDataBalance.Price = tokenData.QuoteRate ?? 0;
                    tokenDataBalance.LastPriceDateTime ??= DateTime.UtcNow;
                    tokenDataBalance.Balance = tokenData.Balance.ToBigInteger();
                    tokenDataBalance.Confidence = !tokenData.IsSpam ? 0.9M : 0;
                    tokenDataBalance.LogoUri ??= tokenData.LogoUrl;
                    tokenDataBalance.Name ??= tokenData.ContractName;
                    tokenDataBalance.Symbol ??= tokenData.ContractTickerSymbol;
                    tokenDataBalance.Decimals ??= tokenData.ContractDecimals.ToString() ?? "18";
                    tokenDataBalance.Source = nameof(BlockchainPlatform.Covalent);
                }
                else
                {
                    tokenDataBalances ??= new List<TokenDataBalance>();
                    tokenDataBalances.Add(new TokenDataBalance
                    {
                        Id = tokenData.ContractAddress,
                        ChainId = blockchainDescriptor.ChainId,
                        Price = tokenData.QuoteRate ?? 0,
                        Symbol = tokenData.ContractTickerSymbol,
                        Name = tokenData.ContractName,
                        Balance = tokenData.Balance.ToBigInteger(),
                        Decimals = tokenData.ContractDecimals.ToString() ?? "18",
                        Confidence = !tokenData.IsSpam ? 0.9M : 0,
                        LastPriceDateTime = DateTime.UtcNow,
                        LogoUri = tokenData.LogoUrl,
                        Source = nameof(BlockchainPlatform.Covalent)
                    });
                }
            }

            return await Result<IList<TokenDataBalance>?>.SuccessAsync(tokenDataBalances, $"Successfully fetched token balances by Covalent API for given wallet address for {blockchainDescriptor.ChainName} blockchain.").ConfigureAwait(false);
        }
    }
}