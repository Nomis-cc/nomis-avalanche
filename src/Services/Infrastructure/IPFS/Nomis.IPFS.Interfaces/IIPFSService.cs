// ------------------------------------------------------------------------------------------------------
// <copyright file="IIPFSService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

using Ipfs.Http;
using Nomis.IPFS.Interfaces.Requests;
using Nomis.IPFS.Interfaces.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.IPFS.Interfaces
{
    /// <summary>
    /// IPFS service.
    /// </summary>
    public interface IIPFSService :
        IInfrastructureService
    {
        /// <inheritdoc cref="IIPFSSettings"/>
        public IIPFSSettings Settings { get; }

        /// <summary>
        /// <see cref="IpfsClient"/>.
        /// </summary>
        public IpfsClient? IPFSClient { get; }

        /// <summary>
        /// Download file from IPFS by id.
        /// </summary>
        /// <param name="fileId">File id.</param>
        /// <returns>Returns downloaded file data.</returns>
        public Task<Result<byte[]>> DownloadFileAsync(
            string fileId);

        /// <summary>
        /// Upload file to IPFS.
        /// </summary>
        /// <param name="request">Upload file to IPFS request.</param>
        /// <returns>Returns uploaded to IPFS file id.</returns>
        public Task<Result<string?>> UploadFileAsync(
            IPFSUploadFileRequest request);
    }
}