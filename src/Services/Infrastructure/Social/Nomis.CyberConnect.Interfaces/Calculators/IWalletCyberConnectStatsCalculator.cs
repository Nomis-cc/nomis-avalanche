// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletCyberConnectStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.CyberConnect.Interfaces.Models;
using Nomis.CyberConnect.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.CyberConnect.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet CyberConnect protocol stats calculator.
    /// </summary>
    public interface IWalletCyberConnectStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletCyberConnectStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletCyberConnectStats.CyberConnectLikes"/>
        public CyberConnectProfileData? CyberConnectProfile { get; }

        /// <inheritdoc cref="IWalletCyberConnectStats.CyberConnectLikes"/>
        public IEnumerable<CyberConnectLikeData>? CyberConnectLikes { get; }

        /// <inheritdoc cref="IWalletCyberConnectStats.CyberConnectEssences"/>
        public IEnumerable<CyberConnectEssenceData>? CyberConnectEssences { get; }

        /// <inheritdoc cref="IWalletCyberConnectStats.CyberConnectSubscribings"/>
        public IEnumerable<CyberConnectSubscribingProfileData>? CyberConnectSubscribings { get; }

        /// <summary>
        /// Get blockchain wallet CyberConnect protocol stats.
        /// </summary>
        public new IWalletCyberConnectStats Stats()
        {
            return new TWalletStats
            {
                CyberConnectProfile = CyberConnectProfile,
                CyberConnectLikes = CyberConnectLikes,
                CyberConnectEssences = CyberConnectEssences,
                CyberConnectSubscribings = CyberConnectSubscribings
            };
        }

        /// <summary>
        /// Blockchain wallet CyberConnect protocol stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}