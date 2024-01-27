// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotProtocolVoteData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Nomis.Snapshot.Interfaces.Models
{
    /// <summary>
    /// Snapshot protocol vote data.
    /// </summary>
    public class SnapshotProtocolVoteData
    {
        /// <summary>
        /// Initialize <see cref="SnapshotProtocolVoteData"/>.
        /// </summary>
        /// <param name="spaceId">The Snapshot protocol space identifier.</param>
        /// <param name="spaceName">The Snapshot protocol space name.</param>
        /// <param name="spaceAvatar">The Snapshot protocol space avatar.</param>
        /// <param name="spaceAbout">The Snapshot protocol space about</param>
        /// <param name="usedVotingPower">Used voting power for the space.</param>
        /// <param name="count">Total votes count for the space.</param>
        public SnapshotProtocolVoteData(
            string spaceId,
            string spaceName,
            string? spaceAvatar,
            string? spaceAbout,
            decimal usedVotingPower,
            int count)
        {
            SpaceId = spaceId;
            SpaceName = spaceName;
            SpaceAvatar = spaceAvatar;
            SpaceAbout = spaceAbout;
            UsedVotingPower = usedVotingPower;
            Count = count;
        }

        /// <summary>
        /// The Snapshot protocol space identifier.
        /// </summary>
        /// <example>theopendao.eth</example>
        [Display(Description = "The Snapshot protocol space identifier", GroupName = "value")]
        public string SpaceId { get; }

        /// <summary>
        /// The Snapshot protocol space name.
        /// </summary>
        /// <example>OpenDAO</example>
        [Display(Description = "The Snapshot protocol space name", GroupName = "value")]
        public string SpaceName { get; }

        /// <summary>
        /// The Snapshot protocol space avatar.
        /// </summary>
        /// <example>ipfs://Qmf89h2b8Xc3D32PnXhvePXzvQnQ8c89udRmtjk6vFtt9C</example>
        [Display(Description = "The Snapshot protocol space avatar", GroupName = "value")]
        public string? SpaceAvatar { get; }

        /// <summary>
        /// The Snapshot protocol space about.
        /// </summary>
        /// <example>Yam</example>
        [Display(Description = "The Snapshot protocol space about data", GroupName = "value")]
        public string? SpaceAbout { get; }

        /// <summary>
        /// Used voting power for the space.
        /// </summary>
        /// <example>9.62</example>
        [Display(Description = "Used voting power in the Snapshot protocol for this space", GroupName = "number")]
        public decimal UsedVotingPower { get; }

        /// <summary>
        /// Total votes count for the space.
        /// </summary>
        /// <example>10</example>
        [Display(Description = "Total votes count in the Snapshot protocol for this space", GroupName = "number")]
        public int Count { get; }
    }
}