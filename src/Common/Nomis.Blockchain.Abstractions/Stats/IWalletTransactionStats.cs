// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTransactionStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Stats
{
    /// <summary>
    /// Wallet transaction stats.
    /// </summary>
    public interface IWalletTransactionStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet transaction stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletTransactionStats
        {
            stats.TotalTransactions = TotalTransactions;
            stats.TotalRejectedTransactions = TotalRejectedTransactions;
            stats.AverageTransactionTime = AverageTransactionTime;
            stats.MaxTransactionTime = MaxTransactionTime;
            stats.MinTransactionTime = MinTransactionTime;
            stats.TimeSinceTheLastTransaction = TimeSinceTheLastTransaction;
            stats.LastMonthTransactions = LastMonthTransactions;
            stats.LastYearTransactions = LastYearTransactions;
        }

        /// <summary>
        /// Total transactions on wallet (number).
        /// </summary>
        public int TotalTransactions { get; set; }

        /// <summary>
        /// Total rejected transactions on wallet (number).
        /// </summary>
        public int TotalRejectedTransactions { get; set; }

        /// <summary>
        /// Average time interval between transactions (hours).
        /// </summary>
        public double AverageTransactionTime { get; set; }

        /// <summary>
        /// Maximum time interval between transactions (hours).
        /// </summary>
        public double MaxTransactionTime { get; set; }

        /// <summary>
        /// Minimal time interval between transactions (hours).
        /// </summary>
        public double MinTransactionTime { get; set; }

        /// <summary>
        /// Time since the last transaction (months).
        /// </summary>
        public int TimeSinceTheLastTransaction { get; set; }

        /// <summary>
        /// Average transaction per months (number).
        /// </summary>
        public double TransactionsPerMonth { get; }

        /// <summary>
        /// Last month transactions (number).
        /// </summary>
        public int LastMonthTransactions { get; set; }

        /// <summary>
        /// Last year transactions on wallet (number).
        /// </summary>
        public int LastYearTransactions { get; set; }

        /// <summary>
        /// Calculate wallet transaction stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet transaction stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            double result = TotalTransactionsScore(chainId, TotalTransactions, calculationModel) / 100 * TotalTransactionsPercents(chainId, calculationModel);

            double lastMonth = 0.0;
            lastMonth += TransactionsPerMonthScore(chainId, TransactionsPerMonth, calculationModel) / 100 * TransactionsPerMonthPercents(chainId, calculationModel);
            lastMonth += TransactionsLastMonthScore(chainId, LastMonthTransactions, calculationModel) / 100 * TransactionsLastMonthPercents(chainId, calculationModel);
            result += lastMonth * LastMonthPercents(chainId, calculationModel);

            return result;
        }

        private static double TotalTransactionsPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                    return 23.2 / 100;
                case ScoringCalculationModel.XDEFI:
                    return 24.01 / 100;
                case ScoringCalculationModel.Halo:
                    return 19.22 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return 3.72 / 100;
                case ScoringCalculationModel.Rubic:
                    return 5.47 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                    return 20.06 / 100;
                case ScoringCalculationModel.HederaDeFi:
                    return 22.07 / 100;
                case ScoringCalculationModel.HederaNFT:
                    return 5.31 / 100;
                case ScoringCalculationModel.HederaReputation:
                    return 12.5 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 17.58 / 100;
                case ScoringCalculationModel.Meso:
                    return 12.67 / 100;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                    return 6.83 / 100;
                case ScoringCalculationModel.Strateg:
                    return 5.82 / 100;
                case ScoringCalculationModel.Radiant:
                    return 10.38 / 100;
                case ScoringCalculationModel.Manta:
                    return 7.32 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 9.99 / 100;
            }
        }

        private static double TransactionsPerMonthPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                    return 60.81 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.Strateg:
                    return 65.56 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                    return 50D / 100;
                case ScoringCalculationModel.ZkSyncEra:
                    return 83.33 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 80.0 / 100;
                case ScoringCalculationModel.Meso:
                case ScoringCalculationModel.Rubic:
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Radiant:
                case ScoringCalculationModel.Manta:
                    return 70.66 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 18.00 / 100;
            }
        }

        private static double TransactionsLastMonthPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                    return 39.19 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.Strateg:
                    return 34.44 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                    return 50D / 100;
                case ScoringCalculationModel.ZkSyncEra:
                    return 16.67 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 20.0 / 100;
                case ScoringCalculationModel.Meso:
                case ScoringCalculationModel.Rubic:
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Radiant:
                case ScoringCalculationModel.Manta:
                    return 29.34 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 10.34 / 100;
            }
        }

        private static double LastMonthPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                    return 4.99 / 100;
                case ScoringCalculationModel.XDEFI:
                    return 5.35 / 100;
                case ScoringCalculationModel.Halo:
                    return 2.14 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return 2.86 / 100;
                case ScoringCalculationModel.Rubic:
                    return 13.28 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                    return 5.63 / 100;
                case ScoringCalculationModel.HederaDeFi:
                    return 5.6 / 100;
                case ScoringCalculationModel.HederaNFT:
                    return 4.8 / 100;
                case ScoringCalculationModel.HederaReputation:
                    return 12.5 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 4.17 / 100;
                case ScoringCalculationModel.Meso:
                    return 2.73 / 100;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                    return 4.72 / 100;
                case ScoringCalculationModel.Manta:
                    return 5.61 / 100;
                case ScoringCalculationModel.Strateg:
                    return 24.67 / 100;
                case ScoringCalculationModel.Radiant:
                    return 8.73 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 5.7 / 100;
            }
        }

        private static double TotalTransactionsScore(
            ulong chainId,
            int transactions,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                    return transactions switch
                    {
                        <= 1 => 8.76,
                        <= 10 => 15.34,
                        <= 100 => 71.03,
                        <= 1000 => 93.49,
                        _ => 100
                    };
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return transactions switch
                    {
                        <= 1 => 9.73,
                        <= 10 => 14.34,
                        <= 100 => 65.98,
                        <= 1000 => 96.96,
                        _ => 100
                    };
                case ScoringCalculationModel.LayerZero:
                    return transactions switch
                    {
                        <= 30 => 9.73,
                        <= 100 => 14.34,
                        <= 300 => 65.98,
                        <= 1000 => 96.96,
                        _ => 100
                    };
                case ScoringCalculationModel.Rubic:
                    return transactions switch
                    {
                        <= 1 => 3.79,
                        <= 10 => 9.13,
                        <= 100 => 23.68,
                        <= 500 => 62.74,
                        _ => 100
                    };
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                    return transactions switch
                    {
                        <= 1 => 3.79,
                        <= 5 => 9.13,
                        <= 10 => 23.68,
                        <= 100 => 62.74,
                        _ => 100
                    };
                case ScoringCalculationModel.Meso:
                    return transactions switch
                    {
                        <= 1 => 9.73,
                        <= 10 => 14.34,
                        <= 100 => 65.98,
                        <= 500 => 96.96,
                        _ => 100
                    };
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Strateg:
                case ScoringCalculationModel.Radiant:
                case ScoringCalculationModel.Manta:
                    return transactions switch
                    {
                        <= 1 => 8.47,
                        <= 10 => 12.49,
                        <= 100 => 57.43,
                        <= 1000 => 73.48,
                        _ => 100
                    };
                case ScoringCalculationModel.CommonV1:
                default:
                    return transactions switch
                    {
                        <= 1 => 7.62,
                        <= 10 => 14.67,
                        <= 100 => 27.82,
                        <= 1000 => 54.93,
                        _ => 100
                    };
            }
        }

        private static double TransactionsPerMonthScore(
            ulong chainId,
            double value,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                case ScoringCalculationModel.ZkSyncEra:
                    return value switch
                    {
                        <= 1 => 7.44,
                        <= 2 => 17.21,
                        <= 5 => 39.71,
                        <= 10 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.Rubic:
                case ScoringCalculationModel.Meso:
                case ScoringCalculationModel.Strateg:
                case ScoringCalculationModel.Radiant:
                    return value switch
                    {
                        <= 2 => 7.44,
                        <= 5 => 17.21,
                        <= 10 => 39.71,
                        <= 20 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.LayerZero:
                    return value switch
                    {
                        <= 10 => 7.44,
                        <= 25 => 17.21,
                        <= 50 => 39.71,
                        <= 100 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Manta:
                    return value switch
                    {
                        <= 5 => 7.44,
                        <= 10 => 17.21,
                        <= 50 => 39.71,
                        <= 100 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.CommonV1:
                default:
                    return value switch
                    {
                        <= 0.5D => 4.59,
                        <= 1 => 10.62,
                        <= 2 => 24.49,
                        <= 5 => 55.38,
                        _ => 100
                    };
            }
        }

        private static double TransactionsLastMonthScore(
            ulong chainId,
            int value,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return value switch
                    {
                        <= 1 => 11.89,
                        <= 5 => 38.22,
                        <= 10 => 88.01,
                        _ => 100
                    };
                case ScoringCalculationModel.LayerZero:
                    return value switch
                    {
                        <= 10 => 11.89,
                        <= 25 => 38.22,
                        <= 50 => 88.01,
                        _ => 100
                    };
                case ScoringCalculationModel.Meso:
                    return value switch
                    {
                        <= 1 => 11.89,
                        <= 10 => 38.22,
                        <= 25 => 88.01,
                        _ => 100
                    };
                case ScoringCalculationModel.Rubic:
                case ScoringCalculationModel.Strateg:
                case ScoringCalculationModel.Radiant:
                    return value switch
                    {
                        <= 2 => 7.44,
                        <= 5 => 17.21,
                        <= 10 => 39.71,
                        <= 20 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                    return value switch
                    {
                        <= 1 => 7.86,
                        <= 5 => 11.11,
                        <= 10 => 70.71,
                        _ => 100
                    };
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Manta:
                    return value switch
                    {
                        <= 5 => 7.44,
                        <= 10 => 17.21,
                        <= 25 => 39.71,
                        <= 50 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.CommonV1:
                default:
                    return value switch
                    {
                        <= 1 => 4.59,
                        <= 10 => 10.62,
                        <= 50 => 24.49,
                        <= 100 => 55.38,
                        _ => 100
                    };
            }
        }
    }
}