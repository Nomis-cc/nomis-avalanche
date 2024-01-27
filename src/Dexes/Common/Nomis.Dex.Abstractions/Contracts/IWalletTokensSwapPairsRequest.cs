// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTokensSwapPairsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// Wallet DEX tokens swap pairs request.
    /// </summary>
    public interface IWalletTokensSwapPairsRequest
    {
        /// <summary>
        /// Get wallet hold tokens swap pairs from supported DEXes.
        /// </summary>
        public bool GetTokensSwapPairs { get; set; }

        /// <summary>
        /// The number of first swap pairs to receive.
        /// </summary>
        public int FirstSwapPairs { get; set; }

        /// <summary>
        /// The number of skipped swap pairs to receive.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Get data from cache.
        /// </summary>
        public bool FromCache { get; set; }
    }
}