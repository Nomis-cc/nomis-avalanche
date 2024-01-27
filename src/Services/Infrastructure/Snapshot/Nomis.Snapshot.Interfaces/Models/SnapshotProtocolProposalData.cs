// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotProtocolProposalData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Nomis.Snapshot.Interfaces.Models
{
    /// <summary>
    /// Snapshot protocol proposal data.
    /// </summary>
    public class SnapshotProtocolProposalData
    {
        /// <summary>
        /// Initialize <see cref="SnapshotProtocolProposalData"/>.
        /// </summary>
        /// <param name="spaceId">The Snapshot protocol space identifier.</param>
        /// <param name="spaceName">The Snapshot protocol space name.</param>
        /// <param name="spaceAvatar">The Snapshot protocol space avatar.</param>
        /// <param name="spaceAbout">The Snapshot protocol space about</param>
        /// <param name="votes">Votes for the space proposal.</param>
        public SnapshotProtocolProposalData(
            string spaceId,
            string spaceName,
            string? spaceAvatar,
            string? spaceAbout,
            int votes)
        {
            SpaceId = spaceId;
            SpaceName = spaceName;
            SpaceAvatar = spaceAvatar;
            SpaceAbout = spaceAbout;
            Votes = votes;
        }

        /// <summary>
        /// The Snapshot protocol space identifier.
        /// </summary>
        /// <example>yam.eth</example>
        [Display(Description = "The Snapshot protocol space identifier", GroupName = "value")]
        public string SpaceId { get; }

        /// <summary>
        /// The Snapshot protocol space name.
        /// </summary>
        /// <example>Yam</example>
        [Display(Description = "The Snapshot protocol space name", GroupName = "value")]
        public string SpaceName { get; }

        /// <summary>
        /// The Snapshot protocol space avatar.
        /// </summary>
        /// <example>ipfs://QmWe6sMgURBDA3cadz6s59MDjExHtNXXsFBvVG84PMGdhU</example>
        [Display(Description = "The Snapshot protocol space avatar", GroupName = "value")]
        public string? SpaceAvatar { get; }

        /// <summary>
        /// The Snapshot protocol space about.
        /// </summary>
        /// <example>Yam</example>
        [Display(Description = "The Snapshot protocol space about data", GroupName = "value")]
        public string? SpaceAbout { get; }

        /// <summary>
        /// Votes for the space proposal.
        /// </summary>
        /// <example>31</example>
        [Display(Description = "Votes for the Snapshot protocol proposal in this space", GroupName = "number")]
        public int Votes { get; }
    }
}