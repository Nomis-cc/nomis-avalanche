// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletAaveStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Aave.Interfaces.Responses;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Aave.Interfaces.Stats
{
    /// <summary>
    /// Wallet Aave protocol stats.
    /// </summary>
    public interface IWalletAaveStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet Aave stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletAaveStats
        {
            stats.AaveData = AaveData;
        }

        /// <summary>
        /// Aave user account data.
        /// </summary>
        public AaveUserAccountDataResponse? AaveData { get; set; }

        /// <summary>
        /// Calculate wallet Aave protocol stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Aave protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            // TODO - add calculation
            return 0;
        }
    }
}