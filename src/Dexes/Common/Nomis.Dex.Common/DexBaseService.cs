// ------------------------------------------------------------------------------------------------------
// <copyright file="DexBaseService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using GraphQL.Client.Abstractions;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Dex.Abstractions;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;
using Nomis.Utils.Wrapper;

namespace Nomis.Dex.Common
{
    /// <inheritdoc cref="IDexBaseService"/>
    // ReSharper disable once InconsistentNaming
    public class DexBaseService :
        IDexBaseService
    {
        private readonly IGraphQLClient _client;
        private readonly ICacheProviderService _cacheProvider;

        /// <summary>
        /// Initialize <see cref="DexBaseService"/>.
        /// </summary>
        /// <param name="client"><see cref="IGraphQLClient"/>.</param>
        /// <param name="cacheProvider"><see cref="ICacheProviderService"/>.</param>
        /// <param name="dexDescriptor"><see cref="IDexDescriptor"/>.</param>
        /// <param name="blockchain"><see cref="Chain"/>.</param>
        public DexBaseService(
            IGraphQLClient client,
            ICacheProviderService cacheProvider,
            IDexDescriptor? dexDescriptor,
            Chain blockchain)
        {
            _client = client;
            _cacheProvider = cacheProvider;
            Blockchain = blockchain;
            DexDescriptor = dexDescriptor;
        }

        /// <inheritdoc />
        public string LastUpdateCacheKey => $"{DexDescriptor?.Provider.ToString()}_swapPairData_lastUpdate";

        /// <inheritdoc />
        public string SwapPairDataCacheKey => $"{DexDescriptor?.Provider.ToString()}_swapPairData";

        /// <inheritdoc/>
        public IDexDescriptor? DexDescriptor { get; }

        /// <inheritdoc/>
        public Chain Blockchain { get; }

        /// <inheritdoc />
        public virtual async Task<Result<(DexProvider, List<ISwapPairData>)>> GetAllSwapPairsAsync(
            Chain blockchain = Chain.None,
            int first = 100,
            int skip = 0,
            bool fromCache = false)
        {
            return await AllSwapPairsAsync(
                $"{blockchain.ToString()}_{first}_{skip}_{SwapPairDataCacheKey}",
                $"{blockchain.ToString()}_{first}_{skip}_{LastUpdateCacheKey}",
                first,
                skip,
                fromCache).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<(DexProvider, List<ISwapPairData>)>> GetAllSwapPairsAsync(
            int first = 100,
            int skip = 0,
            bool fromCache = false)
        {
            return await AllSwapPairsAsync(
                $"{SwapPairDataCacheKey}_{first}_{skip}",
                $"{LastUpdateCacheKey}_{first}_{skip}",
                first,
                skip,
                fromCache).ConfigureAwait(false);
        }

        private async Task<Result<(DexProvider, List<ISwapPairData>)>> AllSwapPairsAsync(
            string swapPairDataCacheKey,
            string lastUpdateCacheKey,
            int first = 100,
            int skip = 0,
            bool fromCache = false)
        {
            if (DexDescriptor?.UseCaching == true && fromCache)
            {
                var pairData = await _cacheProvider.GetFromCacheAsync<List<SwapPairData>>(swapPairDataCacheKey).ConfigureAwait(false);

                if (pairData?.Any() == true)
                {
                    return await Result<(DexProvider, List<ISwapPairData>)>.SuccessAsync((DexDescriptor.Provider, pairData.ConvertAll(x => x as ISwapPairData))).ConfigureAwait(false);
                }
            }

            var currentTime = DateTime.UtcNow;
            var result = await DexPairsAsync(first, skip).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return await Result<(DexProvider, List<ISwapPairData>)>.FailAsync(result.Messages).ConfigureAwait(false);
            }

            foreach (var pairData in result.Data)
            {
                pairData.SetLastCheck(currentTime);
            }

            if (DexDescriptor?.UseCaching == true)
            {
                long lastUpdate = await _cacheProvider.GetFromCacheAsync<long>(lastUpdateCacheKey).ConfigureAwait(false);
                if (lastUpdate != default)
                {
                    var lastUpdateTime = DateTime.FromFileTimeUtc(lastUpdate);
                    if (lastUpdateTime < currentTime)
                    {
                        await _cacheProvider.SetCacheAsync(
                            lastUpdateCacheKey,
                            currentTime.ToFileTimeUtc().ToString(),
                            new() { AbsoluteExpiration = DateTimeOffset.MaxValue }).ConfigureAwait(false);

                        await _cacheProvider.SetCacheAsync(
                            swapPairDataCacheKey,
                            result.Data,
                            new() { AbsoluteExpiration = DateTimeOffset.MaxValue }).ConfigureAwait(false);
                    }
                }
                else
                {
                    await _cacheProvider.SetCacheAsync(
                        lastUpdateCacheKey,
                        currentTime.ToFileTimeUtc().ToString(),
                        new() { AbsoluteExpiration = DateTimeOffset.MaxValue }).ConfigureAwait(false);

                    await _cacheProvider.SetCacheAsync(
                        swapPairDataCacheKey,
                        result.Data,
                        new() { AbsoluteExpiration = DateTimeOffset.MaxValue }).ConfigureAwait(false);
                }
            }

            return await Result<(DexProvider, List<ISwapPairData>)>.SuccessAsync((DexDescriptor!.Provider, result.Data), result.Messages).ConfigureAwait(false);
        }

        private async Task<Result<List<ISwapPairData>>> DexPairsAsync(
            int first = 100,
            int skip = 0)
        {
            try
            {
                var query = new GraphQLRequest
                {
                    Query = $$"""
                    query GetSwapPairs {
                        pairs (orderBy: syncAtTimestamp, orderDirection: desc, first: {{first}}, skip: {{skip}}, where: {reserve0_gt: 0, reserve1_gt: 0, token0Price_gt: 0, token1Price_gt: 0}) {
                          id,
                          token0 {
                            id,
                            symbol,
                            name,
                            decimals
                          },
                          token1 {
                            id,
                            symbol,
                            name,
                            decimals
                          },
                          token0Price,
                          token1Price,
                          reserve0,
                          reserve1,
                          syncAtTimestamp
                        }
                    }
                    """
                };

                var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);

                if (response.Errors?.Any() == true)
                {
                    return await Result<List<ISwapPairData>>.FailAsync(response.Errors.Select(e => e.Message).ToList()).ConfigureAwait(false);
                }

                var pairsData = JsonSerializer.Deserialize<List<SwapPairData>>(response.Data["pairs"]?.ToJsonString(new()) ?? string.Empty);

                return await Result<List<ISwapPairData>>.SuccessAsync(pairsData?.Select(x => x as ISwapPairData).ToList() ?? new List<ISwapPairData>(), "Swap pairs data received.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return await Result<List<ISwapPairData>>.FailAsync($"Failed to get data for DEX provider {DexDescriptor?.Name}: \"{e.Message}\".").ConfigureAwait(false);
            }
        }
    }
}