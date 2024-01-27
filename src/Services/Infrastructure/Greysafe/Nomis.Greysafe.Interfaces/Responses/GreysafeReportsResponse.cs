// ------------------------------------------------------------------------------------------------------
// <copyright file="GreysafeReportsResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Greysafe.Interfaces.Models;

namespace Nomis.Greysafe.Interfaces.Responses
{
    /// <summary>
    /// Greysafe scam reporting service reports response.
    /// </summary>
    public class GreysafeReportsResponse
    {
        /// <summary>
        /// Reports.
        /// </summary>
        [JsonPropertyName("reports")]
        public IList<GreysafeReport> Reports { get; set; } = new List<GreysafeReport>();
    }
}