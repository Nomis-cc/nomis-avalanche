// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentNftTokenExternalData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent NFT token external data.
    /// </summary>
    public class CovalentNftTokenExternalData
    {
        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        [JsonPropertyName("image")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Image { get; set; }

        /// <summary>
        /// Image with 256px resolution.
        /// </summary>
        [JsonPropertyName("image_256")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Image256 { get; set; }

        /// <summary>
        /// Image with 512px resolution.
        /// </summary>
        [JsonPropertyName("image_512")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Image512 { get; set; }

        /// <summary>
        /// Image with 1024px resolution.
        /// </summary>
        [JsonPropertyName("image_1024")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Image1024 { get; set; }

        /// <summary>
        /// Animation URL.
        /// </summary>
        [JsonPropertyName("animation_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AnimationUrl { get; set; }

        /// <summary>
        /// External URL.
        /// </summary>
        [JsonPropertyName("external_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExternalUrl { get; set; }

        /// <summary>
        /// NFT attributes.
        /// </summary>
        [JsonPropertyName("attributes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<CovalentNftAttributeData>? Attributes { get; set; }
    }
}