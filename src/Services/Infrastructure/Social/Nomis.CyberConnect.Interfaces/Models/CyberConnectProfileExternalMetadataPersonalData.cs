// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileExternalMetadataPersonalData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect profile external metadata personal data.
    /// </summary>
    public class CyberConnectProfileExternalMetadataPersonalData
    {
        /// <summary>
        /// Verified discord id.
        /// </summary>
        [JsonPropertyName("verifiedDiscordID")]
        public string? VerifiedDiscordId { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        /// <example>Cofounder</example>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Organization.
        /// </summary>
        /// <example>Cofounder</example>
        [JsonPropertyName("organization")]
        public CyberConnectProfileExternalMetadataPersonalOrganizationData? Organization { get; set; }
    }
}