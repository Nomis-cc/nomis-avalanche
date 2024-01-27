// ------------------------------------------------------------------------------------------------------
// <copyright file="WalletStatsExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Extensions
{
    /// <summary>
    /// <see cref="IWalletStats"/> extension methods.
    /// </summary>
    public static class WalletStatsExtensions
    {
        /// <summary>
        /// Calculate wallet score.
        /// </summary>
        /// <param name="stats"><see cref="IWalletStats"/>.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet score.</returns>
        public static double CalculateScore<TWalletStats, TTransactionIntervalData>(
            this TWalletStats stats,
            ulong chainId,
            ScoringCalculationModel calculationModel = ScoringCalculationModel.CommonV1)
            where TWalletStats : IWalletCommonStats<TTransactionIntervalData>
            where TTransactionIntervalData : class, ITransactionIntervalData
        {
            double result = 0;
            if (stats is IWalletStats walletStats)
            {
                result += walletStats.CalculateScore(chainId, calculationModel);
            }

            if (stats is IWalletCommonStats<TTransactionIntervalData> commonStats)
            {
                if (commonStats.NoData)
                {
                    return result;
                }

                result += commonStats.CalculateScore(chainId, calculationModel);
            }

            if (stats is IWalletBalanceStats balanceStats)
            {
                result += balanceStats.CalculateScore(chainId, calculationModel);
            }

            if (stats is IWalletTransactionStats transactionStats)
            {
                result += transactionStats.CalculateScore(chainId, calculationModel);
            }

            if (stats is IWalletNftStats nftStats)
            {
                result += nftStats.CalculateScore(chainId, calculationModel);
            }

            if (stats is IWalletTokenStats tokenStats)
            {
                result += tokenStats.CalculateScore(chainId, calculationModel);
            }

            if (stats is IWalletCounterpartiesStats counterpartiesStats)
            {
                result += counterpartiesStats.CalculateScore(chainId, calculationModel);
            }

            // add additional scores stored in wallet stats implementation class
            foreach (var additionalScore in stats.AdditionalScores)
            {
                result += additionalScore.Invoke(chainId, calculationModel);
            }

            // add adjusting score multipliers stored in wallet stats implementation class
            foreach (var adjustingScoreMultiplier in stats.AdjustingScoreMultipliers)
            {
                result *= adjustingScoreMultiplier.Invoke(chainId, calculationModel);
            }

            return result;
        }
    }
}