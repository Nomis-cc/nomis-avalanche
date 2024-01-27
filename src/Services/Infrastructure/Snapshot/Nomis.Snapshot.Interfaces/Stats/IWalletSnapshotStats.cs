// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletSnapshotStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Models;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

namespace Nomis.Snapshot.Interfaces.Stats
{
    /// <summary>
    /// Wallet Snapshot protocol stats.
    /// </summary>
    public interface IWalletSnapshotStats :
        IWalletStats
    {
        /// <summary>
        /// Set wallet Snapshot protocol stats.
        /// </summary>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <param name="stats">The wallet stats.</param>
        public new void FillStatsTo<TWalletStats>(TWalletStats stats)
            where TWalletStats : class, IWalletSnapshotStats
        {
            stats.SnapshotVotes = SnapshotVotes;
            stats.SnapshotProposals = SnapshotProposals;
        }

        /// <summary>
        /// The Snapshot protocol votes.
        /// </summary>
        public IEnumerable<SnapshotProtocolVoteData>? SnapshotVotes { get; set; }

        /// <summary>
        /// The Snapshot protocol proposals.
        /// </summary>
        public IEnumerable<SnapshotProtocolProposalData>? SnapshotProposals { get; set; }

        /// <summary>
        /// Calculate wallet Snapshot protocol stats score.
        /// </summary>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="calculationModel">Scoring calculation model.</param>
        /// <returns>Returns wallet Snapshot protocol stats score.</returns>
        public new double CalculateScore(
            ulong chainId,
            ScoringCalculationModel calculationModel)
        {
            // TODO - add calculation
            return 0;
        }
    }
}