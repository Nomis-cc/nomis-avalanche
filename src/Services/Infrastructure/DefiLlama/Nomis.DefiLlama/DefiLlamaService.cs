// ------------------------------------------------------------------------------------------------------
// <copyright file="DefiLlamaService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nethereum.Util;
using Nomis.CacheProviderService.Interfaces;
using Nomis.DefiLlama.Interfaces;
using Nomis.DefiLlama.Interfaces.Models;
using Nomis.DefiLlama.Interfaces.Responses;
using Nomis.DefiLlama.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Converters;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.DefiLlama
{
    /// <inheritdoc cref="IDefiLlamaService"/>
    internal sealed class DefiLlamaService :
        IDefiLlamaService,
        ISingletonService,
        IDisposable
    {
        private readonly DefiLlamaSettings _settings;
        private readonly LlamafolioSettings _llamafolioSettings;
        private readonly HttpClient _client;
        private readonly HttpClient _llamafolioClient;
        private readonly ICacheProviderService _cacheProviderService;
        private readonly ILogger<DefiLlamaService> _logger;

        /// <summary>
        /// Initialize <see cref="DefiLlamaService"/>.
        /// </summary>
        /// <param name="settings"><see cref="DefiLlamaSettings"/>.</param>
        /// <param name="llamafolioSettings"><see cref="LlamafolioSettings"/>.</param>
        /// <param name="client">DefiLlama <see cref="HttpClient"/>.</param>
        /// <param name="llamafolioClient">Llamafolio <see cref="HttpClient"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DefiLlamaService(
            DefiLlamaSettings settings,
            LlamafolioSettings llamafolioSettings,
            HttpClient client,
            HttpClient llamafolioClient,
            ICacheProviderService cacheProviderService,
            ILogger<DefiLlamaService> logger)
        {
            _settings = settings;
            _llamafolioSettings = llamafolioSettings;
            _client = client;
            _llamafolioClient = llamafolioClient;
            _cacheProviderService = cacheProviderService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Result<LlamafolioTokenBalancesResponse>> TokenBalancesAsync(
            string address,
            CancellationToken cancellationToken = default)
        {
            var result = _llamafolioSettings.UseTokenBalancesCaching
                ? await _cacheProviderService.GetFromCacheAsync<LlamafolioTokenBalancesResponse>($"{address}_llamafolio_token_balances").ConfigureAwait(false)
                : null;

            if (result == null)
            {
                var response = await _llamafolioClient.GetAsync($"/balances/{address}", cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadFromJsonAsync<LlamafolioTokenBalancesResponse>(
                             new JsonSerializerOptions
                             {
                                 Converters = { new DecimalStringConverter() }
                             }, cancellationToken: cancellationToken).ConfigureAwait(false)
                         ?? throw new CustomException("Can't get account token balances from API.");

                if (_llamafolioSettings.UseTokenBalancesCaching)
                {
                    await _cacheProviderService.SetCacheAsync($"{address}_llamafolio_token_balances", result, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _llamafolioSettings.TokenBalancesCacheDuration
                    }).ConfigureAwait(false);
                }
            }

            return await Result<LlamafolioTokenBalancesResponse>.SuccessAsync(result, $"Successfully fetched token balances by Llamafolio API for given wallet address.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DefiLlamaTokensPriceResponse?> TokensPriceAsync(
            IList<string> tokensId,
            int searchWidthInHours = 6,
            IList<TokenPriceData>? pricesData = null)
        {
            var result = new DefiLlamaTokensPriceResponse();
            var knownTokensPrices = new Dictionary<string, TokenPriceData>();
            foreach (string tokenId in tokensId.Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                var tokensPriceData = _settings.TokenPriceUseCaching
                    ? await _cacheProviderService.GetFromCacheAsync<TokenPriceData>($"{tokenId}_price").ConfigureAwait(false)
                    : null;
                if (tokensPriceData != null)
                {
                    knownTokensPrices.TryAdd(tokenId, tokensPriceData);
                }
            }

            if (pricesData?.Any() == true)
            {
                string? prefix = tokensId.FirstOrDefault()?.Split(":").FirstOrDefault();
                if (prefix != null)
                {
                    foreach (var priceData in pricesData)
                    {
                        if (!string.IsNullOrWhiteSpace(priceData.Id) && priceData.Id?.StartsWith("0x", StringComparison.OrdinalIgnoreCase) == true && priceData.Price > 0)
                        {
                            knownTokensPrices.TryAdd($"{prefix}:{priceData.Id}", priceData);
                        }
                    }
                }
            }

            string tokensIds = string.Join(',', tokensId.Where(t => !string.IsNullOrWhiteSpace(t) && !knownTokensPrices.ContainsKey(t)));
            if (tokensIds.Any())
            {
                bool needSplit = false;
                HttpResponseMessage? response = null;
                try
                {
                    response = await _client.GetAsync($"/prices/current/{tokensIds}?searchWidth={searchWidthInHours}h").ConfigureAwait(false);
                }
                catch
                {
                    needSplit = true;
                }

                if (response?.IsSuccessStatusCode != true || needSplit)
                {
                    _logger.LogWarning("{Service}: There is an error while fetching tokens prices with status code {StatusCode}: {Response}", nameof(DefiLlamaService), response?.StatusCode, response == null ? string.Empty : await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    if (response?.StatusCode is HttpStatusCode.RequestEntityTooLarge or HttpStatusCode.RequestUriTooLong or HttpStatusCode.BadRequest)
                    {
                        // send requests by batches
                        var tokensPriceResponses = new List<DefiLlamaTokensPriceResponse>();
                        foreach (var tokensIdBatch in tokensId
                                     .Where(t => !string.IsNullOrWhiteSpace(t) && !knownTokensPrices.ContainsKey(t))
                                     .Batch(_settings.RequestTokensBatchSize))
                        {
                            string tokensIdsBatch = string.Join(',', tokensIdBatch);
                            var responseBatch = await _client.GetAsync($"/prices/current/{tokensIdsBatch}?searchWidth={searchWidthInHours}h").ConfigureAwait(false);
                            if (!responseBatch.IsSuccessStatusCode)
                            {
                                _logger.LogError("{Service}: There is an error while fetching tokens prices with status code {StatusCode}: {Response}", nameof(DefiLlamaService), responseBatch.StatusCode, await responseBatch.Content.ReadAsStringAsync().ConfigureAwait(false));
                                return default;
                            }

                            var resultBatch = await responseBatch.Content.ReadFromJsonAsync<DefiLlamaTokensPriceResponse>().ConfigureAwait(false);
                            if (resultBatch != null)
                            {
                                tokensPriceResponses.Add(resultBatch);
                            }
                        }

                        result.TokensPrices = tokensPriceResponses.SelectMany(x => x.TokensPrices).Union(knownTokensPrices).DistinctBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    result = await response.Content.ReadFromJsonAsync<DefiLlamaTokensPriceResponse>().ConfigureAwait(false);
                    if (result != null)
                    {
                        result.TokensPrices = result.TokensPrices.Union(knownTokensPrices).DistinctBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                    }
                }
            }
            else
            {
                result.TokensPrices = knownTokensPrices;
            }

            foreach (var tokenPriceData in result?.TokensPrices ?? new Dictionary<string, TokenPriceData>())
            {
                // add cached prices for concrete tokens
                if (tokenPriceData.Value is { Price: > 0, LastPriceDateTime: not null })
                {
                    await _cacheProviderService.SetCacheAsync($"{tokenPriceData.Key}_price", tokenPriceData.Value, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _settings.TokenPriceCacheDuration
                    }).ConfigureAwait(false);
                }
            }

            return result;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _client.Dispose();
            _llamafolioClient.Dispose();
        }
    }
}