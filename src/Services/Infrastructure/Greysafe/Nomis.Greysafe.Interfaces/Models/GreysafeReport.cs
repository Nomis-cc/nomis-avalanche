// ------------------------------------------------------------------------------------------------------
// <copyright file="GreysafeReport.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Greysafe.Interfaces.Models
{
    /// <summary>
    /// Greysafe report data.
    /// </summary>
    public class GreysafeReport
    {
        /// <summary>
        /// Report code.
        /// </summary>
        [JsonPropertyName("reportCode")]
        public string? ReportCode { get; set; }

        /// <summary>
        /// Wallet type.
        /// </summary>
        [JsonPropertyName("walletType")]
        public string? WalletType { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Abuse type.
        /// </summary>
        [JsonPropertyName("abuseType")]
        public string? AbuseType { get; set; }

        /// <summary>
        /// Abuse subcategory.
        /// </summary>
        [JsonPropertyName("abuseSubCategory")]
        public string? AbuseSubCategory { get; set; }

        /// <summary>
        /// Abuser type.
        /// </summary>
        [JsonPropertyName("abuserType")]
        public string? AbuserType { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Occurred at.
        /// </summary>
        [JsonPropertyName("occuredAt")]
        public string? OccurredAt { get; set; }

        /// <summary>
        /// Reporter.
        /// </summary>
        [JsonPropertyName("reporter")]
        public string? Reporter { get; set; }

        /// <summary>
        /// Involved exchange name.
        /// </summary>
        [JsonPropertyName("involvedExchangeName")]
        public string? InvolvedExchangeName { get; set; }

        /// <summary>
        /// Media platform used.
        /// </summary>
        [JsonPropertyName("mediaPlatformUsed")]
        public string? MediaPlatformUsed { get; set; }

        /*/// <summary>
        /// Media platform used.
        /// </summary>
        [JsonPropertyName("riskDataVariables")]
        public List<object> RiskDataVariables { get; set; } = new();*/

        /// <summary>
        /// Created at.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }
    }
}