// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasSwapPairCacheKeys.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// Has keys for swap pairs data caching.
    /// </summary>
    public interface IHasSwapPairCacheKeys
    {
        /// <summary>
        /// The cache key for storing last update data time.
        /// </summary>
        public string LastUpdateCacheKey { get; }

        /// <summary>
        /// The cache key for storing swap pairs data.
        /// </summary>
        public string SwapPairDataCacheKey { get; }
    }
}