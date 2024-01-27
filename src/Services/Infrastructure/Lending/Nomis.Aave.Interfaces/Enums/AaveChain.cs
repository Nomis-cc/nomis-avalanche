// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveChain.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Aave.Interfaces.Enums
{
    /// <summary>
    /// Aave supported blockchain.
    /// </summary>
    public enum AaveChain
    {
        /// <summary>
        /// Ethereum Mainnet.
        /// </summary>
        Ethereum = 1,

        /// <summary>
        /// Optimism.
        /// </summary>
        Optimism = 10,

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
        /// Avalanche C-Chain.
        /// </summary>
        Avalanche = 43114
    }
}