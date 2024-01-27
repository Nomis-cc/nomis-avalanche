// ------------------------------------------------------------------------------------------------------
// <copyright file="MintedScoreData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Minted score data.
    /// </summary>
    public class MintedScoreData
    {
        /// <summary>
        /// Token id.
        /// </summary>
        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; } = null!;

        /// <summary>
        /// Score owner.
        /// </summary>
        [JsonPropertyName("owner")]
        public string Owner { get; set; } = null!;

        /// <summary>
        /// Score value.
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        [JsonPropertyName("calculationModel")]
        public ScoringCalculationModel CalculationModel { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonPropertyName("transactionHash")]
        public string TransactionHash { get; set; } = null!;

        /// <summary>
        /// Block number.
        /// </summary>
        [JsonPropertyName("blockNumber")]
        public string BlockNumber { get; set; } = null!;

        /// <summary>
        /// Block timestamp.
        /// </summary>
        [JsonPropertyName("blockTimestamp")]
        public string BlockTimestamp { get; set; } = null!;

        /// <summary>
        /// Token metadata URL.
        /// </summary>
        [JsonPropertyName("metadataUrl")]
        public string? MetadataUrl { get; set; }

        /// <summary>
        /// Referral code.
        /// </summary>
        [JsonPropertyName("referralCode")]
        public string? ReferralCode { get; set; }

        /// <summary>
        /// Referrer code.
        /// </summary>
        [JsonPropertyName("referrerCode")]
        public string? ReferrerCode { get; set; }
    }
}