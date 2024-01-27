// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotVote.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Snapshot.Interfaces.Models
{
    /// <summary>
    /// Snapshot vote.
    /// </summary>
    public class SnapshotVote
    {
        /// <summary>
        /// Vote identifier.
        /// </summary>
        /// <example>0x0424e435d91edd5f8ef38c48e7c895bcef492fa428c56337bdfc50811f59b0d4</example>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Space data.
        /// </summary>
        [JsonPropertyName("space")]
        public SnapshotSpace? Space { get; set; }

        /// <summary>
        /// IPFS.
        /// </summary>
        /// <example>bafkreihb2llqowo2uh6paipofst5hdeps3gh6tqacsldo2cpi6gcpwuo4q</example>
        [JsonPropertyName("ipfs")]
        public string? Ipfs { get; set; }

        /// <summary>
        /// Voter.
        /// </summary>
        /// <example>0x653d63E4F2D7112a19f5Eb993890a3F27b48aDa5</example>
        [JsonPropertyName("voter")]
        public string? Voter { get; set; }

        /// <summary>
        /// Vp.
        /// </summary>
        /// <example>540062.46908221</example>
        [JsonPropertyName("vp")]
        public decimal? Vp { get; set; }

        /// <summary>
        /// Vp by strategy.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// [ 540062.46908221 ]
        /// ]]>
        /// </example>
        [JsonPropertyName("vp_by_strategy")]
        public IList<decimal>? VpByStrategy { get; set; } = new List<decimal>();

        /// <summary>
        /// Choice.
        /// </summary>
        /// <example>null</example>
        [JsonPropertyName("choice")]
        public object? Choice { get; set; }
    }
}