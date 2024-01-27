// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletNftStatsCalculator.cs" company="Nomis">
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
    /// Blockchain wallet NFT stats calculator.
    /// </summary>
    public interface IWalletNftStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletNftStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <summary>
        /// Get blockchain wallet NFT stats.
        /// </summary>
        public new IWalletNftStats Stats();

        /// <summary>
        /// Blockchain wallet NFT stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}