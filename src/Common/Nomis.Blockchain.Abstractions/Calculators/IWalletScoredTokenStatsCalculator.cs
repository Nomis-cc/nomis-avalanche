// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletScoredTokenStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Calculators
{
    /// <summary>
    /// Blockchain wallet scored token stats calculator.
    /// </summary>
    public interface IWalletScoredTokenStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletScoredTokenStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletScoredTokenStats.Token"/>
        public string? Token { get; set; }

        /// <inheritdoc cref="IWalletScoredTokenStats.TokenBalance"/>
        public decimal TokenBalance { get; set; }

        /// <inheritdoc cref="IWalletScoredTokenStats.TokenBalanceUSD"/>
        public decimal TokenBalanceUSD { get; set; }

        /// <summary>
        /// Get blockchain wallet scored token stats.
        /// </summary>
        public new IWalletScoredTokenStats Stats()
        {
            return new TWalletStats
            {
                Token = Token,
                TokenBalance = TokenBalance,
                TokenBalanceUSD = TokenBalanceUSD
            };
        }

        /// <summary>
        /// Blockchain wallet scored token stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}