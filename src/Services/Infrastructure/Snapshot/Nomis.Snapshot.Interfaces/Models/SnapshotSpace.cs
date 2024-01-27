// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotSpace.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Snapshot.Interfaces.Models
{
    /// <summary>
    /// Snapshot space data.
    /// </summary>
    public class SnapshotSpace
    {
        /// <summary>
        /// Space identifier.
        /// </summary>
        /// <example>yam.eth</example>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Space name.
        /// </summary>
        /// <example>Yam</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Private.
        /// </summary>
        /// <example>false</example>
        [JsonPropertyName("private")]
        public bool Private { get; set; }

        /// <summary>
        /// About.
        /// </summary>
        /// <example>Only delegated YAM may be used to vote on proposals. You can delegate to yourself or another address here: yam.finance/#/delegate</example>
        [JsonPropertyName("about")]
        public string? About { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        /// <example>ipfs://QmWe6sMgURBDA3cadz6s59MDjExHtNXXsFBvVG84PMGdhU</example>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Website.
        /// </summary>
        /// <example>https://yam.finance/</example>
        [JsonPropertyName("website")]
        public string? Website { get; set; }

        /// <summary>
        /// Twitter.
        /// </summary>
        /// <example>YamFinance</example>
        [JsonPropertyName("twitter")]
        public string? Twitter { get; set; }

        /// <summary>
        /// Network.
        /// </summary>
        /// <example>1</example>
        [JsonPropertyName("network")]
        public string? Network { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        /// <example>YAM</example>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }
}