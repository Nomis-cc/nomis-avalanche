// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFSUploadFileRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

using Ipfs.CoreApi;

namespace Nomis.IPFS.Interfaces.Requests
{
    /// <summary>
    /// Upload file to IPFS request.
    /// </summary>
    public class IPFSUploadFileRequest
    {
        /// <summary>
        /// File name.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// File content.
        /// </summary>
        public Stream? FileContent { get; set; }

        /// <summary>
        /// Add file options
        /// </summary>
        public AddFileOptions Options { get; set; } = new();
    }
}