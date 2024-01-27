// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotProposal.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Utils.Extensions;

// ReSharper disable InconsistentNaming
namespace Nomis.Snapshot.Interfaces.Models
{
    /// <summary>
    /// Snapshot proposal data.
    /// </summary>
    public class SnapshotProposal
    {
        /// <summary>
        /// Proposal identifier.
        /// </summary>
        /// <example>0xdfd3a7f56f68d3153d0d39ce4f6431213312e9a66b669ffcd646b257a469fa8a</example>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        /// <example>Yam Core Development - Phase #2</example>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Body.
        /// </summary>
        /// <example># Summary\nThis proposal is to approve for E to continue on the developments priorities as mentioned...</example>
        [JsonPropertyName("body")]
        public string? Body { get; set; }

        /// <summary>
        /// Choices.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// [ "Approve", "Decline" ]
        /// ]]>
        /// </example>
        [JsonPropertyName("choices")]
        public IList<string>? Choices { get; set; } = new List<string>();

        /// <summary>
        /// Start.
        /// </summary>
        /// <example>1661976000</example>
        [JsonPropertyName("start")]
        public long Start { get; set; }

        /// <summary>
        /// Start in UTC.
        /// </summary>
        /// <example>2022-08-31T20:00:00</example>
        public DateTime StartUTC => Start.ToString().ToDateTime();

        /// <summary>
        /// End.
        /// </summary>
        /// <example>1662318000</example>
        [JsonPropertyName("end")]
        public long End { get; set; }

        /// <summary>
        /// End in UTC.
        /// </summary>
        /// <example>2022-09-04T19:00:00</example>
        public DateTime EndUTC => End.ToString().ToDateTime();

        /// <summary>
        /// Snapshot.
        /// </summary>
        /// <example>15453764</example>
        [JsonPropertyName("snapshot")]
        public string? Snapshot { get; set; }

        /// <summary>
        /// State.
        /// </summary>
        /// <example>closed</example>
        [JsonPropertyName("state")]
        public string? State { get; set; }

        /// <summary>
        /// Author.
        /// </summary>
        /// <example>0x9Ebc8AD4011C7f559743Eb25705CCF5A9B58D0bc</example>
        [JsonPropertyName("author")]
        public string? Author { get; set; }

        /// <summary>
        /// Ipfs.
        /// </summary>
        /// <example>bafkreidy6u5krdft5skldadpaszcgufygz23rgwrizk2kwr27wjuptpska</example>
        [JsonPropertyName("ipfs")]
        public string? Ipfs { get; set; }

        /// <summary>
        /// Created.
        /// </summary>
        /// <example>1662048229</example>
        [JsonPropertyName("created")]
        public long Created { get; set; }

        /// <summary>
        /// Created in UTC.
        /// </summary>
        /// <example>2022-09-01T16:03:49</example>
        public DateTime CreatedUTC => Created.ToString().ToDateTime();

        /// <summary>
        /// Type.
        /// </summary>
        /// <example>single-choice</example>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Discussion.
        /// </summary>
        [JsonPropertyName("discussion")]
        public string? Discussion { get; set; }

        /// <summary>
        /// Link.
        /// </summary>
        /// <example>https://snapshot.org/#/yam.eth/proposal/0xdfd3a7f56f68d3153d0d39ce4f6431213312e9a66b669ffcd646b257a469fa8a</example>
        [JsonPropertyName("link")]
        public string? Link { get; set; }

        /// <summary>
        /// Scores.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// [ 1167.1987148307396, 540062.46908221 ]
        /// ]]>
        /// </example>
        [JsonPropertyName("scores")]
        public IList<decimal>? Scores { get; set; } = new List<decimal>();

        /// <summary>
        /// Votes.
        /// </summary>
        /// <example>7</example>
        [JsonPropertyName("votes")]
        public int Votes { get; set; }

        /// <summary>
        /// Space data.
        /// </summary>
        [JsonPropertyName("space")]
        public SnapshotSpace? Space { get; set; }
    }
}