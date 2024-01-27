// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Models;

namespace Nomis.Snapshot.Interfaces.Responses
{
    /// <summary>
    /// Snapshot data.
    /// </summary>
    public class SnapshotData
    {
        /// <summary>
        /// The collection of <see cref="SnapshotVote"/>.
        /// </summary>
        public IList<SnapshotVote>? Votes { get; set; }

        /// <summary>
        /// The collection of <see cref="SnapshotVote"/>.
        /// </summary>
        public IList<SnapshotProposal>? Proposals { get; set; }
    }
}