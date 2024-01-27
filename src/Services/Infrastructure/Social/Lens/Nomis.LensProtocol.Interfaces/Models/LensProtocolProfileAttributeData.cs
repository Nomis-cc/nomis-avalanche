// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolProfileAttributeData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.LensProtocol.Interfaces.Models
{
    /// <summary>
    /// Lens Protocol profile attribute data.
    /// </summary>
    public class LensProtocolProfileAttributeData
    {
        /// <summary>
        /// Display type.
        /// </summary>
        [JsonPropertyName("displayType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayType { get; set; }

        /// <summary>
        /// Trait type.
        /// </summary>
        [JsonPropertyName("traitType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TraitType { get; set; }

        /// <summary>
        /// Key.
        /// </summary>
        [JsonPropertyName("key")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Key { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Value { get; set; }
    }
}