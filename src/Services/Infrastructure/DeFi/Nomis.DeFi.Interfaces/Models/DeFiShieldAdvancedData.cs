// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiShieldAdvancedData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi shield advanced data.
    /// </summary>
    public class DeFiShieldAdvancedData
    {
        /// <summary>
        /// Contracts responses passed to shield query.
        /// </summary>
        [JsonPropertyName("contracts")]
        public IList<DeFiShieldData> Contracts { get; set; } = new List<DeFiShieldData>();
    }
}