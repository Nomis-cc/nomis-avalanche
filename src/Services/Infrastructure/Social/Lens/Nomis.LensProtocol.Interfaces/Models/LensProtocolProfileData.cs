// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolProfileData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.LensProtocol.Interfaces.Models
{
    /// <summary>
    /// LensProtocol Protocol profile data.
    /// </summary>
    public class LensProtocolProfileData
    {
        /// <summary>
        /// Id.
        /// </summary>
        /// <example>0x01b8ad</example>
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Bio.
        /// </summary>
        [JsonPropertyName("bio")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Bio { get; set; }

        /// <summary>
        /// Attributes.
        /// </summary>
        [JsonPropertyName("attributes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<LensProtocolProfileAttributeData>? Attributes { get; set; }

        /// <summary>
        /// Follow NFT address.
        /// </summary>
        [JsonPropertyName("followNftAddress")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FollowNftAddress { get; set; }

        /// <summary>
        /// Metadata.
        /// </summary>
        [JsonPropertyName("metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Metadata { get; set; }

        /// <summary>
        /// Handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Handle { get; set; }

        /// <summary>
        /// Owned by.
        /// </summary>
        [JsonPropertyName("ownedBy")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OwnedBy { get; set; }

        /// <summary>
        /// Stats.
        /// </summary>
        [JsonPropertyName("stats")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public LensProtocolProfileStatsData? Stats { get; set; }
    }
}