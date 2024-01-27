// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheWalletStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Nomis.Aave.Interfaces.Responses;
using Nomis.Aave.Interfaces.Stats;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Stats;
using Nomis.Utils.Contracts.Stats;

namespace Nomis.Snowtrace.Interfaces.Models
{
    /// <summary>
    /// Avalanche wallet stats.
    /// </summary>
    public sealed class AvalancheWalletStats :
        BaseEvmWalletStats<AvalancheTransactionIntervalData>,
        IWalletDexTokenSwapPairsStats,
        IWalletNftStats,
        IWalletAaveStats
    {
        /// <inheritdoc/>
        public override string NativeToken => "AVAX";

        /// <inheritdoc/>
        [Display(Description = "Total NFTs on wallet", GroupName = "number")]
        public int NFTHolding { get; set; }

        /// <inheritdoc/>
        [Display(Description = "NFT trading activity", GroupName = "Native token")]
        public decimal NFTTrading { get; set; }

        /// <inheritdoc/>
        [Display(Description = "NFT relative turnover", GroupName = "Native token")]
        public decimal NFTWorth { get; set; }

        /// <inheritdoc/>
        [Display(Description = "DEX tokens balances", GroupName = "collection")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<DexTokenSwapPairsData>? DexTokensSwapPairs { get; set; }

        /// <inheritdoc/>
        [Display(Description = "The Aave protocol user account data", GroupName = "value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AaveUserAccountDataResponse? AaveData { get; set; }

        /// <inheritdoc cref="IWalletCommonStats{TTransactionIntervalData}.ExcludedStatDescriptions"/>
        [JsonIgnore]
        public override IEnumerable<string> ExcludedStatDescriptions =>
            base.ExcludedStatDescriptions
                .Union(new List<string>
                {
                    nameof(DexTokensSwapPairs),
                    nameof(AaveData)
                });
    }
}