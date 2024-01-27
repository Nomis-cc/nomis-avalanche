// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileOwnerMetadata.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect Protocol profile owner metadata.
    /// </summary>
    public class CyberConnectProfileOwnerMetadata
    {
        /// <summary>
        /// Labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public IList<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// Project interaction stats.
        /// </summary>
        [JsonPropertyName("projectInteractionStats")]
        public IList<CyberConnectProjectInteractionStats> ProjectInteractionStats { get; set; } = new List<CyberConnectProjectInteractionStats>();
    }
}