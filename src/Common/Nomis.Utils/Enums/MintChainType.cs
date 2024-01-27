// ------------------------------------------------------------------------------------------------------
// <copyright file="MintChainType.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Enums
{
    /// <summary>
    /// Mint blockchain type.
    /// </summary>
    public enum MintChainType :
        byte
    {
        /// <summary>
        /// Mainnet.
        /// </summary>
        Mainnet = 0,

        /// <summary>
        /// Testnet.
        /// </summary>
        Testnet
    }
}