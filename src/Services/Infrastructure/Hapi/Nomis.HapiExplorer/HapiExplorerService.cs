// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiExplorerService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Nethereum.Contracts.Services;
using Nethereum.Contracts.Standards.ENS;
using Nethereum.JsonRpc.Client;
using Nomis.HapiExplorer.Converters;
using Nomis.HapiExplorer.Interfaces;
using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.HapiExplorer.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.HapiExplorer
{
    /// <inheritdoc cref="IHapiExplorerService"/>
    internal sealed class HapiExplorerService :
        IHapiExplorerService,
        ITransientService,
        IDisposable
    {
        private readonly HapiExplorerSettings _settings;
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="HapiExplorerService"/>.
        /// </summary>
        /// <param name="settings"><see cref="HapiExplorerSettings"/>.</param>
        public HapiExplorerService(
            IOptions<HapiExplorerSettings> settings)
        {
            _settings = settings.Value;
            _client = new()
            {
                BaseAddress = new(settings.Value.ApiBaseUrl ?? "https://explorer-api.hapi.one/")
            };
        }

        /// <inheritdoc/>
        public async Task<Result<HapiProxyRiskScoreResponse>> GetWalletRiskScoreAsync(string network, string address)
        {
            if (string.IsNullOrWhiteSpace(network))
            {
                throw new CustomException("Network must be set", statusCode: HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new CustomException("Wallet address must be set", statusCode: HttpStatusCode.BadRequest);
            }

            if (address.EndsWith(".eth", StringComparison.CurrentCultureIgnoreCase))
            {
                address = await new ENSService(new EthApiContractService(new RpcClient(new Uri(_settings.BlockchainProviderUrl ?? string.Empty)))).ResolveAddressAsync(address).ConfigureAwait(false);
            }

            string request =
                $"/check/{network}/{address}";
            var response = await _client.GetAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NoDataException("The wallet risk score not found.");
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<HapiProxyRiskScoreResponse>(new JsonSerializerOptions
            {
                Converters =
                {
                    new HapiProxyCaseStatusConverter(),
                    new HapiProxyReporterRoleConverter(),
                    new HapiProxyCategoryConverter()
                }
            }).ConfigureAwait(false) ?? throw new CustomException("Can't get risk score.");

            return await Result<HapiProxyRiskScoreResponse>.SuccessAsync(result, "Got wallet risk score.").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}