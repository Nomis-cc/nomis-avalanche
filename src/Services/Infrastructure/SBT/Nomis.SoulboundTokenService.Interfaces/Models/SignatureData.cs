// ------------------------------------------------------------------------------------------------------
// <copyright file="SignatureData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Utils.Enums;

namespace Nomis.SoulboundTokenService.Interfaces.Models
{
    /// <summary>
    /// Signature data.
    /// </summary>
    public class SignatureData
    {
        /// <summary>
        /// Score value.
        /// </summary>
        /// <example>1414</example>
        [JsonPropertyName("score")]
        public ushort Score { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("calculation_model")]
        public ScoringCalculationModel CalculationModel { get; set; }

        /// <summary>
        /// To address.
        /// </summary>
        /// <example>0x0000000000000000000000000000000000000000</example>
        [JsonPropertyName("to")]
        public string? To { get; set; }

        /// <summary>
        /// Nonce.
        /// </summary>
        [JsonPropertyName("nonce")]
        public ulong Nonce { get; set; }

        /// <summary>
        /// Time to the verifying deadline.
        /// </summary>
        [JsonPropertyName("deadline")]
        public ulong Deadline { get; set; }

        /// <summary>
        /// Token metadata IPFS URL.
        /// </summary>
        [JsonPropertyName("metadata_url")]
        public string? MetadataUrl { get; set; }

        /// <summary>
        /// Blockchain id in which the SBT will be minted.
        /// </summary>
        /// <example>56</example>
        [JsonPropertyName("chain_id")]
        public string? MintChainId { get; set; }

        /// <summary>
        /// Referral code.
        /// </summary>
        /// <example>anon</example>
        [JsonPropertyName("referral_code")]
        public string? ReferralCode { get; set; } = "anon";

        /// <summary>
        /// Referrer code.
        /// </summary>
        [JsonPropertyName("referrer_code")]
        public string? ReferrerCode { get; set; } = "nomis";
    }
}