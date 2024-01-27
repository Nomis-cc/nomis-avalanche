// ------------------------------------------------------------------------------------------------------
// <copyright file="EvmScoreSoulboundTokenService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text;

using Microsoft.Extensions.Options;
using Nethereum.ABI.EIP712;
using Nethereum.Signer;
using Nethereum.Signer.EIP712;
using Nethereum.Util;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.SoulboundTokenService.Interfaces.Models;
using Nomis.SoulboundTokenService.Interfaces.Requests;
using Nomis.SoulboundTokenService.Models;
using Nomis.SoulboundTokenService.Settings;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Enums;
using Nomis.Utils.Wrapper;

// ReSharper disable InconsistentNaming
namespace Nomis.SoulboundTokenService
{
    /// <inheritdoc cref="IEvmScoreSoulboundTokenService"/>
    internal sealed class EvmScoreSoulboundTokenService :
        IEvmScoreSoulboundTokenService,
        ITransientService,
        IDisposable
    {
        private readonly ScoreSoulboundTokenSettings _settings;
        private readonly HttpClient _tokenImageClient;

        /// <summary>
        /// Initialize <see cref="EvmScoreSoulboundTokenService"/>.
        /// </summary>
        /// <param name="settings"><see cref="ScoreSoulboundTokenSettings"/>.</param>
        public EvmScoreSoulboundTokenService(
            IOptions<ScoreSoulboundTokenSettings> settings)
        {
            _settings = settings.Value;
            _tokenImageClient = new HttpClient
            {
                BaseAddress = new Uri(settings.Value.TokenImageApiBaseUrl ?? "https://images.nomis.cc/")
            };
        }

        /// <inheritdoc />
        public Result<NFTSignature> GetMigrationSignature(
            ScoreMigrationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NFTCommonData?.ContractAddress))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, $"Get token signature for {request.To}: Contract address should be set.");
            }

            if (string.IsNullOrWhiteSpace(request.To))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Wallet address should be set.");
            }

            if (string.IsNullOrWhiteSpace(request.ReferralCode))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, $"Get token signature for {request.To}: Referral code should be set.");
            }

            if (string.IsNullOrWhiteSpace(request.ReferrerCode))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, $"Get token signature for {request.To}: Referrer code should be set.");
            }

            string? privateKey =
                _settings.TokenData[request.ScoreType].ContainsKey(request.MintChainId)
                    ? _settings.TokenData[request.ScoreType][request.MintChainId].SignerWalletPrivateKey
                      ?? _settings.TokenData[request.ScoreType][0].SignerWalletPrivateKey
                    : _settings.TokenData[request.ScoreType][0].SignerWalletPrivateKey;

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Signer-wallet private key is not set.");
            }

            var signer = new Eip712TypedDataSigner();
            var key = new EthECKey(privateKey);

            string? migrationSignature;
            string? version =
                request.NFTCommonData?.Version ??
                (_settings.TokenData[request.ScoreType].ContainsKey(request.MintChainId)
                    ? _settings.TokenData[request.ScoreType][request.MintChainId].Version
                      ?? _settings.TokenData[request.ScoreType][0].Version
                    : _settings.TokenData[request.ScoreType][0].Version);

            TypedData<Domain> migrateTypedData;

            switch (version)
            {
                case "0.8":
                default:
                    var migrateMessage = new MigrateMessage
                    {
                        Deadline = request.Deadline,
                        Nonce = request.Nonce,
                        To = request.To,
                        ReferralCode = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.ReferralCode!)),
                        ReferrerCode = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.ReferrerCode!))
                    };

                    migrateTypedData = GetScoreTypedDefinition(request.NFTCommonData, request.ScoreType, request.MintChainId, nameof(MigrateMessage), typeof(Domain), typeof(MigrateMessage));
                    migrationSignature = signer.SignTypedDataV4(migrateMessage, migrateTypedData, key);

                    break;
            }

            return Result<NFTSignature>.Success(new() { Signature = migrationSignature }, $"Got migration signature for {request.To}.");
        }

        /// <inheritdoc />
        public Result<NFTSignature> GetSoulboundTokenSignature(
            ScoreSoulboundTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NFTCommonData?.ContractAddress))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Contract address should be set.");
            }

            if (string.IsNullOrWhiteSpace(request.To))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Wallet address should be set.");
            }

            if (string.IsNullOrWhiteSpace(request.MetadataUrl))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Metadata URL should be set.");
            }

            string? privateKey =
                _settings.TokenData[request.ScoreType].ContainsKey(request.MintChainId)
                    ? _settings.TokenData[request.ScoreType][request.MintChainId].SignerWalletPrivateKey
                      ?? _settings.TokenData[request.ScoreType][0].SignerWalletPrivateKey
                    : _settings.TokenData[request.ScoreType][0].SignerWalletPrivateKey;

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                return Result<NFTSignature>.Fail(new() { Signature = null }, "Get token signature: Signer-wallet private key is not set.");
            }

            var signer = new Eip712TypedDataSigner();
            var key = new EthECKey(privateKey);

            string? signature;
            string? version =
                request.NFTCommonData?.Version ??
                  (_settings.TokenData[request.ScoreType].ContainsKey(request.MintChainId)
                      ? _settings.TokenData[request.ScoreType][request.MintChainId].Version
                        ?? _settings.TokenData[request.ScoreType][0].Version
                      : _settings.TokenData[request.ScoreType][0].Version);

            TypedData<Domain> typedData;
            switch (version)
            {
                case "0.1":
                    var scoreMessageV1 = new SetScoreMessageV1
                    {
                        Deadline = request.Deadline,
                        Nonce = request.Nonce,
                        Score = request.Score,
                        To = request.To
                    };

                    typedData = GetScoreTypedDefinition(request.NFTCommonData, request.ScoreType, request.MintChainId, nameof(SetScoreMessage), typeof(Domain), typeof(SetScoreMessageV1));
                    signature = signer.SignTypedDataV4(scoreMessageV1, typedData, key);
                    break;
                case "0.5":
                    var scoreMessageV5 = new SetScoreMessageV5
                    {
                        Deadline = request.Deadline,
                        Nonce = request.Nonce,
                        Score = request.Score,
                        CalculationModel = (ushort)request.CalculationModel,
                        To = request.To,
                        MetadataUrl = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.MetadataUrl!))
                    };

                    typedData = GetScoreTypedDefinition(request.NFTCommonData, request.ScoreType, request.MintChainId, nameof(SetScoreMessage), typeof(Domain), typeof(SetScoreMessageV5));
                    signature = signer.SignTypedDataV4(scoreMessageV5, typedData, key);
                    break;
                case "0.8":
                case "1.0":
                    var scoreMessageV8 = new SetScoreMessageV8
                    {
                        Deadline = request.Deadline,
                        Nonce = request.Nonce,
                        Score = request.Score,
                        CalculationModel = (ushort)request.CalculationModel,
                        To = request.To,
                        MetadataUrl = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.MetadataUrl!)),
                        ChainId = request.ScoreChainId ?? request.MintChainId,
                        ReferralCode = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.ReferralCode!)),
                        ReferrerCode = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.ReferrerCode!))
                    };

                    typedData = GetScoreTypedDefinition(request.NFTCommonData, request.ScoreType, request.MintChainId, nameof(SetScoreMessage), typeof(Domain), typeof(SetScoreMessageV8));
                    signature = signer.SignTypedDataV4(scoreMessageV8, typedData, key);

                    break;
                case "0.6":
                case "0.7":
                default:
                    var scoreMessageV6 = new SetScoreMessageV6
                    {
                        Deadline = request.Deadline,
                        Nonce = request.Nonce,
                        Score = request.Score,
                        CalculationModel = (ushort)request.CalculationModel,
                        To = request.To,
                        MetadataUrl = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(request.MetadataUrl!)),
                        ChainId = request.ScoreChainId ?? request.MintChainId
                    };

                    typedData = GetScoreTypedDefinition(request.NFTCommonData, request.ScoreType, request.MintChainId, nameof(SetScoreMessage), typeof(Domain), typeof(SetScoreMessageV6));
                    signature = signer.SignTypedDataV4(scoreMessageV6, typedData, key);
                    break;
            }

            return Result<NFTSignature>.Success(new() { Signature = signature }, "Got soulbound token signature.");
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

        /// <inheritdoc />
        public async Task<Result<NFTMetadata>> GetSoulboundTokenMetadataAsync(
            NFTMetadataRequest request)
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

        /// <inheritdoc />
        public async Task<Result<NFTMetadata>> GetONFTSoulboundTokenMetadataAsync(
            NFTMetadataRequest request)
        {
            var result = new NFTMetadata
            {
                Image = request.Image,
                Attributes = request.Attributes,
                Name = request.Name ?? _settings.ONFTMetadataTokenName,
                Description = request.Description ?? _settings.ONFTMetadataTokenDescription,
                ExternalUrl = request.ExternalUrl ?? _settings.ONFTMetadataTokenExternalUrl
            };

            return await Result<NFTMetadata>.SuccessAsync(result, "Successfully got ONFT token metadata.").ConfigureAwait(false);
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

        /// <inheritdoc />
        public void Dispose()
        {
            _tokenImageClient.Dispose();
        }

        private TypedData<Domain> GetScoreTypedDefinition(
            NFTCommonData? nftData,
            ScoreType scoreType,
            ulong chainId,
            string primaryType,
            params Type[] types)
        {
            return new()
            {
                Domain = new()
                {
                    Name = nftData?.TokenName ??
                        (_settings.TokenData[scoreType].ContainsKey(chainId)
                            ? _settings.TokenData[scoreType][chainId].TokenName
                              ?? _settings.TokenData[scoreType][0].TokenName
                            : _settings.TokenData[scoreType][0].TokenName),
                    Version = nftData?.Version ??
                        (_settings.TokenData[scoreType].ContainsKey(chainId)
                            ? _settings.TokenData[scoreType][chainId].Version
                              ?? _settings.TokenData[scoreType][0].Version
                            : _settings.TokenData[scoreType][0].Version),
                    ChainId = new BigInteger(chainId),
                    VerifyingContract = nftData?.ContractAddress
                },
                Types = MemberDescriptionFactory.GetTypesMemberDescription(types),
                PrimaryType = primaryType
            };
        }
    }
}