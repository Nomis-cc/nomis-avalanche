// ------------------------------------------------------------------------------------------------------
// <copyright file="SwapPairDataResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.Dex.Abstractions.Contracts;

namespace Nomis.Dex.Abstractions.Responses
{
    /// <summary>
    /// DEX swap pair data response.
    /// </summary>
    public class SwapPairDataResponse
    {
        /// <summary>
        /// Initialize <see cref="SwapPairDataResponse"/>.
        /// </summary>
        /// <param name="dexSwapPairs">List of DEX swap pair data.</param>
        /// <param name="blockchainDescriptor">Blockchain descriptor.</param>
        public SwapPairDataResponse(
            List<DexSwapPairsData> dexSwapPairs,
            IBlockchainDescriptor? blockchainDescriptor = null)
        {
            DexSwapPairs = dexSwapPairs;
            BlockchainDescriptor = blockchainDescriptor;
        }

        /// <summary>
        /// Blockchain descriptor.
        /// </summary>
        public IBlockchainDescriptor? BlockchainDescriptor { get; }

        /// <summary>
        /// List of DEX swap pairs data.
        /// </summary>
        public IList<DexSwapPairsData> DexSwapPairs { get; }
    }
}