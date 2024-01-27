// ------------------------------------------------------------------------------------------------------
// <copyright file="MintChain.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.Utils.Enums
{
    /// <summary>
    /// Blockchain id in which the SBT will be minted.
    /// </summary>
    public enum MintChain :
        ulong
    {
        /// <summary>
        /// Native blockchain.
        /// </summary>
        /// <remarks>
        /// The same as the blockchain in which the score is calculated.
        /// </remarks>
        Native = 0,

        /// <summary>
        /// Optimism.
        /// </summary>
        Optimism = 10,

        /// <summary>
        /// Gnosis.
        /// </summary>
        Gnosis = 100,

        /// <summary>
        /// Polygon Mainnet.
        /// </summary>
        Polygon = 137,

        /// <summary>
        /// Fantom Opera.
        /// </summary>
        Fantom = 250,

        /// <summary>
        /// Arbitrum One.
        /// </summary>
        ArbitrumOne = 42161,

        /// <summary>
        /// Celo Mainnet.
        /// </summary>
        Celo = 42220,
    }
}