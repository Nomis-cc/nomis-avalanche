// ------------------------------------------------------------------------------------------------------
// <copyright file="INFTRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Contracts.Requests
{
    /// <summary>
    /// NFT request.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface INFTRequest
    {
        /// <summary>
        /// To address.
        /// </summary>
        public string? To { get; set; }

        /// <summary>
        /// Nonce.
        /// </summary>
        public ulong Nonce { get; set; }

        /// <summary>
        /// Time to the verifying deadline.
        /// </summary>
        public ulong Deadline { get; set; }

        /// <summary>
        /// Token metadata IPFS URL.
        /// </summary>
        public string? MetadataUrl { get; set; }
    }
}