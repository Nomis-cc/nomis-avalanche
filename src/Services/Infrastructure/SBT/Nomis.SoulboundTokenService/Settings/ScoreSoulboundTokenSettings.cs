// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoreSoulboundTokenSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.SoulboundTokenService.Models;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Enums;

// ReSharper disable InconsistentNaming
namespace Nomis.SoulboundTokenService.Settings
{
    /// <summary>
    /// Score soulbound token settings.
    /// </summary>
    public class ScoreSoulboundTokenSettings :
        ISettings
    {
        /// <summary>
        /// Token image API base URL.
        /// </summary>
        public string? TokenImageApiBaseUrl { get; init; }

        /// <summary>
        /// Token signature API base URL.
        /// </summary>
        public string? SignatureApiBaseUrl { get; init; }

        /// <summary>
        /// Metadata token name.
        /// </summary>
        public string? MetadataTokenName { get; init; }

        /// <summary>
        /// Metadata token description.
        /// </summary>
        public string? MetadataTokenDescription { get; init; }

        /// <summary>
        /// Metadata token external URL.
        /// </summary>
        public string? MetadataTokenExternalUrl { get; init; }

        /// <summary>
        /// ONFT metadata token name.
        /// </summary>
        public string? ONFTMetadataTokenName { get; init; }

        /// <summary>
        /// ONFT metadata token description.
        /// </summary>
        public string? ONFTMetadataTokenDescription { get; init; }

        /// <summary>
        /// ONFT metadata token external URL.
        /// </summary>
        public string? ONFTMetadataTokenExternalUrl { get; init; }

        /// <summary>
        /// Token data by score type.
        /// </summary>
        public IDictionary<ScoreType, IDictionary<ulong, SoulboundTokenData>> TokenData { get; init; } = new Dictionary<ScoreType, IDictionary<ulong, SoulboundTokenData>>();
    }
}