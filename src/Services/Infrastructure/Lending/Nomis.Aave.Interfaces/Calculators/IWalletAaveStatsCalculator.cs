// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletAaveStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Aave.Interfaces.Responses;
using Nomis.Aave.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Aave.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet Aave protocol stats calculator.
    /// </summary>
    public interface IWalletAaveStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletAaveStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletAaveStats.AaveData"/>
        public AaveUserAccountDataResponse? AaveData { get; }

        /// <summary>
        /// Get blockchain wallet Aave protocol stats.
        /// </summary>
        public new IWalletAaveStats Stats()
        {
            return new TWalletStats
            {
                AaveData = AaveData
            };
        }

        /// <summary>
        /// Blockchain wallet Aave protocol stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}