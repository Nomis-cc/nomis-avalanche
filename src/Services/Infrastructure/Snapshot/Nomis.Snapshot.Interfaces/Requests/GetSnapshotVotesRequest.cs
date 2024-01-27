// ------------------------------------------------------------------------------------------------------
// <copyright file="GetSnapshotVotesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Snapshot.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the Snapshot votes.
    /// </summary>
    public class GetSnapshotVotesRequest
    {
        /// <summary>
        /// Order by.
        /// </summary>
        /// <example>vp</example>
        public string? OrderBy { get; set; } = "vp";

        /// <summary>
        /// Order direction.
        /// </summary>
        /// <example>desc</example>
        public string? OrderDirection { get; set; } = "desc";

        /// <summary>
        /// First items count.
        /// </summary>
        /// <example>1000</example>
        public int First { get; set; } = 1000;

        /// <summary>
        /// Skip items count.
        /// </summary>
        /// <example>0</example>
        public int Skip { get; set; } = 0;

        /// <summary>
        /// Voter.
        /// </summary>
        /// <example>0x653d63E4F2D7112a19f5Eb993890a3F27b48aDa5</example>
        public string? Voter { get; set; }

        /// <summary>
        /// Blockchain id.
        /// </summary>
        /// <example>1</example>
        public ulong ChainId { get; set; }

        /// <summary>
        /// Spaces.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// [""]
        /// ]]>
        /// </example>
        public IList<string> Spaces { get; set; } = new List<string>();
    }
}