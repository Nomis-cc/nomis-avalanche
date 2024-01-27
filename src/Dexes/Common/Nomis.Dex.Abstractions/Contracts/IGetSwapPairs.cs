// ------------------------------------------------------------------------------------------------------
// <copyright file="IGetSwapPairs.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Enums;
using Nomis.Utils.Wrapper;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// Interface with methods for getting data of swap pairs of tokens.
    /// </summary>
    public interface IGetSwapPairs
    {
        /// <summary>
        /// Get data of all swap pairs of tokens.
        /// </summary>
        /// <param name="first">The number of first swap pairs to receive.</param>
        /// <param name="skip">The number of skipped swap pairs to receive.</param>
        /// <param name="fromCache">Get data from cache.</param>
        /// <returns>Returns data of all swap pairs of tokens.</returns>
        public Task<Result<(DexProvider, List<ISwapPairData>)>> GetAllSwapPairsAsync(
            int first = 100,
            int skip = 0,
            bool fromCache = false);

        /// <summary>
        /// Get data of all swap pairs of tokens by blockchain.
        /// </summary>
        /// <param name="blockchain">The blockchain.</param>
        /// <param name="first">The number of first swap pairs to receive.</param>
        /// <param name="skip">The number of skipped swap pairs to receive.</param>
        /// <param name="fromCache">Get data from cache.</param>
        /// <returns>Returns data of all swap pairs of tokens by blockchain for DEX provider.</returns>
        public Task<Result<(DexProvider, List<ISwapPairData>)>> GetAllSwapPairsAsync(
            Chain blockchain = Chain.None,
            int first = 100,
            int skip = 0,
            bool fromCache = false);
    }
}