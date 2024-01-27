// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTransactionStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Blockchain.Abstractions.Calculators
{
    /// <summary>
    /// Blockchain wallet transaction stats calculator.
    /// </summary>
    public interface IWalletTransactionStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletTransactionStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <summary>
        /// Get blockchain wallet transaction stats.
        /// </summary>
        public new IWalletTransactionStats Stats();

        /// <summary>
        /// Blockchain wallet transaction stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}