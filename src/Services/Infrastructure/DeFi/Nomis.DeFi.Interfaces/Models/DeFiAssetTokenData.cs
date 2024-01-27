// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiAssetTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi asset token data.
    /// </summary>
    public class DeFiAssetTokenData
    {
        /// <summary>
        /// Token contract address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// De.Fi chain internal id.
        /// </summary>
        [JsonPropertyName("chainId")]
        public ulong ChainId { get; set; }

        /// <summary>
        /// Token name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Token display name.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Token symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Token icon.
        /// </summary>
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        /// <summary>
        /// Token decimals.
        /// </summary>
        [JsonPropertyName("decimals")]
        public int? Decimals { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}