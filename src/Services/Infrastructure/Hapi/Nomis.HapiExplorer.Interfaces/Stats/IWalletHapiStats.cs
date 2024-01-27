// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletHapiStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.HapiExplorer.Interfaces.Stats
{
    /// <summary>
    /// Wallet HAPI protocol stats.
    /// </summary>
    public interface IWalletHapiStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet HAPI protocol stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletHapiStats
        {
            stats.HapiRiskScore = HapiRiskScore;
        }

        /// <summary>
        /// The HAPI protocol risk score.
        /// </summary>
        public HapiProxyRiskScoreResponse? HapiRiskScore { get; set; }

        /// <summary>
        /// Calculate wallet HAPI protocol stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet HAPI protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return 0;
        }

        /// <summary>
        /// Calculate wallet HAPI protocol adjusting score multiplier.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <remarks>
        /// <see href="https://hapi-one.gitbook.io/hapi-protocol/hapi-core-of-decentralized-cybersecurity/risk-assessment"/>
        /// </remarks>
        /// <returns>Returns wallet HAPI protocol adjusting score multiplier.</returns>
        public new double CalculateAdjustingScoreMultiplier(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return HapiRiskScore?.Address?.Risk switch
            {
                >= 10 => 0,
                >= 5 => 0.6,
                >= 1 => 0.8,
                _ => 1
            };
        }
    }
}