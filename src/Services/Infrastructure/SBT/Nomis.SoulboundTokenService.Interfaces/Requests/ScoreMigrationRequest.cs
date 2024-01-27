// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoreMigrationRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Enums;

namespace Nomis.SoulboundTokenService.Interfaces.Requests
{
    /// <summary>
    /// Score migration request.
    /// </summary>
    public class ScoreMigrationRequest
    {
        /// <summary>
        /// To address.
        /// </summary>
        /// <example>0x0000000000000000000000000000000000000000</example>
        public string? To { get; set; }

        /// <summary>
        /// Nonce.
        /// </summary>
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

        /// <summary>
        /// Time to the verifying deadline.
        /// </summary>
        /// <example>0</example>
        public ulong Deadline { get; set; }

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
    }
}