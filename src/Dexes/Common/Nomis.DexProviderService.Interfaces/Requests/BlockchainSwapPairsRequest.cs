// ------------------------------------------------------------------------------------------------------
// <copyright file="BlockchainSwapPairsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using Nomis.Dex.Abstractions.Enums;

namespace Nomis.DexProviderService.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the list of swap pairs from all supported DEXes by blockchain.
    /// </summary>
    public class BlockchainSwapPairsRequest
    {
        /// <summary>
        /// Blockchain.
        /// </summary>
        /// <example>0</example>
        public Chain Blockchain { get; set; } = Chain.None;

        /// <summary>
        /// The number of first swap pairs to receive.
        /// </summary>
        /// <example>100</example>
        [Range(typeof(int), "1", "1000")]
        public int First { get; set; } = 100;

        /// <summary>
        /// The number of skipped swap pairs to receive.
        /// </summary>
        /// <example>0</example>
        [Range(typeof(int), "0", "2147483647")]
        public int Skip { get; set; } = 0;

        /// <summary>
        /// List of ignored DEX-providers.
        /// </summary>
        public IList<DexProvider> IgnoredProviderIds { get; set; } = new List<DexProvider>();

        /// <summary>
        /// List of included DEX-providers.
        /// </summary>
        /// <remarks>
        /// Include all if empty.
        /// </remarks>
        public IList<DexProvider> IncludedProviderIds { get; set; } = new List<DexProvider>();

        /// <summary>
        /// Use token lists for getting tokens data.
        /// </summary>
        /// <example>true</example>
        public bool UseTokenLists { get; set; } = true;

        /// <summary>
        /// Include universal token lists (with multiple blockchains).
        /// </summary>
        /// <example>false</example>
        public bool IncludeUniversalTokenLists { get; set; } = false;

        /// <summary>
        /// Get swap pairs from all supported DEXes from cache.
        /// </summary>
        public bool FromCache { get; set; } = false;
    }
}