// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyReporter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.HapiExplorer.Interfaces.Enums;

namespace Nomis.HapiExplorer.Interfaces.Models
{
    /// <summary>
    /// HAPI Proxy reporter data.
    /// </summary>
    public class HapiProxyReporter
    {
        /// <summary>
        /// Reporter public key.
        /// </summary>
        /// <example>GfetJsLPLM6ExJCKbdBUHq1nuw3J9R8CtG9WAaExwqYS</example>
        [JsonPropertyName("pubkey")]
        public string? Pubkey { get; set; }

        /// <summary>
        /// Reporter role.
        /// </summary>
        /// <example>Tracer</example>
        [JsonPropertyName("role")]
        public HapiProxyReporterRole Role { get; set; }

        /// <summary>
        /// Reporter name.
        /// </summary>
        /// <example>api-team-validator</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}