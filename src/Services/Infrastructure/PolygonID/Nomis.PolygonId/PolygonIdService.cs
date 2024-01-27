// ------------------------------------------------------------------------------------------------------
// <copyright file="PolygonIdService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.PolygonId.Interfaces;
using Nomis.PolygonId.Interfaces.Contracts;
using Nomis.PolygonId.Interfaces.Credentials;
using Nomis.PolygonId.Interfaces.PolygonIdIssuerNode;
using Nomis.PolygonId.Settings;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Wrapper;

namespace Nomis.PolygonId
{
    /// <inheritdoc cref="IPolygonIdService"/>
    internal sealed class PolygonIdService :
        IPolygonIdService,
        ISingletonService
    {
        private readonly ILogger<PolygonIdService> _logger;
        private readonly PolygonIdSettings _polygonIdSettings;

        public PolygonIdService(
            IOptions<PolygonIdSettings> polygonIdOptions,
            ILogger<PolygonIdService> logger)
        {
            _logger = logger;
            _polygonIdSettings = polygonIdOptions.Value;
            var httpClient = new HttpClient
            {
                BaseAddress = new(polygonIdOptions.Value.ApiBaseUrl ?? "https://polygon.nomis.cc/")
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{polygonIdOptions.Value.IssuerBasicAuthUsername}:{polygonIdOptions.Value.IssuerBasicAuthPassword}"))}");
            Client = new PolygonIdIssuerClient(httpClient);
        }

        public IPolygonIdIssuerClient Client { get; }

        /// <inheritdoc />
        public async Task<Result<DIDData?>> CreateClaimAndGetQrAsync<TWalletStatsRequest, TWalletStats,
            TTransactionIntervalData>(
            TWalletStatsRequest request, ushort mintedScore, TWalletStats walletStats, long expiration, ulong scoredChain, CancellationToken cancellationToken, string? multichainBlockchains = null)
            where TWalletStatsRequest : WalletStatsRequest, IWalletPolygonIdRequest
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData
        {
            if (request.UseDIDStorage)
            {
                try
                {
                    var nomisScoreCredential = new NomisScoreCredential
                    {
                        Id = request.DID,
                        Score = mintedScore,
                        CalculationModel = (int)request.CalculationModel,
                        MintChain = (ulong)request.MintChain,
                        ScoreType = (int)request.ScoreType,
                        ScoredChain = scoredChain
                    };

                    if (!string.IsNullOrWhiteSpace(multichainBlockchains))
                    {
                        nomisScoreCredential.MultichainBlockchains = multichainBlockchains;
                    }

                    if (string.IsNullOrWhiteSpace(nomisScoreCredential.MultichainBlockchains))
                    {
                        nomisScoreCredential.MultichainBlockchains = "-";
                    }

                    var createClaimResponse = await Client.CreateClaimAsync(
                        _polygonIdSettings.IssuerDid,
                        new CreateClaimRequest
                        {
                            CredentialSchema = _polygonIdSettings.NomisCredentialSchema,
                            Expiration = expiration,
                            CredentialSubject = nomisScoreCredential,
                            Type = _polygonIdSettings.NomisCredentialType
                        },
                        cancellationToken).ConfigureAwait(false);

                    var oldClaims = await Client.GetClaimsAsync(
                        _polygonIdSettings.IssuerDid,
                        _polygonIdSettings.NomisCredentialType,
                        null,
                        request.DID,
                        false,
                        null,
                        "scoredChain",
                        scoredChain.ToString(),
                        cancellationToken)
                        .ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(createClaimResponse.Id))
                    {
#pragma warning disable S6608 // Prefer indexing instead of "Enumerable" methods on types implementing "IList"
                        foreach (var oldClaim in oldClaims.Where(c => !new Uri(c.Id).Segments.Last().Equals(createClaimResponse.Id, StringComparison.OrdinalIgnoreCase)))
#pragma warning restore S6608 // Prefer indexing instead of "Enumerable" methods on types implementing "IList"
                        {
                            if ((int)((dynamic)oldClaim.CredentialSubject).calculationModel == (int)request.CalculationModel)
                            {
                                await Client.RevokeClaimAsync(_polygonIdSettings.IssuerDid, (long)((dynamic)oldClaim.CredentialStatus).revocationNonce, cancellationToken).ConfigureAwait(false);
                            }
                        }

                        _logger.LogInformation($"DID claim created: {createClaimResponse.Id}");
                        var claimQrCodeResponse = await Client.GetClaimQrCodeAsync(_polygonIdSettings.IssuerDid, createClaimResponse.Id, cancellationToken).ConfigureAwait(false);
                        if (claimQrCodeResponse != null)
                        {
                            return await Result<DIDData?>.SuccessAsync(new DIDData(request, createClaimResponse.Id, claimQrCodeResponse), "Successfully create the Polygon ID claim.").ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception e)
                {
                    return await Result<DIDData?>.FailAsync($"Can't create the Polygon ID claim: {e.Message}").ConfigureAwait(false);
                }
            }

            return await Result<DIDData?>.FailAsync("Can't create the Polygon ID claim.").ConfigureAwait(false);
        }
    }
}