// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiAssetBalances.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi asset balances data
    /// </summary>
    public class DeFiAssetBalances
    {
        /// <summary>
        /// Total balance in USD.
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Asset data.
        /// </summary>
        [JsonPropertyName("assets")]
        public IList<DeFiAssetData> Assets { get; set; } = new List<DeFiAssetData>();
    }
}