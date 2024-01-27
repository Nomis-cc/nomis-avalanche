// ------------------------------------------------------------------------------------------------------
// <copyright file="LlamafolioTokenBalancesProtocolData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.DefiLlama.Interfaces.Models
{
    /// <summary>
    /// Llamafolio token balances protocol data.
    /// </summary>
    public class LlamafolioTokenBalancesProtocolData
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        /// <summary>
        /// Chain.
        /// </summary>
        [JsonPropertyName("chain")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Chain { get; set; }

        /// <summary>
        /// Total balance in USD.
        /// </summary>
        [JsonPropertyName("balanceUSD")]
        public decimal BalanceUSD { get; set; }

        /// <summary>
        /// Total reward in USD.
        /// </summary>
        [JsonPropertyName("rewardUSD")]
        public decimal RewardUSD { get; set; }

        /// <summary>
        /// Group list.
        /// </summary>
        [JsonPropertyName("groups")]
        public IList<LlamafolioTokenBalancesProtocolGroupData> Groups { get; set; } = new List<LlamafolioTokenBalancesProtocolGroupData>();
    }
}