// ------------------------------------------------------------------------------------------------------
// <copyright file="PinataUploadResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.IPFS.Responses
{
    /// <summary>
    /// Pinata upload response.
    /// </summary>
    public class PinataUploadResponse
    {
        /// <summary>
        /// IPFS file hash.
        /// </summary>
        public string? IpfsHash { get; set; }

        /// <summary>
        /// Pin size.
        /// </summary>
        public int PinSize { get; set; }

        /// <summary>
        /// Timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}