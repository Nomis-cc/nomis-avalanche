// ------------------------------------------------------------------------------------------------------
// <copyright file="LlamafolioTokenBalancesProtocolGroupBalanceData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.DefiLlama.Interfaces.Models
{
    /// <summary>
    /// Llamafolio token balances protocol group balance data.
    /// </summary>
    public class LlamafolioTokenBalancesProtocolGroupBalanceData :
        LlamafolioTokenBalancesProtocolGroupBalanceBaseData
    {
        /// <summary>
        /// Category.
        /// </summary>
        [JsonPropertyName("category")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Category { get; set; }

        #region Category - reward, stake

        /// <summary>
        /// Reward in USD.
        /// </summary>
        /// <remarks>
        /// For category - reward.
        /// </remarks>
        [JsonPropertyName("rewardUSD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? RewardUSD { get; set; }

        /// <summary>
        /// APY.
        /// </summary>
        /// <remarks>
        /// For category - reward, stake.
        /// </remarks>
        [JsonPropertyName("apy")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? APY { get; set; }

        /// <summary>
        /// APY reward.
        /// </summary>
        /// <remarks>
        /// For category - reward, stake, lend.
        /// </remarks>
        [JsonPropertyName("apyReward")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? APYReward { get; set; }

        #endregion Category - reward

        #region Category - lend

        /// <summary>
        /// Collateral value in USD.
        /// </summary>
        /// <remarks>
        /// For category - lend.
        /// </remarks>
        [JsonPropertyName("collateralUSD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? CollateralUSD { get; set; }

        /// <summary>
        /// Collateral factor.
        /// </summary>
        /// <remarks>
        /// For category - lend.
        /// </remarks>
        [JsonPropertyName("collateralFactor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CollateralFactor { get; set; }

        #endregion Category - lend

        #region Category - lock

        /// <summary>
        /// Unlock at.
        /// </summary>
        /// <remarks>
        /// For category - lock.
        /// </remarks>
        [JsonPropertyName("unlockAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ulong? UnlockAt { get; set; }

        #endregion Category - lock

        /// <summary>
        /// Underlyings.
        /// </summary>
        [JsonPropertyName("underlyings")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<LlamafolioTokenBalancesProtocolGroupBalanceBaseData>? Underlyings { get; set; }
    }
}