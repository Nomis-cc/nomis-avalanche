// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyParticipation.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally participation data.
    /// </summary>
    public class TallyParticipation
    {
        /// <summary>
        /// Governor.
        /// </summary>
        [JsonPropertyName("governor")]
        public TallyGovernor? Governor { get; set; }

        /// <summary>
        /// Votes made by the account on the governor.
        /// </summary>
        [JsonPropertyName("votes")]
        public IList<TallyVote> Votes { get; set; } = new List<TallyVote>();

        /// <summary>
        /// Proposals created by this account.
        /// </summary>
        [JsonPropertyName("proposals")]
        public IList<TallyProposal> Proposals { get; set; } = new List<TallyProposal>();

        /// <summary>
        /// Aggregations of account activity in this governor.
        /// </summary>
        [JsonPropertyName("stats")]
        public TallyParticipationStats? Stats { get; set; }
    }
}