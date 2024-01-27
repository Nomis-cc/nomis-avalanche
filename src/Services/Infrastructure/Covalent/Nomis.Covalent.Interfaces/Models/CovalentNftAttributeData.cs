// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentNftAttributeData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent NFT attribute data.
    /// </summary>
    public class CovalentNftAttributeData
    {
        /// <summary>
        /// Value.
        /// </summary>
        [JsonPropertyName("value")]
        public object Value { get; set; } = null!;

        /// <summary>
        /// Trait type.
        /// </summary>
        [JsonPropertyName("trait_type")]
        public string TraitType { get; set; } = null!;

        /// <summary>
        /// Display type.
        /// </summary>
        [JsonPropertyName("display_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayType { get; set; }
    }
}