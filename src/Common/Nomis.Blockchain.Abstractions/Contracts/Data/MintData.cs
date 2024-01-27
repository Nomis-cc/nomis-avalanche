// ------------------------------------------------------------------------------------------------------
// <copyright file="MintData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Utils.Enums;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Mint data.
    /// </summary>
    public sealed class MintData
    {
        /// <summary>
        /// Initialize <see cref="MintData"/>.
        /// </summary>
        /// <param name="signature">Soulbound token signature.</param>
        /// <param name="mintedScore">Nomis minted Score in range of [0; 10000].</param>
        /// <param name="calculationModel"><see cref="ScoringCalculationModel"/>.</param>
        /// <param name="deadline">Verifying deadline block timestamp.</param>
        /// <param name="metadataUrl">Token metadata IPFS URL.</param>
        /// <param name="chainId">The blockchain id in which the score was calculated.</param>
        /// <param name="mintedChain">The blockchain descriptor in which the score will be minted.</param>
        /// <param name="referralCode">Referral code.</param>
        /// <param name="referrerCode">Referrer code.</param>
        /// <param name="onftMetadataUrl">ONFT token metadata IPFS URL.</param>
        public MintData(
            string? signature,
            ushort mintedScore,
            ScoringCalculationModel calculationModel,
            ulong deadline,
            string? metadataUrl,
            ulong chainId,
            IBlockchainDescriptor mintedChain,
            string referralCode = "anon",
            string referrerCode = "nomis",
            string? onftMetadataUrl = null)
        {
            Signature = signature;
            MintedScore = mintedScore;
            CalculationModel = calculationModel;
            Deadline = deadline;
            MetadataUrl = metadataUrl;
            ReferralCode = referralCode;
            ReferrerCode = referrerCode;
            ChainId = chainId;
            MintedChain = mintedChain;
            ONFTMetadataUrl = onftMetadataUrl;
        }

        /// <summary>
        /// Soulbound token signature.
        /// </summary>
        public string? Signature { get; init; }

        /// <summary>
        /// Nomis minted Score in range of [0; 10000].
        /// </summary>
        public ushort MintedScore { get; init; }

        /// <inheritdoc cref="ScoringCalculationModel"/>
        public ScoringCalculationModel CalculationModel { get; init; }

        /// <summary>
        /// Verifying deadline block timestamp.
        /// </summary>
        public ulong Deadline { get; init; }

        /// <summary>
        /// Token metadata IPFS URL.
        /// </summary>
        public string? MetadataUrl { get; init; }

        /// <summary>
        /// The blockchain id in which the score was calculated.
        /// </summary>
        public ulong ChainId { get; init; }

        /// <summary>
        /// Referral code.
        /// </summary>
        public string? ReferralCode { get; init; }

        /// <summary>
        /// Referrer code.
        /// </summary>
        public string? ReferrerCode { get; init; }

        /// <summary>
        /// ONFT token metadata IPFS URL.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ONFTMetadataUrl { get; init; }

        /// <summary>
        /// The blockchain descriptor in which the score will be minted.
        /// </summary>
        public IBlockchainDescriptor MintedChain { get; init; }
    }
}