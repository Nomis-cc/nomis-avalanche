// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileExternalMetadataPersonalOrganizationData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect profile external metadata personal organization data.
    /// </summary>
    public class CyberConnectProfileExternalMetadataPersonalOrganizationData
    {
        /// <summary>
        /// Id.
        /// </summary>
        /// <example>1439094074103898115</example>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <example>CyberConnectHQ</example>
        [JsonPropertyName("handle")]
        public string? Handle { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        /// <example>CyberConnect</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        /// <example>https://pbs.twimg.com/profile_images/1478199976429637632/oKM4rxSV.jpg</example>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
    }
}