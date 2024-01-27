// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.SoulboundTokenService.Interfaces.Contracts;
using Nomis.SoulboundTokenService.Interfaces.Responses;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Enums;
using Nomis.Utils.Wrapper;

namespace Nomis.SoulboundTokenService.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get the signed migration data signature.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="IScoreSoulboundTokenService"/>.</param>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="mintChainId">Blockchain id in which the SBT will be minted.</param>
        /// <param name="sbtData">The soulbound token common data.</param>
        /// <param name="referralCode">Referral code.</param>
        /// <param name="referrerCode">Referrer code.</param>
        /// <returns>Returns the signed data signature.</returns>
        public static async Task<Result<NFTSignature>> MigrationSignatureAsync<TWalletRequest>(
            this IScoreSoulboundTokenService service,
            TWalletRequest request,
            ulong? mintChainId,
            NFTCommonData? sbtData,
            string? referralCode = "anon",
            string? referrerCode = "nomis")
            where TWalletRequest : WalletStatsRequest
        {
            var signatureResult = await Result<NFTSignature>.FailAsync(
                new NFTSignature
                {
                    Signature = null
                }, "Get token signature: Can't get signature with empty blockchain id.").ConfigureAwait(false);
#pragma warning disable S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
            if (mintChainId != null)
#pragma warning restore S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
            {
                signatureResult = service.GetMigrationSignature(new()
                {
                    ScoreType = ScoreType.Migration,
                    To = request.Address,
                    Nonce = request.Nonce,
                    MintChainId = (ulong)mintChainId,
                    NFTCommonData = sbtData,
                    Deadline = request.Deadline,
                    ReferralCode = referralCode,
                    ReferrerCode = referrerCode
                });
            }

            return signatureResult;
        }

        /// <summary>
        /// Get the signed data signature.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="IScoreSoulboundTokenService"/>.</param>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="mintedScore">The wallet minted score.</param>
        /// <param name="mintChainId">Blockchain id in which the SBT will be minted.</param>
        /// <param name="sbtData">The soulbound token common data.</param>
        /// <param name="metadataUrl">Token metadata IPFS URL.</param>
        /// <param name="scoreChainId">Blockchain id in which the score was calculated.</param>
        /// <param name="referralCode">Referral code.</param>
        /// <param name="referrerCode">Referrer code.</param>
        /// <param name="criteria">Criteria to get signature.</param>
        /// <returns>Returns the signed data signature.</returns>
        public static async Task<Result<NFTSignature>> SignatureAsync<TWalletRequest>(
            this IScoreSoulboundTokenService service,
            TWalletRequest request,
            ushort mintedScore,
            ulong? mintChainId,
            IDictionary<ScoreType, NFTCommonData>? sbtData,
            string? metadataUrl = null,
            ulong? scoreChainId = null,
            string? referralCode = "anon",
            string? referrerCode = "nomis",
            params bool?[] criteria)
            where TWalletRequest : WalletStatsRequest
        {
            if (!request.PrepareToMint)
            {
                return await Result<NFTSignature>.FailAsync(
                    new NFTSignature
                    {
                        Signature = null
                    }, $"Can't get token signature: {nameof(request.PrepareToMint)} parameter is false.").ConfigureAwait(false);
            }

            var signatureResult = await Result<NFTSignature>.FailAsync(
                new NFTSignature
                {
                    Signature = null
                }, "Get token signature: Can't get signature without Risk adjusting score or empty blockchain id.").ConfigureAwait(false);
#pragma warning disable S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
            if (request.PrepareToMint && mintChainId != null && (!criteria.Any() || criteria.All(c => c == true)))
#pragma warning restore S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
            {
                signatureResult = service.GetSoulboundTokenSignature(new()
                {
                    Score = mintedScore,
                    ScoreType = request.ScoreType,
                    CalculationModel = request.CalculationModel,
                    To = request.Address,
                    Nonce = request.Nonce,
                    MintChainId = (ulong)mintChainId,
                    NFTCommonData = sbtData?.TryGetValue(request.ScoreType, out var value) is true ? value : null,
                    Deadline = request.Deadline,
                    MetadataUrl = metadataUrl,
                    ScoreChainId = scoreChainId,
                    ReferralCode = referralCode,
                    ReferrerCode = referrerCode
                });
            }

            return signatureResult;
        }

        /// <summary>
        /// Get the signed data signature for non-EVM blockchain.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="IScoreSoulboundTokenService"/>.</param>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="mintedScore">The wallet minted score.</param>
        /// <param name="mintChainId">Blockchain id in which the SBT will be minted.</param>
        /// <param name="sbtData">The soulbound token common data.</param>
        /// <param name="metadataUrl">Token metadata IPFS URL.</param>
        /// <param name="scoreChainId">Blockchain id in which the score was calculated.</param>
        /// <param name="referralCode">Referral code.</param>
        /// <param name="referrerCode">Referrer code.</param>
        /// <param name="criteria">Criteria to get signature.</param>
        /// <returns>Returns the signed data signature for non-EVM blockchain.</returns>
        public static async Task<Result<SignatureResponse?>> NonEvmSignatureAsync<TWalletRequest>(
            this INonEvmScoreSoulboundTokenService service,
            TWalletRequest request,
            ushort mintedScore,
            ulong? mintChainId,
            IDictionary<ScoreType, NFTCommonData>? sbtData,
            string? metadataUrl = null,
            ulong? scoreChainId = null,
            string? referralCode = "anon",
            string? referrerCode = "nomis",
            params bool?[] criteria)
            where TWalletRequest : WalletStatsRequest
        {
            if (!request.PrepareToMint)
            {
                return await Result<SignatureResponse?>.FailAsync(
                    new SignatureResponse
                    {
                        Signature = null
                    }, $"Can't get token signature: {nameof(request.PrepareToMint)} parameter is false.").ConfigureAwait(false);
            }

            var signatureResult = await Result<SignatureResponse?>.FailAsync(
                new SignatureResponse
                {
                    Signature = null
                }, "Get token signature: Can't get signature with empty blockchain id.").ConfigureAwait(false);

#pragma warning disable S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
            if (request.PrepareToMint && mintChainId != null && (!criteria.Any() || criteria.All(c => c == true)))
#pragma warning restore S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
            {
                signatureResult = await service.GetNonEvmSoulboundTokenSignatureAsync(new()
                {
                    Score = mintedScore,
                    ScoreType = request.ScoreType,
                    CalculationModel = request.CalculationModel,
                    To = request.Address,
                    Nonce = request.Nonce,
                    MintChainId = (ulong)mintChainId,
                    NFTCommonData = sbtData?.TryGetValue(request.ScoreType, out var value) is true ? value : null,
                    Deadline = request.Deadline,
                    MetadataUrl = metadataUrl,
                    ScoreChainId = scoreChainId,
                    ReferralCode = referralCode,
                    ReferrerCode = referrerCode
                }).ConfigureAwait(false);
            }

            return signatureResult;
        }
    }
}