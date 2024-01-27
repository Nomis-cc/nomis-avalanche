// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nethereum.Util;
using Nomis.CacheProviderService.Interfaces;
using Nomis.DeBank.Interfaces;
using Nomis.DeBank.Interfaces.Models;
using Nomis.DeBank.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Converters;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.DeBank
{
    /// <inheritdoc cref="IDeBankService"/>
    internal sealed class DeBankService :
        IDeBankService,
        ISingletonService,
        IDisposable
    {
        private readonly DeBankSettings _settings;
        private readonly HttpClient _client;
        private readonly ICacheProviderService _cacheProviderService;
        private readonly ILogger<DeBankService> _logger;

        /// <summary>
        /// Initialize <see cref="DeBankService"/>.
        /// </summary>
        /// <param name="settings"><see cref="DeBankSettings"/>.</param>
        /// <param name="client"><see cref="HttpClient"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DeBankService(
            DeBankSettings settings,
            HttpClient client,
            ICacheProviderService cacheProviderService,
            ILogger<DeBankService> logger)
        {
            _settings = settings;
            _client = client;
            _cacheProviderService = cacheProviderService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<DeBankUnitsData> UnitsDataAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await _client.GetAsync("/v1/account/units", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DeBankUnitsData>(cancellationToken: cancellationToken).ConfigureAwait(false)
                   ?? throw new CustomException("Can't get DeBank units data.");
        }

        /// <inheritdoc />
        public async Task<Result<IList<DeBankTokenData>>> HoldTokensDataAsync(
            string owner,
            string debankChainId,
            CancellationToken cancellationToken = default)
        {
            var result = _settings.UseDeBankCaching
                ? await _cacheProviderService.GetFromCacheAsync<List<DeBankTokenData>>($"{debankChainId}_{owner}_debank_tokens").ConfigureAwait(false)
                : null;

            if (result == null)
            {
                var response = await _client.GetAsync($"/v1/user/token_list?id={owner}&chain_id={debankChainId}&is_all=false", cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadFromJsonAsync<List<DeBankTokenData>>(
                             new JsonSerializerOptions
                             {
                                 Converters = { new DecimalStringConverter() }
                             }, cancellationToken: cancellationToken).ConfigureAwait(false)
                         ?? throw new CustomException("Can't get account token balances from API.");

                if (_settings.UseDeBankCaching)
                {
                    await _cacheProviderService.SetCacheAsync($"{debankChainId}_{owner}_debank_tokens", result, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _settings.DeBankHoldTokensCacheDuration
                    }).ConfigureAwait(false);
                }
            }

            return await Result<IList<DeBankTokenData>>.SuccessAsync(result, $"Successfully fetched token balances by DeBank API for given wallet address for {debankChainId} blockchain.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<IList<DeBankTokenData>>> TokensDataAsync(
            IList<string> ids,
            string debankChainId,
            CancellationToken cancellationToken = default)
        {
            var result = new List<DeBankTokenData>();
            if (_settings.UseDeBankCaching)
            {
                foreach (string id in ids)
                {
                    var tokenData = await _cacheProviderService.GetFromCacheAsync<DeBankTokenData>($"{debankChainId}_{id}_debank_token_data").ConfigureAwait(false);
                    if (tokenData != null)
                    {
                        result.Add(tokenData);
                    }
                }
            }

            foreach (var idsBatch in ids.Except(result.Select(x => x.Id)).Batch(100))
            {
                if (idsBatch != null)
                {
                    var response = await _client.GetAsync($"/v1/user/list_by_ids?ids={string.Join(',', idsBatch)}&chain_id={debankChainId}", cancellationToken).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var tokensResult = await response.Content.ReadFromJsonAsync<List<DeBankTokenData>>(
                                 new JsonSerializerOptions
                                 {
                                     Converters = { new DecimalStringConverter() }
                                 }, cancellationToken: cancellationToken).ConfigureAwait(false)
                             ?? throw new CustomException("Can't get hold tokens data from API.");

                    if (_settings.UseDeBankCaching)
                    {
                        foreach (var tokenResult in tokensResult)
                        {
                            await _cacheProviderService.SetCacheAsync($"{debankChainId}_{tokenResult.Id}_debank_token_data", tokenResult, new DistributedCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow = _settings.DeBankTokensDataCacheDuration
                            }).ConfigureAwait(false);
                        }
                    }

                    result.AddRange(tokensResult);
                }
            }

            return await Result<IList<DeBankTokenData>>.SuccessAsync(result, $"Successfully fetched tokens data by DeBank API for given token address for {debankChainId} blockchain.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}