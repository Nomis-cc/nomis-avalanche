// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiChainData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi chain data.
    /// </summary>
    public class DeFiChainData
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Absolute chain id.
        /// </summary>
        [JsonPropertyName("absoluteChainId")]
        public string? AbsoluteChainId { get; set; }

        /// <summary>
        /// Abbr.
        /// </summary>
        [JsonPropertyName("abbr")]
        public string? Abbr { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}