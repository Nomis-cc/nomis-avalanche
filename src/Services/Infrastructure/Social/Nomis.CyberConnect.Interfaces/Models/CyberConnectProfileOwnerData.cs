// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileOwnerData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect Protocol profile owner data.
    /// </summary>
    public class CyberConnectProfileOwnerData
    {
        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Metadata.
        /// </summary>
        [JsonPropertyName("metadata")]
        public CyberConnectProfileOwnerMetadata? Metadata { get; set; }
    }
}