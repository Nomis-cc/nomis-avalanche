// ------------------------------------------------------------------------------------------------------
// <copyright file="NFTTrait.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Utils.Contracts.NFT
{
    /// <summary>
    /// NFT trait.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class NFTTrait
    {
        /// <summary>
        /// Display type.
        /// </summary>
        [JsonPropertyName("display_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayType { get; set; }

        /// <summary>
        /// The trait type.
        /// </summary>
        [JsonPropertyName("trait_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TraitType { get; set; }

        /// <summary>
        /// The trait value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Value { get; set; }
    }
}