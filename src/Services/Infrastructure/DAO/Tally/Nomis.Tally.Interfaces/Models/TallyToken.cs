// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyToken.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally token data.
    /// </summary>
    public class TallyToken
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Onchain name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Onchain symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Token contract type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Number of decimal places included in BigInt values.
        /// </summary>
        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }
    }
}