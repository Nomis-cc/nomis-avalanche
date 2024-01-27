// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileMetadataData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect Protocol profile metadata.
    /// </summary>
    public class CyberConnectProfileMetadataData
    {
        /// <summary>
        /// Avatar.
        /// </summary>
        /// <example>https://dm2zb8bwza29x.cloudfront.net/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/0x0000000000000000000000000000000000000000000000000000000000000762.png</example>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Bio.
        /// </summary>
        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        /// <summary>
        /// Display name.
        /// </summary>
        /// <example>ryan</example>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }
}