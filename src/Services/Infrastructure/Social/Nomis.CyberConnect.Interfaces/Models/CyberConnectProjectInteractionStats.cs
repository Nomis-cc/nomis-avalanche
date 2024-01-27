// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProjectInteractionStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect project interaction stats.
    /// </summary>
    public class CyberConnectProjectInteractionStats
    {
        /// <summary>
        /// First interaction.
        /// </summary>
        [JsonPropertyName("firstInteraction")]
        public DateTime? FirstInteraction { get; set; }

        /// <summary>
        /// Last interaction.
        /// </summary>
        [JsonPropertyName("lastInteraction")]
        public DateTime? LastInteraction { get; set; }

        /// <summary>
        /// Num received.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("numReceived")]
        public int NumReceived { get; set; }

        /// <summary>
        /// Num sent.
        /// </summary>
        /// <example>92</example>
        [JsonPropertyName("numSent")]
        public int NumSent { get; set; }

        /// <summary>
        /// Project.
        /// </summary>
        /// <example>Uniswap User</example>
        [JsonPropertyName("project")]
        public string? Project { get; set; }

        /// <summary>
        /// Tx count.
        /// </summary>
        /// <example>92</example>
        [JsonPropertyName("txCount")]
        public int TxCount { get; set; }
    }
}