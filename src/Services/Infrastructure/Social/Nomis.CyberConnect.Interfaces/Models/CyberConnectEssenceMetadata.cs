// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectEssenceMetadata.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect essence metadata.
    /// </summary>
    public class CyberConnectEssenceMetadata
    {
        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Animation URL.
        /// </summary>
        [JsonPropertyName("animation_url")]
        public string? AnimationUrl { get; set; }

        /// <summary>
        /// App Id.
        /// </summary>
        [JsonPropertyName("app_id")]
        public string? AppId { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// External Url.
        /// </summary>
        [JsonPropertyName("external_url")]
        public string? ExternalUrl { get; set; }

        /// <summary>
        /// Issue date.
        /// </summary>
        [JsonPropertyName("issue_date")]
        public string? IssueDate { get; set; }

        /// <summary>
        /// Lang.
        /// </summary>
        [JsonPropertyName("lang")]
        public string? Lang { get; set; }

        /// <summary>
        /// Metadata Id.
        /// </summary>
        [JsonPropertyName("metadata_id")]
        public string? MetadataId { get; set; }
    }
}