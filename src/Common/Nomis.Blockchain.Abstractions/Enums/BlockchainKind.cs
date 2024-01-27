// ------------------------------------------------------------------------------------------------------
// <copyright file="BlockchainKind.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Enums
{
    /// <summary>
    /// Blockchain kind.
    /// </summary>
    public enum BlockchainKind :
        byte
    {
        /// <summary>
        /// Mainnet.
        /// </summary>
        Mainnet = 0,

        /// <summary>
        /// Testnet.
        /// </summary>
        Testnet = 1
    }
}