// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTallyStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Tally.Interfaces.Models;
using Nomis.Tally.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Tally.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet Tally protocol stats calculator.
    /// </summary>
    public interface IWalletTallyStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletTallyStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletTallyStats.TallyAccount"/>
        public TallyAccount? TallyAccount { get; }

        /// <summary>
        /// Get blockchain wallet Tally protocol stats.
        /// </summary>
        public new IWalletTallyStats Stats()
        {
            return new TWalletStats
            {
                TallyAccount = TallyAccount
            };
        }

        /// <summary>
        /// Blockchain wallet Tally protocol stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}