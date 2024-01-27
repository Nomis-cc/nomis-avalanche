// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiAssetData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi asset data.
    /// </summary>
    public class DeFiAssetData
    {
        /// <summary>
        /// Balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal? Balance { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Total balance in USD.
        /// </summary>
        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        /// <summary>
        /// Asset token data.
        /// </summary>
        [JsonPropertyName("asset")]
        public DeFiAssetTokenData? Asset { get; set; }
    }
}