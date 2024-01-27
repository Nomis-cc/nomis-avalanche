// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Blockchain.Abstractions.Requests;
using Nomis.DefiLlama.Interfaces.Models;
using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces.Requests;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Enums;

namespace Nomis.DexProviderService.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get stablecoins price data.
        /// </summary>
        /// <param name="service"><see cref="IDexProviderService"/>.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <returns>Returns stablecoins price data.</returns>
        public static IList<TokenPriceData> StablecoinsPriceData(
            this IDexProviderService service,
            ulong chainId)
        {
            var stablecoinsResult = service.StablecoinsData();
            if (stablecoinsResult.Succeeded)
            {
                return stablecoinsResult.Data.Where(x => Enum.TryParse<Chain>(chainId.ToString(), out var chain) && x.Contracts.ContainsKey(chain)).Select(x => new TokenPriceData
                {
                    Id = x.Contracts[(Chain)chainId],
                    Price = 1,
                    Confidence = 1,
                    Symbol = x.Symbol
                }).ToList();
            }

            return new List<TokenPriceData>();
        }

        /// <summary>
        /// Get the blockchain descriptor in which the score will be minted.
        /// </summary>
        /// <param name="service"><see cref="IDexProviderService"/>.</param>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="scoredChain">Scored chain.</param>
        /// <param name="isEnabled">Is enabled.</param>
        /// <param name="blockchainType">Blockchain type.</param>
        /// <returns>Returns the blockchain descriptor in which the score will be minted.</returns>
        public static IBlockchainDescriptor? MintChain(
            this IDexProviderService service,
            IHasMintChain request,
            ulong scoredChain,
            bool? isEnabled = true,
            BlockchainType blockchainType = BlockchainType.EVM)
        {
            var mintChain = request.MintChain;
            var supportedBlockchains = service.Blockchains(blockchainType, isEnabled);
            if (mintChain == Utils.Enums.MintChain.Native)
            {
                var scoredChainDescriptor = supportedBlockchains.Data.Find(b => b.ChainId == scoredChain);
                if (request.MintBlockchainType == MintChainType.Testnet)
                {
                    scoredChainDescriptor = supportedBlockchains.Data.Find(b => b.IsTestnet && b.BlockchainSlug?.Equals(scoredChainDescriptor?.BlockchainSlug, StringComparison.InvariantCultureIgnoreCase) == true);
                    if (scoredChainDescriptor == null)
                    {
                        throw new NotSupportedException($"{mintChain} blockchain does not supported or disabled.");
                    }
                }

                return scoredChainDescriptor;
            }

            var mintBlockchain = supportedBlockchains.Data.Find(b => b.ChainId == (ulong)mintChain);
            if (mintBlockchain == null)
            {
                throw new NotSupportedException($"{mintChain} blockchain does not supported or disabled.");
            }

            return mintBlockchain;
        }

        /// <summary>
        /// Enrich data by tokens lists.
        /// </summary>
        /// <param name="tokenDataBalances">Source list.</param>
        /// <param name="dexProviderService"><see cref="IDexProviderService"/>.</param>
        /// <param name="request">Request.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <returns>Returns enriched data by tokens lists.</returns>
#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
        public static async Task<List<T>> EnrichWithTokensListsAsync<T>(
            this List<T>? tokenDataBalances,
            IDexProviderService dexProviderService,
            IWalletTokensBalancesRequest request,
            ulong chainId)
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation
            where T : TokenDataBalance
        {
            if (tokenDataBalances == null)
            {
                return new List<T>();
            }

            if (request.UseTokenLists)
            {
                var dexTokensDataResult = await dexProviderService.TokensDataAsync(new TokensDataRequest
                {
                    Blockchain = (Chain)chainId,
                    IncludeUniversalTokenLists = request.IncludeUniversalTokenLists,
                    FromCache = true
                }).ConfigureAwait(false);

                foreach (var tokenDataBalance in tokenDataBalances)
                {
                    var dexTokenData = dexTokensDataResult.Data.Find(t => t.Id?.Equals(tokenDataBalance.Id, StringComparison.OrdinalIgnoreCase) == true);
                    tokenDataBalance.LogoUri ??= dexTokenData?.LogoUri;
                    tokenDataBalance.Decimals ??= dexTokenData?.Decimals;
                    tokenDataBalance.Symbol ??= dexTokenData?.Symbol;
                    tokenDataBalance.Name ??= dexTokenData?.Name;
                }
            }

            return tokenDataBalances;
        }
    }
}