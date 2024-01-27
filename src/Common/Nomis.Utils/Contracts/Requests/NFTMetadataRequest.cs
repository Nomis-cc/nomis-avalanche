// ------------------------------------------------------------------------------------------------------
// <copyright file="NFTMetadataRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.NFT;

namespace Nomis.Utils.Contracts.Requests
{
    /// <summary>
    /// NFT metadata request.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class NFTMetadataRequest
    {
        /// <summary>
        /// Image.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Attributes.
        /// </summary>
        public IList<NFTTrait> Attributes { get; set; } = new List<NFTTrait>();

        /// <summary>
        /// Optional name.
        /// </summary>
        /// <remarks>
        /// If empty uses from settings.
        /// </remarks>
        public string? Name { get; set; }

        /// <summary>
        /// Optional description.
        /// </summary>
        /// <remarks>
        /// If empty uses from settings.
        /// </remarks>
        public string? Description { get; set; }

        /// <summary>
        /// Optional external URL.
        /// </summary>
        /// <remarks>
        /// If empty uses from settings.
        /// </remarks>
        public string? ExternalUrl { get; set; }
    }
}