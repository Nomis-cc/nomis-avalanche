// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletCommonStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Stats;

namespace Nomis.Utils.Contracts.Calculators
{
    /// <summary>
    /// Blockchain wallet common stats calculator.
    /// </summary>
    /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
    /// <typeparam name="TTransactionIntervalData">The transaction interval data type.</typeparam>
    public interface IWalletCommonStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletCommonStats{TTransactionIntervalData}.WalletAge"/>
        public int WalletAge { get; }

        /// <inheritdoc cref="IWalletCommonStats{TTransactionIntervalData}.ScoredAt"/>
        public DateTime ScoredAt { get; }

        /// <inheritdoc cref="IWalletCommonStats{TTransactionIntervalData}.TurnoverIntervals"/>
        public IList<TTransactionIntervalData> TurnoverIntervals { get; }

        /// <summary>
        /// Get blockchain wallet common stats.
        /// </summary>
        public new IWalletCommonStats<TTransactionIntervalData> Stats()
        {
            return new TWalletStats
            {
                NoData = !TurnoverIntervals.Any(),
                WalletAge = WalletAge,
                TurnoverIntervals = TurnoverIntervals,
                ScoredAt = ScoredAt
            };
        }

        /// <summary>
        /// Blockchain wallet common stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}