// ------------------------------------------------------------------------------------------------------
// <copyright file="CoingeckoService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

using Microsoft.Extensions.Options;
using Nomis.Coingecko.Interfaces;
using Nomis.Coingecko.Interfaces.Models;
using Nomis.Coingecko.Settings;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Coingecko
{
    /// <inheritdoc cref="ICoingeckoService"/>
    internal sealed class CoingeckoService :
        ICoingeckoService,
        ISingletonService,
        IDisposable
    {
        private readonly HttpClient _client;

        public CoingeckoService(
            IOptions<CoingeckoSettings> coingeckoOptions)
        {
            _client = new()
            {
                BaseAddress = new(coingeckoOptions.Value.ApiBaseUrl ?? "https://api.coingecko.com/")
            };
        }

        /// <inheritdoc />
        public async Task<CoingeckoTokenContractDataResponse?> GetTokenDataAsync(
            string assetPlatformId,
            string tokenContractAddress)
        {
            var response = await _client.GetAsync($"/api/v3/coins/{assetPlatformId}/contract/{tokenContractAddress}").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CoingeckoTokenContractDataResponse?>().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<decimal> GetUsdBalanceAsync(decimal balance, string tokenId)
        {
            if (balance == default)
            {
                return 0;
            }

            var response = await _client.GetAsync($"/api/v3/simple/price?ids={tokenId}&vs_currencies=usd").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            string? usdString = (await response.Content.ReadFromJsonAsync<JsonObject>().ConfigureAwait(false))?[tokenId]?["usd"]?.ToString();
            if (usdString?.Contains("e-", StringComparison.OrdinalIgnoreCase) == true)
            {
                string[] usdSplittedString = usdString.Split('e');
                usdString = usdSplittedString.FirstOrDefault();
                decimal.TryParse(usdString, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out decimal usd);
                string? decimalsString = usdSplittedString.LastOrDefault();
                decimalsString = decimalsString?[1..];
                if (int.TryParse(decimalsString, out int decimals))
                {
                    for (int i = 0; i < decimals; i++)
                    {
                        usd /= 10;
                    }
                }

                return balance * usd;
            }
            else
            {
                decimal.TryParse(usdString, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out decimal usd);
                return balance * usd;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}