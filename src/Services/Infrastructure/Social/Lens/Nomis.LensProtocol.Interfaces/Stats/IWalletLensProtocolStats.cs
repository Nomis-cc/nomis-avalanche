// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletLensProtocolStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.LensProtocol.Interfaces.Models;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.LensProtocol.Interfaces.Stats
{
    /// <summary>
    /// Wallet LensProtocol protocol stats.
    /// </summary>
    public interface IWalletLensProtocolStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet LensProtocol protocol stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletLensProtocolStats
        {
            stats.LensProtocolProfileStats = LensProtocolProfileStats;
        }

        /// <summary>
        /// The LensProtocol protocol profile stats data.
        /// </summary>
        public LensProtocolProfileStatsData? LensProtocolProfileStats { get; set; }

        /// <summary>
        /// Calculate wallet LensProtocol protocol stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet LensProtocol protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            // TODO - add calculation
            return 0;
        }
    }
}