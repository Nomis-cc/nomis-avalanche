// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectSubscribingProfileData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect subscribing profile data.
    /// </summary>
    public class CyberConnectSubscribingProfileData
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Profile id.
        /// </summary>
        [JsonPropertyName("profileID")]
        public ulong ProfileId { get; set; }

        /// <summary>
        /// Handle.
        /// </summary>
        [JsonPropertyName("handle")]
        public string? Handle { get; set; }

        /// <summary>
        /// Owner.
        /// </summary>
        [JsonPropertyName("owner")]
        public CyberConnectProfileOwnerData? Owner { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Is primary.
        /// </summary>
        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }
    }
}