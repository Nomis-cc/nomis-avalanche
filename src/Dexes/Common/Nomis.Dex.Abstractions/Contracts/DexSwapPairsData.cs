// ------------------------------------------------------------------------------------------------------
// <copyright file="DexSwapPairsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// DEX swap pairs data.
    /// </summary>
    public class DexSwapPairsData
    {
        /// <summary>
        /// Initialize <see cref="DexSwapPairsData"/>.
        /// </summary>
        /// <param name="dexDescriptor">DEX descriptor.</param>
        /// <param name="swapPairs">DEX swap pairs.</param>
        public DexSwapPairsData(
            DexDescriptor dexDescriptor,
            List<ISwapPairData> swapPairs)
        {
            DexDescriptor = dexDescriptor;
            SwapPairs = swapPairs;
        }

        /// <summary>
        /// DEX descriptor.
        /// </summary>
        public DexDescriptor DexDescriptor { get; }

        /// <summary>
        /// DEX swap pairs count.
        /// </summary>
        public int SwapPairsCount => SwapPairs.Count;

        /// <summary>
        /// DEX swap pairs.
        /// </summary>
        public IList<ISwapPairData> SwapPairs { get; }
    }
}