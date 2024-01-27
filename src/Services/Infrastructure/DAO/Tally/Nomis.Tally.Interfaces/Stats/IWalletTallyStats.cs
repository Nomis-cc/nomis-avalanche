// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTallyStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Tally.Interfaces.Models;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Tally.Interfaces.Stats
{
    /// <summary>
    /// Wallet Tally protocol stats.
    /// </summary>
    public interface IWalletTallyStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet Tally protocol stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletTallyStats
        {
            stats.TallyAccount = TallyAccount;
        }

        /// <summary>
        /// The Tally protocol account data.
        /// </summary>
        public TallyAccount? TallyAccount { get; set; }

        /// <summary>
        /// Calculate wallet Tally protocol stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Tally protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            // TODO - add calculation
            return 0;
        }
    }
}