// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletGreysafeStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Greysafe.Interfaces.Models;
using Nomis.Greysafe.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Greysafe.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet Greysafe scam reporting service stats calculator.
    /// </summary>
    public interface IWalletGreysafeStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletGreysafeStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletGreysafeStats.GreysafeReports"/>
        public IEnumerable<GreysafeReport>? GreysafeReports { get; }

        /// <summary>
        /// Get blockchain wallet Greysafe scam reporting service stats.
        /// </summary>
        public new IWalletGreysafeStats Stats()
        {
            return new TWalletStats
            {
                GreysafeReports = GreysafeReports
            };
        }

        /// <summary>
        /// Blockchain wallet Greysafe scam reporting service stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}