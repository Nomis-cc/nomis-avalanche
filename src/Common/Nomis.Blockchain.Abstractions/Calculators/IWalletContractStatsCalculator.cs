// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletContractStatsCalculator.cs" company="Nomis">
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
    /// Blockchain wallet contract stats calculator.
    /// </summary>
    public interface IWalletContractStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletContractStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletContractStats.DeployedContracts"/>
        public int DeployedContracts { get; }

        /// <summary>
        /// Get blockchain wallet contract stats.
        /// </summary>
        public new IWalletContractStats Stats()
        {
            return new TWalletStats
            {
                DeployedContracts = DeployedContracts
            };
        }

        /// <summary>
        /// Blockchain wallet contract stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}