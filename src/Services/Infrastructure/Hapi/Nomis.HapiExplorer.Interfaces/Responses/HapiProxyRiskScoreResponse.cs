// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyRiskScoreResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.HapiExplorer.Interfaces.Models;

namespace Nomis.HapiExplorer.Interfaces.Responses
{
    /// <summary>
    /// HAPI proxy risk score response.
    /// </summary>
    public class HapiProxyRiskScoreResponse
    {
        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public HapiProxyAddress? Address { get; set; }
    }
}