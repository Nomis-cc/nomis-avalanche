// ------------------------------------------------------------------------------------------------------
// <copyright file="ChainanalysisReportsResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Chainanalysis.Interfaces.Models;

namespace Nomis.Chainanalysis.Interfaces.Responses
{
    /// <summary>
    /// Chainanalysis sanctions reporting service reports response.
    /// </summary>
    public class ChainanalysisReportsResponse
    {
        /// <summary>
        /// Sanction identifications.
        /// </summary>
        [JsonPropertyName("identifications")]
        public IList<ChainanalysisReport> Identifications { get; set; } = new List<ChainanalysisReport>();
    }
}