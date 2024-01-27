// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolProfileStatsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.LensProtocol.Interfaces.Models
{
    /// <summary>
    /// Lens Protocol profile stats data.
    /// </summary>
    public class LensProtocolProfileStatsData
    {
        /// <summary>
        /// Total followers.
        /// </summary>
        [JsonPropertyName("totalFollowers")]
        public int TotalFollowers { get; set; }

        /// <summary>
        /// Total following.
        /// </summary>
        [JsonPropertyName("totalFollowing")]
        public int TotalFollowing { get; set; }

        /// <summary>
        /// Total posts.
        /// </summary>
        [JsonPropertyName("totalPosts")]
        public int TotalPosts { get; set; }

        /// <summary>
        /// Total comments.
        /// </summary>
        [JsonPropertyName("totalComments")]
        public int TotalComments { get; set; }

        /// <summary>
        /// Total mirrors.
        /// </summary>
        [JsonPropertyName("totalMirrors")]
        public int TotalMirrors { get; set; }

        /// <summary>
        /// Total publications.
        /// </summary>
        [JsonPropertyName("totalPublications")]
        public int TotalPublications { get; set; }

        /// <summary>
        /// Total collects.
        /// </summary>
        [JsonPropertyName("totalCollects")]
        public int TotalCollects { get; set; }
    }
}