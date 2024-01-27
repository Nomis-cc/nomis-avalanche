// ------------------------------------------------------------------------------------------------------
// <copyright file="LlamafolioTokenBalancesResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.DefiLlama.Interfaces.Models;

namespace Nomis.DefiLlama.Interfaces.Responses
{
    /// <summary>
    /// Llamafolio token balances response.
    /// </summary>
    public class LlamafolioTokenBalancesResponse
    {
        /// <summary>
        /// Status.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Updated at.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public ulong UpdatedAt { get; set; }

        /// <summary>
        /// Protocol list.
        /// </summary>
        [JsonPropertyName("protocols")]
        public IList<LlamafolioTokenBalancesProtocolData> Protocols { get; set; } = new List<LlamafolioTokenBalancesProtocolData>();
    }
}