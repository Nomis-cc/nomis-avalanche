// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletBalanceStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Calculators
{
    /// <summary>
    /// Blockchain wallet balance stats calculator.
    /// </summary>
    /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
    /// <typeparam name="TTransactionIntervalData">The transaction interval data type.</typeparam>
    public interface IWalletBalanceStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletBalanceStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletBalanceStats.NativeBalance"/>
        public decimal NativeBalance { get; }

        /// <inheritdoc cref="IWalletBalanceStats.NativeBalanceUSD"/>
        public decimal NativeBalanceUSD { get; }

        /// <inheritdoc cref="IWalletBalanceStats.HistoricalMedianBalanceUSD"/>
        public decimal HistoricalMedianBalanceUSD { get; }

        /// <inheritdoc cref="IWalletBalanceStats.BalanceChangeInLastMonth"/>
        public decimal BalanceChangeInLastMonth { get; }

        /// <inheritdoc cref="IWalletBalanceStats.BalanceChangeInLastYear"/>
        public decimal BalanceChangeInLastYear { get; }

        /// <inheritdoc cref="IWalletBalanceStats.WalletTurnover"/>
        public decimal WalletTurnover { get; }

        /// <inheritdoc cref="IWalletBalanceStats.WalletTurnoverUSD"/>
        public decimal WalletTurnoverUSD { get; }

        /// <inheritdoc cref="IWalletBalanceStats.TokenBalances"/>
        public IEnumerable<TokenDataBalance>? TokenBalances { get; }

        /// <inheritdoc cref="IWalletBalanceStats.TransferTokens"/>
        public IEnumerable<TransferTokenDataBalance>? TransferTokens { get; }

        /// <summary>
        /// Get blockchain wallet balance stats.
        /// </summary>
        public new IWalletBalanceStats Stats()
        {
            return new TWalletStats
            {
                NativeBalance = NativeBalance,
                NativeBalanceUSD = NativeBalanceUSD,
                HistoricalMedianBalanceUSD = HistoricalMedianBalanceUSD,
                BalanceChangeInLastMonth = BalanceChangeInLastMonth,
                BalanceChangeInLastYear = BalanceChangeInLastYear,
                WalletTurnover = WalletTurnover,
                WalletTurnoverUSD = WalletTurnoverUSD,
                TokenBalances = TokenBalances,
                HoldTokensBalanceUSD = TokenBalances?.Sum(b => b.TotalAmountPrice) ?? 0,
                TransferTokens = TransferTokens
            };
        }

        /// <summary>
        /// Blockchain wallet balance stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}