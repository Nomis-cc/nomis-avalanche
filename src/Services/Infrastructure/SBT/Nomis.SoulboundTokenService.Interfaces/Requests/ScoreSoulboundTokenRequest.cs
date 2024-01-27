// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoreSoulboundTokenRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Enums;

namespace Nomis.SoulboundTokenService.Interfaces.Requests
{
    /// <inheritdoc cref="INFTRequest"/>
    public class ScoreSoulboundTokenRequest :
        INFTRequest
    {
        /// <inheritdoc />
        /// <example>0x0000000000000000000000000000000000000000</example>
        public string? To { get; set; }

        /// <inheritdoc />
        /// <example>0</example>
        public ulong Nonce { get; set; }

        /// <summary>
        /// Blockchain id in which the SBT will be minted.
        /// </summary>
        /// <example>56</example>
        public ulong MintChainId { get; set; }

        /// <summary>
        /// NFT common data.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public NFTCommonData? NFTCommonData { get; set; }

        /// <inheritdoc />
        /// <example>0</example>
        public ulong Deadline { get; set; }

        /// <inheritdoc />
        public string? MetadataUrl { get; set; }

        /// <summary>
        /// Referral code.
        /// </summary>
        /// <example>anon</example>
        public string? ReferralCode { get; set; } = "anon";

        /// <summary>
        /// Referrer code.
        /// </summary>
        public string? ReferrerCode { get; set; } = "nomis";

        /// <summary>
        /// The score type.
        /// </summary>
        /// <example>0</example>
        public ScoreType ScoreType { get; set; }

        /// <summary>
        /// Score value.
        /// </summary>
        /// <example>1414</example>
        public ushort Score { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        /// <example>0</example>
        public ScoringCalculationModel CalculationModel { get; set; }

        /// <summary>
        /// Blockchain id in which the score was calculated.
        /// </summary>
        /// <example>56</example>
        public ulong? ScoreChainId { get; set; }
    }
}