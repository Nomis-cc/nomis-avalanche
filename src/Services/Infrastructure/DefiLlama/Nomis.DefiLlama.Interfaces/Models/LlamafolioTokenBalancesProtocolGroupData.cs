// ------------------------------------------------------------------------------------------------------
// <copyright file="LlamafolioTokenBalancesProtocolGroupData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.DefiLlama.Interfaces.Models
{
    /// <summary>
    /// Llamafolio token balances protocol group data.
    /// </summary>
    public class LlamafolioTokenBalancesProtocolGroupData
    {
        /// <summary>
        /// Group total balance in USD.
        /// </summary>
        [JsonPropertyName("balanceUSD")]
        public decimal BalanceUSD { get; set; }

        /// <summary>
        /// Group total debt in USD.
        /// </summary>
        [JsonPropertyName("debtUSD")]
        public decimal DebtUSD { get; set; }

        /// <summary>
        /// Group total reward in USD.
        /// </summary>
        [JsonPropertyName("rewardUSD")]
        public decimal RewardUSD { get; set; }

        /// <summary>
        /// Health factor value.
        /// </summary>
        [JsonPropertyName("healthFactor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? HealthFactor { get; set; }

        /// <summary>
        /// Group balance list.
        /// </summary>
        [JsonPropertyName("balances")]
        public IList<LlamafolioTokenBalancesProtocolGroupBalanceData> Balances { get; set; } = new List<LlamafolioTokenBalancesProtocolGroupBalanceData>();
    }
}