// ------------------------------------------------------------------------------------------------------
// <copyright file="Web3StorageUploadResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.IPFS.Responses
{
    /// <summary>
    /// Web3 Storage upload response.
    /// </summary>
    public class Web3StorageUploadResponse
    {
        /// <summary>
        /// CID.
        /// </summary>
        [JsonPropertyName("cid")]
        public string? Cid { get; set; }

        /// <summary>
        /// Car CID.
        /// </summary>
        [JsonPropertyName("carCid")]
        public string? CarCid { get; set; }
    }
}