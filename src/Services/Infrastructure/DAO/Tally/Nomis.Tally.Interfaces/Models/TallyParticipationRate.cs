// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyParticipationRate.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally participation rate.
    /// </summary>
    public class TallyParticipationRate
    {
        /// <summary>
        /// Number of votes on the last 10 proposals on this Governor.
        /// </summary>
        [JsonPropertyName("recentVoteCount")]
        public int RecentVoteCount { get; set; }

        /// <summary>
        /// 10 or the number of proposals on this Governor if less than 10.
        /// </summary>
        [JsonPropertyName("recentProposalCount")]
        public int RecentProposalCount { get; set; }
    }
}