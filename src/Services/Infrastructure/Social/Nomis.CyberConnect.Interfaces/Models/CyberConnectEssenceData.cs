// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectEssenceData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect essence data.
    /// </summary>
    public class CyberConnectEssenceData
    {
        /// <summary>
        /// Essence Id.
        /// </summary>
        [JsonPropertyName("essenceID")]
        public int EssenceId { get; set; }

        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Metadata.
        /// </summary>
        [JsonPropertyName("metadata")]
        public CyberConnectEssenceMetadata? Metadata { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Token URI.
        /// </summary>
        [JsonPropertyName("tokenURI")]
        public string? TokenURI { get; set; }

        /// <summary>
        /// Created by.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public CyberConnectEssenceCreator? CreatedBy { get; set; }
    }
}