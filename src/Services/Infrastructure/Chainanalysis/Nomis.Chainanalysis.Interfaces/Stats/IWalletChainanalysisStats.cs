// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletChainanalysisStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Chainanalysis.Interfaces.Models;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Chainanalysis.Interfaces.Stats
{
    /// <summary>
    /// Wallet Chainanalysis sanctions reporting service stats.
    /// </summary>
    public interface IWalletChainanalysisStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet Chainanalysis sanctions reporting service stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletChainanalysisStats
        {
            stats.ChainanalysisReports = ChainanalysisReports;
        }

        /// <summary>
        /// The Chainanalysis sanctions reports.
        /// </summary>
        public IEnumerable<ChainanalysisReport>? ChainanalysisReports { get; set; }

        /// <summary>
        /// Calculate wallet Chainanalysis stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Chainanalysis protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return 0;
        }

        /// <summary>
        /// Calculate wallet Chainanalysis adjusting score multiplier.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Chainanalysis adjusting score multiplier.</returns>
        public new double CalculateAdjustingScoreMultiplier(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            // TODO - add
            return 1;
        }
    }
}