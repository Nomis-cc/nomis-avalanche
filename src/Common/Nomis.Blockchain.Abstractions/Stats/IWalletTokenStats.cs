// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTokenStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Stats
{
    /// <summary>
    /// Wallet token stats.
    /// </summary>
    public interface IWalletTokenStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet token stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletTokenStats
        {
            stats.TokensHolding = TokensHolding;
        }

        /// <summary>
        /// Value of all holding tokens (number).
        /// </summary>
        public int TokensHolding { get; set; }

        /// <summary>
        /// Calculate wallet token stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet token stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            double result = TokensHoldingScore(chainId, TokensHolding, calculationModel) / 100 * TokensHoldingPercents(chainId, calculationModel);

            return result;
        }

        private static double TokensHoldingPercents(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            switch (calculationModel)
            {
                case ScoringCalculationModel.Symbiosis:
                    return 3.23 / 100;
                case ScoringCalculationModel.XDEFI:
                    return 17.86 / 100;
                case ScoringCalculationModel.Halo:
                    return 7.4 / 100;
                case ScoringCalculationModel.CommonV2:
                case ScoringCalculationModel.CommonV3:
                case ScoringCalculationModel.ZkSyncEra:
                    return 14.07 / 100;
                case ScoringCalculationModel.Rubic:
                    return 10.09 / 100;
                case ScoringCalculationModel.HederaSybilPrevention:
                    return 10.47 / 100;
                case ScoringCalculationModel.HederaDeFi:
                    return 10.19 / 100;
                case ScoringCalculationModel.HederaNFT:
                    return 19.04 / 100;
                case ScoringCalculationModel.HederaReputation:
                    return 12.5 / 100;
                case ScoringCalculationModel.LayerZero:
                    return 7.93 / 100;
                case ScoringCalculationModel.Meso:
                    return 18.7 / 100;
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                    return 17.84 / 100;
                case ScoringCalculationModel.Strateg:
                    return 8.67 / 100;
                case ScoringCalculationModel.Radiant:
                    return 14.05 / 100;
                case ScoringCalculationModel.Manta:
                    return 18.73 / 100;
                case ScoringCalculationModel.CommonV1:
                default:
                    return 3.86 / 100;
            }
        }

        private static double TokensHoldingScore(
            ulong chainId,
            int tokens,
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
                case ScoringCalculationModel.LayerZero:
                    return tokens switch
                    {
                        <= 1 => 10.91,
                        <= 3 => 48.96,
                        <= 7 => 59.57,
                        <= 10 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.Rubic:
                case ScoringCalculationModel.Meso:
                case ScoringCalculationModel.Linea:
                case ScoringCalculationModel.Scroll:
                case ScoringCalculationModel.Strateg:
                case ScoringCalculationModel.Radiant:
                case ScoringCalculationModel.Manta:
                    return tokens switch
                    {
                        <= 2 => 10.91,
                        <= 5 => 48.96,
                        <= 10 => 59.57,
                        <= 15 => 78.6,
                        _ => 100
                    };
                case ScoringCalculationModel.CommonV1:
                default:
                    return tokens switch
                    {
                        <= 1 => 7.7,
                        <= 5 => 14.86,
                        <= 10 => 34.49,
                        <= 100 => 65.98,
                        _ => 100
                    };
            }
        }
    }
}