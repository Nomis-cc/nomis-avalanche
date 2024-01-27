// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletHapiStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.HapiExplorer.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.HapiExplorer.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet HAPI protocol stats calculator.
    /// </summary>
    public interface IWalletHapiStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletHapiStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletHapiStats.HapiRiskScore"/>
        public HapiProxyRiskScoreResponse? HapiRiskScore { get; }

        /// <summary>
        /// Get blockchain wallet HAPI protocol stats.
        /// </summary>
        public new IWalletHapiStats Stats()
        {
            return new TWalletStats
            {
                HapiRiskScore = HapiRiskScore
            };
        }

        /// <summary>
        /// Blockchain wallet HAPI protocol stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}