// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyProposal.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally proposal data.
    /// </summary>
    public class TallyProposal
    {
        /// <summary>
        /// Chain Scoped onchain Proposal ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Proposal title: usually first line of description.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Proposal description onchain.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// First block when you can cast a vote, also the time when quorum is established.
        /// </summary>
        [JsonPropertyName("start")]
        public TallyBlock? Start { get; set; }

        /// <summary>
        /// Last block when you can cast a vote.
        /// </summary>
        [JsonPropertyName("end")]
        public TallyBlock? End { get; set; }

        /// <summary>
        /// Account that created this proposal.
        /// </summary>
        [JsonPropertyName("proposer")]
        public TallyAccount? Proposer { get; set; }

        /// <summary>
        /// Summary of voting by vote choice.
        /// </summary>
        [JsonPropertyName("voteStats")]
        public IList<TallyVoteStat> VoteStats { get; set; } = new List<TallyVoteStat>();
    }
}