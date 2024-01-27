// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyGovernor.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally governor data.
    /// </summary>
    public class TallyGovernor
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Governor contract type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// The minimum amount of votes (total or for depending on type) that are currently required to pass a proposal.
        /// </summary>
        [JsonPropertyName("quorum")]
        public string? Quorum { get; set; }

        /// <summary>
        /// List of related tokens used to operate this contract.
        /// </summary>
        /// <remarks>
        /// Most governors only have one.
        /// </remarks>
        [JsonPropertyName("tokens")]
        public IList<TallyToken> Tokens { get; set; } = new List<TallyToken>();

        /// <summary>
        /// Tally name of the governor contract.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Tally slug used for this governance: tally.xyz/gov/[slug].
        /// </summary>
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
    }
}