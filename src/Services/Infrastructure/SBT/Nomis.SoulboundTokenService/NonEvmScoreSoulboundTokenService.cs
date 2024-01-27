// ------------------------------------------------------------------------------------------------------
// <copyright file="NonEvmScoreSoulboundTokenService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;

using Microsoft.Extensions.Options;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.SoulboundTokenService.Interfaces.Models;
using Nomis.SoulboundTokenService.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Responses;
using Nomis.SoulboundTokenService.Settings;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Enums;
using Nomis.Utils.Wrapper;

namespace Nomis.SoulboundTokenService
{
    /// <inheritdoc cref="INonEvmScoreSoulboundTokenService"/>
    internal sealed class NonEvmScoreSoulboundTokenService :
        INonEvmScoreSoulboundTokenService,
        ITransientService
    {
        private readonly ScoreSoulboundTokenSettings _settings;
        private readonly HttpClient _tokenImageClient;
        private readonly HttpClient _signatureClient;

        /// <summary>
        /// Initialize <see cref="NonEvmScoreSoulboundTokenService"/>.
        /// </summary>
        /// <param name="settings"><see cref="ScoreSoulboundTokenSettings"/>.</param>
        public NonEvmScoreSoulboundTokenService(
            IOptions<ScoreSoulboundTokenSettings> settings)
        {
            _settings = settings.Value;
            _tokenImageClient = new HttpClient
            {
                BaseAddress = new Uri(settings.Value.TokenImageApiBaseUrl ?? "https://images.nomis.cc/")
            };
            _signatureClient = new HttpClient
            {
                BaseAddress = new Uri(settings.Value.SignatureApiBaseUrl ?? "http://app-starknet-sign-svc-1:3000/")
            };
        }

        /// <inheritdoc />
        public Result<NFTSignature> GetMigrationSignature(
            ScoreMigrationRequest request)
        {
            return Result<NFTSignature>.Fail(new() { Signature = null }, "Get migration signature: Verifying the contract signature for non EVM-compatible blockchains is not implemented yet.");
        }

        /// <inheritdoc />
        public Result<NFTSignature> GetSoulboundTokenSignature(
            ScoreSoulboundTokenRequest request)
        {
            return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Verifying the contract signature for non EVM-compatible blockchains is not implemented yet.");
        }

        /// <inheritdoc />
        public async Task<Result<SignatureResponse?>> GetNonEvmSoulboundTokenSignatureAsync(
            ScoreSoulboundTokenRequest request)
        {
            if (request.ScoreChainId == 111127 || request.ScoreChainId == 111128) // Starknet
            {
                string signatureRequest = "/generateSignature";
                var response = await _signatureClient.PostAsJsonAsync(signatureRequest, new SignatureRequest
                {
                    Data = new SignatureData
                    {
                        Nonce = request.Nonce,
                        CalculationModel = request.CalculationModel,
                        Deadline = request.Deadline,
                        MetadataUrl = request.MetadataUrl,
                        MintChainId = request.MintChainId.ToString(),
                        ReferralCode = request.ReferralCode,
                        ReferrerCode = request.ReferrerCode,
                        Score = request.Score,
                        To = request.To
                    }
                }).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<SignatureResponse>().ConfigureAwait(false);
                return await Result<SignatureResponse?>.SuccessAsync(result, "Got soulbound token signature.").ConfigureAwait(false);
            }

            return await Result<SignatureResponse?>.FailAsync(new(), "Get token signature: Verifying the contract signature for non EVM-compatible blockchains is not implemented yet.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<NFTImage>> GetSoulboundTokenImageAsync(
            ScoreSoulboundTokenImageRequest request)
        {
            if (request.Score < 3)
            {
                return await Result<NFTImage>.FailAsync("The score must be greater than 0.").ConfigureAwait(false);
            }

            string imageRequest = $"/api/score?&address={request.Address}&type={request.Type?.ToLower()}&score={request.Score}&size={request.Size}&chainId={request.ChainId}";
            var response = await _tokenImageClient.GetAsync(imageRequest).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await Result<NFTImage>.SuccessAsync(
                new NFTImage
                {
                    ImageType = response.Content.Headers.ContentType?.MediaType,
                    Image = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false)
                }, "Got soulbound token image.").ConfigureAwait(false);
        }

        public async Task<Result<NFTMetadata>> GetSoulboundTokenMetadataAsync(NFTMetadataRequest request)
        {
            var result = new NFTMetadata
            {
                Image = request.Image,
                Attributes = request.Attributes,
                Name = _settings.MetadataTokenName,
                Description = _settings.MetadataTokenDescription,
                ExternalUrl = _settings.MetadataTokenExternalUrl
            };

            return await Result<NFTMetadata>.SuccessAsync(result, "Successfully got token metadata.").ConfigureAwait(false);
        }

        public async Task<Result<NFTMetadata>> GetONFTSoulboundTokenMetadataAsync(NFTMetadataRequest request)
        {
            return await Result<NFTMetadata>.FailAsync(new NFTMetadata(), "Get token metadata: for non EVM-compatible blockchains is not implemented yet.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<IList<ScoringCalculationModelData>>> GetScoringCalculationModelsAsync()
        {
            var result = new List<ScoringCalculationModelData>();
            result.AddRange(Enum.GetValues<ScoringCalculationModel>().Select(x => new ScoringCalculationModelData
            {
                Model = x
            }));

            return await Result<IList<ScoringCalculationModelData>>.SuccessAsync(result, "Successfully got calculation models.").ConfigureAwait(false);
        }
    }
}