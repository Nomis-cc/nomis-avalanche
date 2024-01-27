// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect Protocol profile data.
    /// </summary>
    public class CyberConnectProfileData
    {
        /// <summary>
        /// Follower Count.
        /// </summary>
        /// <example>80</example>
        [JsonPropertyName("followerCount")]
        public int FollowerCount { get; set; }

        /// <summary>
        /// Subscriber Count.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("subscriberCount")]
        public int SubscriberCount { get; set; }

        /// <summary>
        /// Profile metadata.
        /// </summary>
        [JsonPropertyName("metadataInfo")]
        public CyberConnectProfileMetadataData? MetadataInfo { get; set; }

        /// <summary>
        /// Profile owner.
        /// </summary>
        [JsonPropertyName("owner")]
        public CyberConnectProfileOwnerData? Owner { get; set; }

        /// <summary>
        /// Is primary.
        /// </summary>
        /// <example>true</example>
        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Profile external metadata.
        /// </summary>
        [JsonPropertyName("externalMetadataInfo")]
        public CyberConnectProfileExternalMetadataData? ExternalMetadataInfo { get; set; }
    }
}