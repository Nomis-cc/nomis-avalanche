// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTokenStatsCalculator.cs" company="Nomis">
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
    /// Blockchain wallet token stats calculator.
    /// </summary>
    public interface IWalletTokenStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletTokenStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletTokenStats.TokensHolding"/>
        public int TokensHolding { get; }

        /// <summary>
        /// Get blockchain wallet token stats.
        /// </summary>
        public new IWalletTokenStats Stats()
        {
            return new TWalletStats
            {
                TokensHolding = TokensHolding
            };
        }

        /// <summary>
        /// Blockchain wallet token stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}