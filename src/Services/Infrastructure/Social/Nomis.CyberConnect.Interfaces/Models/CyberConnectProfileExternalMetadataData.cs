// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileExternalMetadataData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect profile external metadata.
    /// </summary>
    public class CyberConnectProfileExternalMetadataData
    {
        /// <summary>
        /// Type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Verified Twitter ID.
        /// </summary>
        [JsonPropertyName("verifiedTwitterID")]
        public string? VerifiedTwitterId { get; set; }

        /// <summary>
        /// Personal.
        /// </summary>
        [JsonPropertyName("personal")]
        public CyberConnectProfileExternalMetadataPersonalData? Personal { get; set; }
    }
}