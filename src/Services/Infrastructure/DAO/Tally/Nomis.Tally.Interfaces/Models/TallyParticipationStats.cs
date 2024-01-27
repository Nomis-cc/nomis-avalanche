// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyParticipationStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally participation stats data.
    /// </summary>
    public class TallyParticipationStats
    {
        /// <summary>
        /// Current overall number of delegations include those that delegate zero voting power.
        /// </summary>
        [JsonPropertyName("delegationCount")]
        public int DelegationCount { get; set; }

        /// <summary>
        /// Current overall number of delegations that delegate non-zero voting power.
        /// </summary>
        [JsonPropertyName("activeDelegationCount")]
        public int ActiveDelegationCount { get; set; }

        /// <summary>
        /// Number of proposals created by this Account.
        /// </summary>
        [JsonPropertyName("createdProposalsCount")]
        public int CreatedProposalsCount { get; set; }

        /// <summary>
        /// Number of votes made by this Account.
        /// </summary>
        [JsonPropertyName("voteCount")]
        public int VoteCount { get; set; }

        /// <summary>
        /// Number of votes on the last 10 proposals if there are at least ten made on this contract.
        /// </summary>
        /// <remarks>
        /// If there are not at least 10 proposals the amount of proposals is provided as recentProposalCount.
        /// </remarks>
        [JsonPropertyName("recentParticipationRate")]
        public TallyParticipationRate? RecentParticipationRate { get; set; }

        /// <summary>
        /// Current number of tokens owned by this Account.
        /// </summary>
        [JsonPropertyName("tokenBalance")]
        public string? TokenBalance { get; set; }
    }
}