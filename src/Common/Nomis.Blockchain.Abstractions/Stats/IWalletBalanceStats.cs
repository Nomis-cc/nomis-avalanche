// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletBalanceStats.cs" company="Nomis">
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
    /// Wallet token balance stats.
    /// </summary>
    public interface IWalletBalanceStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet token balance stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletBalanceStats
        {
            stats.NativeBalance = NativeBalance;
            stats.NativeBalanceUSD = NativeBalanceUSD;
            stats.HistoricalMedianBalanceUSD = HistoricalMedianBalanceUSD;
            stats.BalanceChangeInLastMonth = BalanceChangeInLastMonth;
            stats.BalanceChangeInLastYear = BalanceChangeInLastYear;
            stats.WalletTurnover = WalletTurnover;
            stats.WalletTurnoverUSD = WalletTurnoverUSD;

            stats.TokenBalances = TokenBalances;
            stats.HoldTokensBalanceUSD = HoldTokensBalanceUSD;
            if (HoldTokensBalanceUSD == 0)
            {
                HoldTokensBalanceUSD = TokenBalances?.Sum(b => b.TotalAmountPrice) ?? 0;
            }

            stats.TransferTokens = TransferTokens;
        }

        /// <summary>
        /// Native token symbol.
        /// </summary>
        public string NativeToken { get; }

        /// <summary>
        /// Wallet balance (Native token).
        /// </summary>
        public decimal NativeBalance { get; set; }

        /// <summary>
        /// Wallet balance (Native token in USD).
        /// </summary>
        public decimal NativeBalanceUSD { get; set; }

        /// <summary>
        /// Wallet historical median balance in USD.
        /// </summary>
        public decimal HistoricalMedianBalanceUSD { get; set; }

        /// <summary>
        /// The balance change value in the last month (Native token).
        /// </summary>
        public decimal BalanceChangeInLastMonth { get; set; }

        /// <summary>
        /// The balance change value in the last year (Native token).
        /// </summary>
        public decimal BalanceChangeInLastYear { get; set; }

        /// <summary>
        /// The movement of funds on the wallet (Native token).
        /// </summary>
        public decimal WalletTurnover { get; set; }

        /// <summary>
        /// The movement of funds on the wallet (Native token in USD).
        /// </summary>
        public decimal WalletTurnoverUSD { get; set; }

        /// <summary>
        /// Hold token balances.
        /// </summary>
        public IEnumerable<TokenDataBalance>? TokenBalances { get; set; }

        /// <summary>
        /// Wallet hold tokens total balance (USD).
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public decimal HoldTokensBalanceUSD { get; set; }

        /// <summary>
        /// Transfer tokens data.
        /// </summary>
        public IEnumerable<TransferTokenDataBalance>? TransferTokens { get; set; }

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
            if (HoldTokensBalanceUSD == 0)
            {
                HoldTokensBalanceUSD = TokenBalances?.Sum(b => b.TotalAmountPrice) ?? 0;
            }

            decimal scoredUSDBalance;
            if (HistoricalMedianBalanceUSD == 0)
            {
                scoredUSDBalance = NativeBalanceUSD + HoldTokensBalanceUSD;
            }
            else
            {
                scoredUSDBalance = HistoricalMedianBalanceUSD;
            }

            double result = BalanceScore(chainId, NativeBalance, scoredUSDBalance, calculationModel) / 100 * BalancePercents(chainId, calculationModel);
            result += WalletTurnoverScore(chainId, WalletTurnover, WalletTurnoverUSD + TransferTokens?.Sum(x => x.TotalAmountPrice) ?? 0, calculationModel) / 100 * WalletTurnoverPercents(chainId, calculationModel);

            return result;
        }

        private static double BalancePercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                    return 20.41 / 100;
                case ScoringCalculationModel.XDEFI:
                    return 14.49 / 100;
                case ScoringCalculationModel.Halo:
                    return 17.23 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return 26.05 / 100;
                case ScoringCalculationModel.Rubic:
                    return 15.02 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                    return 26.02 / 100;
                case ScoringCalculationModel.HederaDeFi:
                    return 24.26 / 100;
                case ScoringCalculationModel.HederaNFT:
                    return 12.34 / 100;
                case ScoringCalculationModel.HederaReputation:
                    return 12.5 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 8.64 / 100;
                case ScoringCalculationModel.Meso:
                    return 7.16 / 100;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                    return 20.16 / 100;
                case ScoringCalculationModel.Strateg:
                    return 5.27 / 100;
                case ScoringCalculationModel.Radiant:
                    return 15.68 / 100;
                case ScoringCalculationModel.Manta:
                    return 21.55 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 26.88 / 100;
            }
        }

        private static double WalletTurnoverPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                    return 23.2 / 100;
                case ScoringCalculationModel.XDEFI:
                    return 7.67 / 100;
                case ScoringCalculationModel.Halo:
                    return 9.0 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return 4.43 / 100;
                case ScoringCalculationModel.Rubic:
                    return 26.7 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                    return 4.2 / 100;
                case ScoringCalculationModel.HederaDeFi:
                    return 12.76 / 100;
                case ScoringCalculationModel.HederaNFT:
                    return 5.16 / 100;
                case ScoringCalculationModel.HederaReputation:
                    return 12.5 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 25.30 / 100;
                case ScoringCalculationModel.Meso:
                    return 3.94 / 100;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                    return 28.66 / 100;
                case ScoringCalculationModel.Strateg:
                    return 16.13 / 100;
                case ScoringCalculationModel.Radiant:
                    return 19.61 / 100;
                case ScoringCalculationModel.Manta:
                    return 32.21 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 16.31 / 100;
            }
        }

        private static double BalanceScore(
            ulong chainId,
            decimal balance,
            decimal scoredUSDBalance,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.Meso:
                    if (scoredUSDBalance > 0)
                    {
                        return scoredUSDBalance switch
                        {
                            <= 50 => 8.84,
                            <= 200 => 28.44,
                            <= 1000 => 51.35,
                            <= 10000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Strateg:
                case ScoringCalculationModel.Radiant:
                    if (scoredUSDBalance > 0)
                    {
                        return scoredUSDBalance switch
                        {
                            <= 50 => 7.88,
                            <= 500 => 24.76,
                            <= 5000 => 42.51,
                            <= 50000 => 71.77,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Rubic:
                    if (scoredUSDBalance > 0)
                    {
                        return scoredUSDBalance switch
                        {
                            <= 20 => 8.84,
                            <= 200 => 28.44,
                            <= 1000 => 51.35,
                            <= 10000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                    if (scoredUSDBalance > 0)
                    {
                        return scoredUSDBalance switch
                        {
                            <= 10 => 9.89,
                            <= 50 => 15.41,
                            <= 100 => 27.20,
                            <= 200 => 47.29,
                            <= 1000 => 53.1,
                            <= 10000 => 95.31,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.ZkSyncEra:
                case ScoringCalculationModel.LayerZero:
                    if (scoredUSDBalance > 0)
                    {
                        return scoredUSDBalance switch
                        {
                            <= 10 => 8.84,
                            <= 100 => 28.44,
                            <= 500 => 51.35,
                            <= 5000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Manta:
                    if (scoredUSDBalance > 0)
                    {
                        return scoredUSDBalance switch
                        {
                            <= 50 => 7.88,
                            <= 200 => 24.76,
                            <= 1000 => 42.51,
                            <= 10000 => 71.77,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.CommonV1:
                default:
                    return balance switch
                    {
                        <= 0.2m => 7.7,
                        <= 0.4m => 22.23,
                        <= 0.7m => 23.05,
                        <= 1m => 65.98,
                        _ => 100
                    };
            }
        }

        private static double WalletTurnoverScore(
            ulong chainId,
            decimal turnover,
            decimal turnoverUSD,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                case ScoringCalculationModel.XDEFI:
                case ScoringCalculationModel.Halo:
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.ZkSyncEra:
                    return turnover switch
                    {
                        <= 1 => 8.62,
                        <= 10 => 36.15,
                        <= 20 => 64.44,
                        <= 50 => 92.21,
                        _ => 100
                    };
                case ScoringCalculationModel.CommonV3:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 500 => 8.62,
                            <= 5000 => 36.15,
                            <= 10000 => 64.44,
                            <= 50000 => 92.21,
                            _ => 100
                        };
                    }

                    return turnover switch
                    {
                        <= 1 => 8.62,
                        <= 10 => 36.15,
                        <= 20 => 64.44,
                        <= 50 => 92.21,
                        _ => 100
                    };
                case ScoringCalculationModel.HederaSybilPrevention:
                case ScoringCalculationModel.HederaDeFi:
                case ScoringCalculationModel.HederaNFT:
                case ScoringCalculationModel.HederaReputation:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 50 => 8.62,
                            <= 200 => 36.15,
                            <= 1000 => 64.44,
                            <= 10000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Rubic:
                case ScoringCalculationModel.Meso:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 100 => 8.62,
                            <= 500 => 36.15,
                            <= 5000 => 64.44,
                            <= 50000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 100 => 7.51,
                            <= 500 => 29.02,
                            <= 5000 => 56.1,
                            <= 50000 => 75.79,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Manta:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 100 => 7.51,
                            <= 1000 => 29.02,
                            <= 10000 => 56.1,
                            <= 100000 => 75.79,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Strateg:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 100 => 8.62,
                            <= 500 => 36.15,
                            <= 5000 => 64.44,
                            <= 50000 => 92.21,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.Radiant:
                    if (turnoverUSD > 0)
                    {
                        return turnoverUSD switch
                        {
                            <= 1000 => 7.51,
                            <= 5000 => 29.02,
                            <= 50000 => 56.1,
                            <= 500000 => 75.79,
                            _ => 100
                        };
                    }

                    return 0;
                case ScoringCalculationModel.LayerZero:
                    return 0;
                case ScoringCalculationModel.CommonV1:
                default:
                    return turnover switch
                    {
                        <= 10 => 7.62,
                        <= 50 => 14.67,
                        <= 100 => 27.82,
                        <= 1000 => 55.38,
                        _ => 100
                    };
            }
        }
    }
}