// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectLikeData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect like data.
    /// </summary>
    public class CyberConnectLikeData
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Author.
        /// </summary>
        [JsonPropertyName("author")]
        public string? Author { get; set; }

        /// <summary>
        /// Handle.
        /// </summary>
        [JsonPropertyName("handle")]
        public string? Handle { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Body.
        /// </summary>
        [JsonPropertyName("body")]
        public string? Body { get; set; }

        /// <summary>
        /// Digest.
        /// </summary>
        [JsonPropertyName("digest")]
        public string? Digest { get; set; }

        /// <summary>
        /// Arweave Tx hash.
        /// </summary>
        [JsonPropertyName("arweaveTxHash")]
        public string? ArweaveTxHash { get; set; }

        /// <summary>
        /// Created at.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Updated at.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Like count.
        /// </summary>
        /// <example>1</example>
        [JsonPropertyName("likeCount")]
        public int LikeCount { get; set; }

        /// <summary>
        /// Dislike count.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("dislikeCount")]
        public int DislikeCount { get; set; }
    }
}