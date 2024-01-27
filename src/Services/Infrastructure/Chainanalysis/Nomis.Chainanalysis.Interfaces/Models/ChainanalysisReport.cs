// ------------------------------------------------------------------------------------------------------
// <copyright file="ChainanalysisReport.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Chainanalysis.Interfaces.Models
{
    /// <summary>
    /// Chainanalysis report data.
    /// </summary>
    public class ChainanalysisReport
    {
        /// <summary>
        /// Category.
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}