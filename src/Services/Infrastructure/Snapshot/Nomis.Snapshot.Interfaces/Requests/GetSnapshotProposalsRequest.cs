// ------------------------------------------------------------------------------------------------------
// <copyright file="GetSnapshotProposalsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Snapshot.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the Snapshot proposals.
    /// </summary>
    public class GetSnapshotProposalsRequest
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
        /// Author.
        /// </summary>
        /// <example>0x9Ebc8AD4011C7f559743Eb25705CCF5A9B58D0bc</example>
        public string? Author { get; set; }

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