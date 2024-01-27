// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiIssueData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi issue data.
    /// </summary>
    public class DeFiIssueData
    {
        /// <summary>
        /// Internal id.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Vulnerability id.
        /// </summary>
        [JsonPropertyName("registryId")]
        public string? RegistryId { get; set; }

        /// <summary>
        /// Vulnerability severity level.
        /// </summary>
        /// <remarks>
        /// Supported levels (from most to least dangerous): Critical, High, Medium, Low, Informational, Optimisation.
        /// </remarks>
        [JsonPropertyName("impact")]
        public string? Impact { get; set; }

        /// <summary>
        /// Vulnerability name.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Vulnerability description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Vulnerability category.
        /// </summary>
        /// <remarks>
        /// Supported categories: Authorisation control, Byte-Code Safety, Control Flow, Defi-Specific, ERC Standarts, Private data safety, Solidity Coding Best Practices.
        /// </remarks>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Vulnerability additional data.
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }
}