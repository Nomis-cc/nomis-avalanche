// ------------------------------------------------------------------------------------------------------
// <copyright file="ISnapshotService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Models;
using Nomis.Snapshot.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Snapshot.Interfaces
{
    /// <summary>
    /// Service for interaction with Snapshot API.
    /// </summary>
    public interface ISnapshotService :
        IInfrastructureService
    {
        /// <summary>
        /// Get the Snapshot votes by voter.
        /// </summary>
        /// <param name="request">Snapshot votes request.</param>
        /// <returns>Returns Snapshot votes by voter.</returns>
        public Task<Result<List<SnapshotVote>?>> GetSnapshotVotesAsync(GetSnapshotVotesRequest request);

        /// <summary>
        /// Get the Snapshot proposals by author.
        /// </summary>
        /// <param name="request">Snapshot proposals request.</param>
        /// <returns>Returns Snapshot proposals by author.</returns>
        public Task<Result<List<SnapshotProposal>?>> GetSnapshotProposalsAsync(GetSnapshotProposalsRequest request);
    }
}