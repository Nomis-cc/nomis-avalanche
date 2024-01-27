// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Enums;

namespace Nomis.Utils.Contracts.Stats
{
    /// <summary>
    /// Wallet stats.
    /// </summary>
    public interface IWalletStats
    {
        /// <summary>
        /// No data.
        /// </summary>
        public bool NoData { get; set; }

        /// <summary>
        /// Set wallet stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletStats
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Calculate wallet default stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet default stats score.</returns>
        public double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return 0;
        }

        /// <summary>
        /// Calculate wallet default adjusting score multiplier.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet default adjusting score multiplier.</returns>
        public double CalculateAdjustingScoreMultiplier(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return 1;
        }
    }
}