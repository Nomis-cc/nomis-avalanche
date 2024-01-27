// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletCounterpartiesStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Stats
{
    /// <summary>
    /// Wallet counterparties stats.
    /// </summary>
    public interface IWalletCounterpartiesStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet counterparties stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletCounterpartiesStats
        {
            stats.CounterpartiesData = CounterpartiesData;
            stats.TotalCounterpartiesTransactions = TotalCounterpartiesTransactions;
            stats.TotalCounterpartiesTransfers = TotalCounterpartiesTransfers;
            stats.TotalCounterpartiesNFTTransfers = TotalCounterpartiesNFTTransfers;
            stats.TotalCounterpartiesTurnoverUSD = TotalCounterpartiesTurnoverUSD;
            stats.TotalCounterpartiesUsed = TotalCounterpartiesUsed;
        }

        /// <summary>
        /// Counterparties data.
        /// </summary>
        public IEnumerable<ExtendedCounterpartyData>? CounterpartiesData { get; set; }

        /// <summary>
        /// Total counterparties transactions.
        /// </summary>
        public int? TotalCounterpartiesTransactions { get; set; }

        /// <summary>
        /// Total counterparties transfers.
        /// </summary>
        public int? TotalCounterpartiesTransfers { get; set; }

        /// <summary>
        /// Total counterparties NFT transfers.
        /// </summary>
        public int? TotalCounterpartiesNFTTransfers { get; set; }

        /// <summary>
        /// Total counterparties used.
        /// </summary>
        public int? TotalCounterpartiesUsed { get; set; }

        /// <summary>
        /// Total counterparties turnover in USD.
        /// </summary>
        public decimal? TotalCounterpartiesTurnoverUSD { get; set; }

        /// <summary>
        /// Calculate wallet token balance stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet token balance stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            double result = TotalCounterpartiesTurnoverUSDScore(chainId, TotalCounterpartiesTurnoverUSD ?? 0, calculationModel) / 100 * TotalCounterpartiesTurnoverUSDPercents(chainId, calculationModel);

            return result;
        }

        private static double TotalCounterpartiesTurnoverUSDPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.LayerZero:
                    return 25.30 / 100;
                default:
                    return 0;
            }
        }

        private static double TotalCounterpartiesTurnoverUSDScore(
            ulong chainId,
            decimal turnoverUSD,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.LayerZero:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 1000 => 8.62,
                            <= 5000 => 36.15,
                            <= 25000 => 64.44,
                            <= 50000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                default:
                    return 0;
            }
        }
    }
}