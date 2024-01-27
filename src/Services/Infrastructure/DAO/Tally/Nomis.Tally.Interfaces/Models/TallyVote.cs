// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyVote.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally vote data.
    /// </summary>
    public class TallyVote
    {
        /// <summary>
        /// Proposal and voter concatenated id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Vote choice made by voter.
        /// </summary>
        [JsonPropertyName("support")]
        public string? Support { get; set; }

        /// <summary>
        /// Weight of the vote.
        /// </summary>
        /// <remarks>
        /// Typically total delegated voting power of voter at proposal voting start block.
        /// </remarks>
        [JsonPropertyName("weight")]
        public string? Weight { get; set; }

        /// <summary>
        /// Transaction vote was cast in.
        /// </summary>
        [JsonPropertyName("transaction")]
        public TallyTransaction? Transaction { get; set; }

        /// <summary>
        /// Optional reason for vote choice provided by the voter.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Proposal on which vote was cast.
        /// </summary>
        [JsonPropertyName("proposal")]
        public TallyProposal? Proposal { get; set; }
    }
}