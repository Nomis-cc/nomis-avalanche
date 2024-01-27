// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletSnapshotStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Models;
using Nomis.Snapshot.Interfaces.Stats;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Snapshot.Interfaces.Calculators
{
    /// <summary>
    /// Blockchain wallet Snapshot protocol stats calculator.
    /// </summary>
    public interface IWalletSnapshotStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>
        where TWalletStats : class, IWalletSnapshotStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <inheritdoc cref="IWalletSnapshotStats.SnapshotProposals"/>
        public IEnumerable<SnapshotProposal>? SnapshotProposals { get; }

        /// <inheritdoc cref="IWalletSnapshotStats.SnapshotVotes"/>
        public IEnumerable<SnapshotVote>? SnapshotVotes { get; }

        /// <summary>
        /// Get blockchain wallet Snapshot protocol stats.
        /// </summary>
        public new IWalletSnapshotStats Stats()
        {
            return new TWalletStats
            {
                SnapshotVotes = SnapshotProtocolVotesData(SnapshotVotes),
                SnapshotProposals = SnapshotProtocolProposalsData(SnapshotProposals)
            };
        }

        /// <summary>
        /// Get Snapshot protocol votes data.
        /// </summary>
        /// <param name="snapshotVotes">Collection of <see cref="SnapshotVote"/>.</param>
        /// <returns>Returns collection of <see cref="SnapshotProtocolVoteData"/>.</returns>
        public static IEnumerable<SnapshotProtocolVoteData>? SnapshotProtocolVotesData(
            IEnumerable<SnapshotVote>? snapshotVotes)
        {
            return snapshotVotes?
                .GroupBy(x => x.Space?.Id)
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .Select(x => new SnapshotProtocolVoteData(
                    x.Key!,
                    x.FirstOrDefault()?.Space?.Name ?? string.Empty,
                    x.FirstOrDefault()?.Space?.Avatar,
                    x.FirstOrDefault()?.Space?.About,
                    x.Sum(y => y.Vp ?? 0),
                    x.Count()));
        }

        /// <summary>
        /// Get Snapshot protocol proposals data.
        /// </summary>
        /// <param name="snapshotProposals">Collection of <see cref="SnapshotProposal"/>.</param>
        /// <returns>Returns collection of <see cref="SnapshotProtocolProposalData"/>.</returns>
        public static IEnumerable<SnapshotProtocolProposalData>? SnapshotProtocolProposalsData(
            IEnumerable<SnapshotProposal>? snapshotProposals)
        {
            return snapshotProposals?
                .GroupBy(x => x.Space?.Id)
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .Select(x => new SnapshotProtocolProposalData(
                    x.Key!,
                    x.FirstOrDefault()?.Space?.Name ?? string.Empty,
                    x.FirstOrDefault()?.Space?.Avatar,
                    x.FirstOrDefault()?.Space?.About,
                    x.Sum(y => y.Votes)));
        }

        /// <summary>
        /// Blockchain wallet Snapshot protocol stats filler.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal new Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }
    }
}