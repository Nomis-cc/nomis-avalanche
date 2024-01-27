// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyAddress.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.HapiExplorer.Interfaces.Enums;

namespace Nomis.HapiExplorer.Interfaces.Models
{
    /// <summary>
    /// HAPI proxy address data.
    /// </summary>
    public class HapiProxyAddress
    {
        /// <summary>
        /// Case.
        /// </summary>
        [JsonPropertyName("case")]
        public HapiProxyCase? Case { get; set; }

        /// <summary>
        /// Reporter.
        /// </summary>
        [JsonPropertyName("reporter")]
        public HapiProxyReporter? Reporter { get; set; }

        /// <summary>
        /// Network.
        /// </summary>
        [JsonPropertyName("network")]
        public HapiProxyNetwork? Network { get; set; }

        /// <summary>
        /// Category of most likely activity associated with the address.
        /// </summary>
        /// <example>Theft</example>
        [JsonPropertyName("category")]
        public HapiProxyCategory Category { get; set; }

        /// <summary>
        /// Address risk score on a scale from 0 (safe) to 10 (maximum risk).
        /// </summary>
        /// <remarks>
        /// From 0 to 10.
        /// </remarks>
        /// <example>3</example>
        [JsonPropertyName("risk")]
        public int Risk { get; set; }

        /// <summary>
        /// Number of independent confirmations of this risk score.
        /// </summary>
        /// <example>10</example>
        [JsonPropertyName("confirmations")]
        public int Confirmations { get; set; }

        /// <summary>
        /// Date of address reporting.
        /// </summary>
        /// <example>2022-01-14T12:01:01.000Z</example>
        [JsonPropertyName("timeCreated")]
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Date of last address data modification.
        /// </summary>
        /// <example>2022-01-14T12:01:01.000Z</example>
        [JsonPropertyName("timeUpdated")]
        public DateTime TimeUpdated { get; set; }
    }
}