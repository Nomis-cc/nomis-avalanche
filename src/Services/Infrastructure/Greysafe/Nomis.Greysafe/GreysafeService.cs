// ------------------------------------------------------------------------------------------------------
// <copyright file="GreysafeService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http.Json;

using Microsoft.Extensions.Options;
using Nomis.Greysafe.Interfaces;
using Nomis.Greysafe.Interfaces.Responses;
using Nomis.Greysafe.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.Greysafe
{
    /// <inheritdoc cref="IGreysafeService"/>
    internal sealed class GreysafeService :
        IGreysafeService,
        ITransientService,
        IDisposable
    {
        private readonly GreysafeSettings _settings;
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="GreysafeService"/>.
        /// </summary>
        /// <param name="settings"><see cref="GreysafeSettings"/>.</param>
        public GreysafeService(
            IOptions<GreysafeSettings> settings)
        {
            _settings = settings.Value;
            _client = new()
            {
                BaseAddress = new(settings.Value.ApiBaseUrl ?? "https://greysafe.com/")
            };
        }

        /// <inheritdoc/>
        public async Task<Result<GreysafeReportsResponse>> GetWalletReportsAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new CustomException("Wallet address must be set", statusCode: HttpStatusCode.BadRequest);
            }

            string request =
                $"/api/wallets/{address}";
            var response = await _client.GetAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NoDataException("The wallet reports not found.");
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GreysafeReportsResponse>().ConfigureAwait(false) ?? throw new CustomException("Can't get wallet scam reports.");

            return await Result<GreysafeReportsResponse>.SuccessAsync(result, "Got wallet scam reports.").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}