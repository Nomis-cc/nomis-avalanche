// ------------------------------------------------------------------------------------------------------
// <copyright file="ChainanalysisService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http.Json;

using Microsoft.Extensions.Options;
using Nomis.Chainanalysis.Interfaces;
using Nomis.Chainanalysis.Interfaces.Responses;
using Nomis.Chainanalysis.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.Chainanalysis
{
    /// <inheritdoc cref="IChainanalysisService"/>
    internal sealed class ChainanalysisService :
        IChainanalysisService,
        ITransientService,
        IDisposable
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="ChainanalysisService"/>.
        /// </summary>
        /// <param name="settings"><see cref="ChainanalysisSettings"/>.</param>
        public ChainanalysisService(
            IOptions<ChainanalysisSettings> settings)
        {
            _client = new()
            {
                BaseAddress = new(settings.Value.ApiBaseUrl ?? "https://public.chainalysis.com/")
            };
            _client.DefaultRequestHeaders.Add("X-API-KEY", settings.Value.ApiKey);
        }

        /// <inheritdoc/>
        public async Task<Result<ChainanalysisReportsResponse>> GetWalletReportsAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new CustomException("Wallet address must be set", statusCode: HttpStatusCode.BadRequest);
            }

            string request =
                $"/api/v1/address/{address}";
            var response = await _client.GetAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NoDataException("The wallet reports not found.");
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ChainanalysisReportsResponse>()
                .ConfigureAwait(false) ?? throw new CustomException("Can't get wallet sanctions reports.");

            return await Result<ChainanalysisReportsResponse>.SuccessAsync(result, "Got wallet sanctions reports.").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}