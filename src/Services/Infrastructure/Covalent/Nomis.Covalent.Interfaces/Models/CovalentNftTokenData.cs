// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentNftTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent NFT token data.
    /// </summary>
    public class CovalentNftTokenData
    {
        /// <summary>
        /// The token's id.
        /// </summary>
        [JsonPropertyName("token_id")]
        public string TokenId { get; set; } = null!;

        /// <summary>
        /// The token balance.
        /// </summary>
        [JsonPropertyName("token_balance")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TokenBalance { get; set; }

        /// <summary>
        /// The token URL.
        /// </summary>
        [JsonPropertyName("token_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TokenUrl { get; set; }

        /// <summary>
        /// A list of supported standard ERC interfaces, eg: ERC20 and ERC721.
        /// </summary>
        [JsonPropertyName("supports_erc")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<string>? SupportsErc { get; set; }

        /// <summary>
        /// The original minter.
        /// </summary>
        [JsonPropertyName("original_owner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OriginalOwner { get; set; }

        /// <summary>
        /// The current owner.
        /// </summary>
        [JsonPropertyName("owner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Owner { get; set; }

        /// <summary>
        /// External data.
        /// </summary>
        [JsonPropertyName("external_data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CovalentNftTokenExternalData? ExternalData { get; set; }
    }
}