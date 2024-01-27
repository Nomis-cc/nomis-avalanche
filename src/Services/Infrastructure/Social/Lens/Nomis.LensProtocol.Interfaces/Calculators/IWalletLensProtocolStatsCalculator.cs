// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletLensProtocolStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.LensProtocol.Interfaces.Models;
using Nomis.LensProtocol.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.LensProtocol.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet Lens Protocol protocol stats calculator.
    /// </summary>
    public interface IWalletLensProtocolStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletLensProtocolStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletLensProtocolStats.LensProtocolProfileStats"/>
        public LensProtocolProfileStatsData? LensProtocolProfileStats { get; }

        /// <summary>
        /// Get blockchain wallet LensProtocol protocol stats.
        /// </summary>
        public new IWalletLensProtocolStats Stats()
        {
            return new TWalletStats
            {
                LensProtocolProfileStats = LensProtocolProfileStats
            };
        }

        /// <summary>
        /// Blockchain wallet Lens Protocol protocol stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}