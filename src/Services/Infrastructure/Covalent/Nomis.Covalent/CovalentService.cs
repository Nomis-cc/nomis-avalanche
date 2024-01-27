// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Covalent.Interfaces;
using Nomis.Covalent.Interfaces.Models;
using Nomis.Covalent.Interfaces.Responses;
using Nomis.Covalent.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;

namespace Nomis.Covalent
{
    /// <inheritdoc cref="ICovalentService"/>
    internal sealed class CovalentService :
        ICovalentService,
        ISingletonService,
        IDisposable
    {
        private readonly CovalentSettings _settings;
        private readonly HttpClient _client;
        private readonly ICacheProviderService _cacheProviderService;
        private readonly ILogger<CovalentService> _logger;

        /// <summary>
        /// Initialize <see cref="CovalentService"/>.
        /// </summary>
        /// <param name="settings"><see cref="CovalentSettings"/>.</param>
        /// <param name="client"><see cref="HttpClient"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public CovalentService(
            CovalentSettings settings,
            HttpClient client,
            ICacheProviderService cacheProviderService,
            ILogger<CovalentService> logger)
        {
            _settings = settings;
            _client = client;
            _cacheProviderService = cacheProviderService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<CovalentChainsResponse> AllChainsAsync(
            CancellationToken cancellationToken = default)
        {
            var result = await _cacheProviderService.GetFromCacheAsync<CovalentChainsResponse>("covalent_chains").ConfigureAwait(false) ?? new CovalentChainsResponse
            {
                Data = new CovalentChainsData
                {
                    Chains = new List<CovalentChainData>(),
                    UpdatedAt = DateTime.UtcNow
                }
            };

            if (result.Data?.Chains.Any() == true)
            {
                return result;
            }

            string request =
                $"/v1/chains/?key={_settings.ApiKey}";

            var response = await _client.GetAsync(request, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("{Service}: There is an error while fetching blockchains data with status code {StatusCode}: {Response}", nameof(CovalentService), response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
                return result;
            }

            result = await response.Content.ReadFromJsonAsync<CovalentChainsResponse>(cancellationToken: cancellationToken).ConfigureAwait(false)
                     ?? throw new CustomException("Can't get Covalent blockchains data.");

            await _cacheProviderService.SetCacheAsync("covalent_chains", result, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = new TimeSpan(256, 0, 0, 0)
            }).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public async Task<CovalentTokenBalancesResponse> WalletBalancesAsync(
            string chain,
            string address,
            bool fetchNft = false,
            CancellationToken cancellationToken = default)
        {
            var result = _settings.UseTokenBalancesCaching
                ? await _cacheProviderService.GetFromCacheAsync<CovalentTokenBalancesResponse>($"{chain}_{fetchNft}_{address}_covalent_token_balances").ConfigureAwait(false) ?? new CovalentTokenBalancesResponse
                {
                    Error = true
                }
                : new CovalentTokenBalancesResponse
                {
                    Error = true
                };

            if (!result.Error)
            {
                return result;
            }

            string request =
                $"/v1/{chain}/address/{address}/balances_v2/?quote-currency=USD&no-spam=true&nft={fetchNft}&key={_settings.ApiKey}";

            var response = await _client.GetAsync(request, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("{Service}: There is an error while fetching wallet token balances with request {Request} status code {StatusCode}: {Response}", nameof(CovalentService), response.RequestMessage?.RequestUri, response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
                return result;
            }

            result = await response.Content.ReadFromJsonAsync<CovalentTokenBalancesResponse>(cancellationToken: cancellationToken).ConfigureAwait(false)
                     ?? throw new CustomException($"Can't get wallet token balances in {chain} blockchain.");

            if (_settings.UseTokenBalancesCaching)
            {
                await _cacheProviderService.SetCacheAsync($"{chain}_{fetchNft}_{address}_covalent_token_balances", result, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _settings.TokenBalancesCacheDuration
                }).ConfigureAwait(false);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<CovalentNftsResponse> WalletNftsAsync(
            string chain,
            string address,
            bool noSpam = true,
            CancellationToken cancellationToken = default)
        {
            var result = _settings.UseNftBalancesCaching
                ? await _cacheProviderService.GetFromCacheAsync<CovalentNftsResponse>($"{chain}_{address}_covalent_nft_balances").ConfigureAwait(false) ?? new CovalentNftsResponse
                {
                    Error = true
                }
                : new CovalentNftsResponse
                {
                    Error = true
                };

            if (!result.Error)
            {
                return result;
            }

            string request =
                $"/v1/{chain}/address/{address}/balances_nft/?no-spam={noSpam}&with-uncached=true&key={_settings.ApiKey}";

            var response = await _client.GetAsync(request, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("{Service}: There is an error while fetching wallet NFT balances with request {Request} status code {StatusCode}: {Response}", nameof(CovalentService), response.RequestMessage?.RequestUri, response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
                return result;
            }

            result = await response.Content.ReadFromJsonAsync<CovalentNftsResponse>(cancellationToken: cancellationToken).ConfigureAwait(false)
                     ?? throw new CustomException($"Can't get wallet NFT token balances in {chain} blockchain.");

            if (_settings.UseNftBalancesCaching)
            {
                await _cacheProviderService.SetCacheAsync($"{chain}_{address}_covalent_nft_balances", result, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _settings.NftTokenBalancesCacheDuration
                }).ConfigureAwait(false);
            }

            return result;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}