// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyVoteStat.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally vote stat data.
    /// </summary>
    public class TallyVoteStat
    {
        /// <summary>
        /// Vote Choice.
        /// </summary>
        [JsonPropertyName("support")]
        public string? Support { get; set; }

        /// <summary>
        /// Total weight (voting power) for this Choice/SupportType.
        /// </summary>
        [JsonPropertyName("weight")]
        public string? Weight { get; set; }

        /// <summary>
        /// Number of distinct votes cast for this Choice/SupportType.
        /// </summary>
        [JsonPropertyName("votes")]
        public string? Votes { get; set; }

        /// <summary>
        /// Percent of total weight cast in this Proposal.
        /// </summary>
        [JsonPropertyName("percent")]
        public float Percent { get; set; }
    }
}