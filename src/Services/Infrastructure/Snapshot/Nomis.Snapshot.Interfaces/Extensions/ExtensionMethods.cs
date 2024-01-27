// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Contracts;
using Nomis.Snapshot.Interfaces.Models;
using Nomis.Snapshot.Interfaces.Responses;
using Nomis.Utils.Contracts.Properties;

namespace Nomis.Snapshot.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get Snapshot data.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="ISnapshotService"/>.</param>
        /// <param name="request"><see cref="IWalletSnapshotProtocolRequest"/>.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <returns>Returns the Snapshot data.</returns>
        public static async Task<SnapshotData> DataAsync<TWalletRequest>(
            this ISnapshotService service,
            TWalletRequest? request,
            ulong chainId)
            where TWalletRequest : IWalletSnapshotProtocolRequest, IHasAddress
        {
            List<SnapshotVote>? snapshotVotes = null;
            List<SnapshotProposal>? snapshotProposals = null;
            if (request?.GetSnapshotProtocolData == true)
            {
                snapshotVotes = (await service.GetSnapshotVotesAsync(new()
                {
                    Voter = request.Address,
                    ChainId = chainId
                }).ConfigureAwait(false)).Data;
                snapshotProposals = (await service.GetSnapshotProposalsAsync(new()
                {
                    Author = request.Address,
                    ChainId = chainId
                }).ConfigureAwait(false)).Data;
            }

            return new SnapshotData
            {
                Votes = snapshotVotes,
                Proposals = snapshotProposals
            };
        }
    }
}