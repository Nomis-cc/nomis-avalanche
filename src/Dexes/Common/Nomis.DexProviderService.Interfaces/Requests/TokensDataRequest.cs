// ------------------------------------------------------------------------------------------------------
// <copyright file="TokensDataRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces.Enums;

namespace Nomis.DexProviderService.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the list of tokens from supported tokens providers by blockchain.
    /// </summary>
    public class TokensDataRequest
    {
        /// <summary>
        /// Blockchain.
        /// </summary>
        /// <example>0</example>
        public Chain Blockchain { get; set; } = Chain.None;

        /// <summary>
        /// List of ignored tokens providers.
        /// </summary>
        public IList<TokensProvider> IgnoredProviderIds { get; set; } = new List<TokensProvider>();

        /// <summary>
        /// List of included tokens providers.
        /// </summary>
        /// <remarks>
        /// Include all if empty.
        /// </remarks>
        public IList<TokensProvider> IncludedProviderIds { get; set; } = new List<TokensProvider>();

        /// <summary>
        /// Include universal token lists (with multiple blockchains).
        /// </summary>
        /// <example>true</example>
        public bool IncludeUniversalTokenLists { get; set; } = true;

        /// <summary>
        /// Get data from cache.
        /// </summary>
        /// <example>false</example>
        public bool FromCache { get; set; } = false;
    }
}