// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletScoredTokenStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Stats
{
    /// <summary>
    /// Wallet scored token stats.
    /// </summary>
    public interface IWalletScoredTokenStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet scored token stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletScoredTokenStats
        {
            stats.Token = Token;
            stats.TokenBalance = TokenBalance;
            stats.TokenBalanceUSD = TokenBalanceUSD;
        }

        /// <summary>
        /// Token.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Token balance.
        /// </summary>
        public decimal TokenBalance { get; set; }

        /// <summary>
        /// Wallet token balance (in USD).
        /// </summary>
        public decimal TokenBalanceUSD { get; set; }

        /// <summary>
        /// Calculate wallet scored token stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet scored token stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            return 0;
        }
    }
}