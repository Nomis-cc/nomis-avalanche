// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyCase.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.HapiExplorer.Interfaces.Enums;

namespace Nomis.HapiExplorer.Interfaces.Models
{
    /// <summary>
    /// HAPI Proxy case.
    /// </summary>
    public class HapiProxyCase
    {
        /// <summary>
        /// Case ID.
        /// </summary>
        /// <example>7</example>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Brief case description.
        /// </summary>
        /// <example>Case number 7</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Case status.
        /// </summary>
        /// <example>Closed</example>
        [JsonPropertyName("status")]
        public HapiProxyCaseStatus Status { get; set; }
    }
}