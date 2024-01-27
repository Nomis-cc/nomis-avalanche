// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletCounterpartiesStatsCalculator.cs" company="Nomis">
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
    /// Blockchain wallet counterparties stats calculator.
    /// </summary>
    /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
    /// <typeparam name="TTransactionIntervalData">The transaction interval data type.</typeparam>
    // ReSharper disable once InconsistentNaming
    public interface IWalletCounterpartiesStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletCounterpartiesStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletCounterpartiesStats.CounterpartiesData"/>
        public IEnumerable<ExtendedCounterpartyData>? CounterpartiesData { get; }

        /// <summary>
        /// Get blockchain wallet counterparties stats.
        /// </summary>
        public new IWalletCounterpartiesStats Stats()
        {
            int? totalCounterpartiesTransactions = CounterpartiesData?.Where(x => x.UseCounterparty).Sum(x => x.CounterpartyTransactions ?? 0);
            int? totalCounterpartiesTransfers = CounterpartiesData?.Where(x => x.UseCounterparty).Sum(x => x.CounterpartyTransfers ?? 0);
            int? totalCounterpartiesNFTTransfers = CounterpartiesData?.Where(x => x.UseCounterparty).Sum(x => x.CounterpartyNFTTransfers?.Count ?? 0);
            decimal? totalCounterpartiesTurnoverUSD = CounterpartiesData?.Where(x => x.UseCounterparty).Sum(x => x.CounterpartyTurnoverUSD ?? 0);

            return new TWalletStats
            {
                CounterpartiesData = CounterpartiesData,
                TotalCounterpartiesTransactions = totalCounterpartiesTransactions > 0 ? totalCounterpartiesTransactions : null,
                TotalCounterpartiesTransfers = totalCounterpartiesTransfers > 0 ? totalCounterpartiesTransfers : null,
                TotalCounterpartiesNFTTransfers = totalCounterpartiesNFTTransfers > 0 ? totalCounterpartiesNFTTransfers : null,
                TotalCounterpartiesTurnoverUSD = totalCounterpartiesTurnoverUSD > 0 ? totalCounterpartiesTurnoverUSD : null,
                TotalCounterpartiesUsed = CounterpartiesData?.Count(x => x.UseCounterparty) ?? 0
            };
        }

        /// <summary>
        /// Blockchain wallet counterparties stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}