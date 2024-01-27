// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletChainanalysisStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Chainanalysis.Interfaces.Models;
using Nomis.Chainanalysis.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Chainanalysis.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet Chainanalysis sanctions reporting service stats calculator.
    /// </summary>
    public interface IWalletChainanalysisStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletChainanalysisStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletChainanalysisStats.ChainanalysisReports"/>
        public IEnumerable<ChainanalysisReport>? ChainanalysisReports { get; }

        /// <summary>
        /// Get blockchain wallet Chainanalysis sanctions reporting service stats.
        /// </summary>
        public new IWalletChainanalysisStats Stats()
        {
            return new TWalletStats
            {
                ChainanalysisReports = ChainanalysisReports
            };
        }

        /// <summary>
        /// Blockchain wallet Chainanalysis sanctions reporting service stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}