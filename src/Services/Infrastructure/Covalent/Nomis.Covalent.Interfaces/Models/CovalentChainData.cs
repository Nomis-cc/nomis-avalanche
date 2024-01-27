// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentChainData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent blockchain data.
    /// </summary>
    public class CovalentChainData
    {
        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Blockchain id.
        /// </summary>
        [JsonPropertyName("chain_id")]
        public string ChainId { get; set; } = null!;

        /// <summary>
        /// Is testnet.
        /// </summary>
        [JsonPropertyName("is_testnet")]
        public bool IsTestnet { get; set; }

        /// <summary>
        /// Label.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = null!;

        /// <summary>
        /// Category label.
        /// </summary>
        [JsonPropertyName("category_label")]
        public string CategoryLabel { get; set; } = null!;

        /// <summary>
        /// Logo URL.
        /// </summary>
        [JsonPropertyName("logo_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LogoUrl { get; set; }

        /// <summary>
        /// Black logo URL.
        /// </summary>
        [JsonPropertyName("black_logo_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BlackLogoUrl { get; set; }

        /// <summary>
        /// White logo URL.
        /// </summary>
        [JsonPropertyName("white_logo_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? WhiteLogoUrl { get; set; }

        /// <summary>
        /// Is appchain.
        /// </summary>
        [JsonPropertyName("is_appchain")]
        public bool IsAppchain { get; set; }

        /// <summary>
        /// Appchain of.
        /// </summary>
        [JsonPropertyName("appchain_of")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CovalentChainData? AppchainOf { get; set; }
    }
}