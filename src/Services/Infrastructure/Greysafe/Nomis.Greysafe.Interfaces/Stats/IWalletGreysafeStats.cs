// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletGreysafeStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Greysafe.Interfaces.Models;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Greysafe.Interfaces.Stats
{
    /// <summary>
    /// Wallet Greysafe scam reporting service stats.
    /// </summary>
    public interface IWalletGreysafeStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet Greysafe scam reporting service stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletGreysafeStats
        {
            stats.GreysafeReports = GreysafeReports;
        }

        /// <summary>
        /// The Greysafe scam reports.
        /// </summary>
        public IEnumerable<GreysafeReport>? GreysafeReports { get; set; }

        /// <summary>
        /// Calculate wallet Greysafe stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Greysafe protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return 0;
        }

        /// <summary>
        /// Calculate wallet Greysafe adjusting score multiplier.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Greysafe adjusting score multiplier.</returns>
        public new double CalculateAdjustingScoreMultiplier(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            // TODO - add
            return 1;
        }
    }
}