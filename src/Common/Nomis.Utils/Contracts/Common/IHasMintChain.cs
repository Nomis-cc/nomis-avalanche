// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasMintChain.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Enums;

namespace Nomis.Utils.Contracts.Common
{
    /// <summary>
    /// Has properties for mint chain.
    /// </summary>
    public interface IHasMintChain
    {
        /// <summary>
        /// Blockchain in which the score will be minted.
        /// </summary>
        /// <example>0</example>
        public MintChain MintChain { get; set; }

        /// <summary>
        /// Blockchain type in which the score will be minted.
        /// </summary>
        /// <remarks>
        /// Uses only if `MintChain = 0 (Native)`.
        /// </remarks>
        /// <example>0</example>
        public MintChainType MintBlockchainType { get; set; }
    }
}