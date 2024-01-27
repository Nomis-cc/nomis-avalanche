// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletDexTokenSwapPairsStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Dex.Abstractions.Calculators
{
    /// <summary>
    /// Blockchain wallet DEX token swap pairs stats calculator.
    /// </summary>
    public interface IWalletDexTokenSwapPairsStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletDexTokenSwapPairsStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletDexTokenSwapPairsStats.DexTokensSwapPairs"/>
        public IEnumerable<DexTokenSwapPairsData>? DexTokensSwapPairs { get; }

        /// <summary>
        /// Get blockchain wallet DEX token swap pairs stats.
        /// </summary>
        public new IWalletDexTokenSwapPairsStats Stats()
        {
            return new TWalletStats
            {
                DexTokensSwapPairs = DexTokensSwapPairs
            };
        }

        /// <summary>
        /// Blockchain wallet DEX token swap pairs stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}