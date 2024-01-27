// ------------------------------------------------------------------------------------------------------
// <copyright file="BlockchainPlatform.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Enums
{
    /// <summary>
    /// Blockchain platform.
    /// </summary>
    public enum BlockchainPlatform :
        byte
    {
        /// <summary>
        /// Coingecko.
        /// </summary>
        Coingecko = 0,

        /// <summary>
        /// Coinmarketcap.
        /// </summary>
        Coinmarketcap,

        /// <summary>
        /// Debank.
        /// </summary>
        Debank,

        /// <summary>
        /// DefiLLama.
        /// </summary>
        DefiLLama,

        /// <summary>
        /// LayerZero (L0).
        /// </summary>
        LayerZero,

        /// <summary>
        /// De.Fi.
        /// </summary>
        DeFi,

        /// <summary>
        /// OpenSea.
        /// </summary>
        OpenSea,

        /// <summary>
        /// Covalent.
        /// </summary>
        Covalent,

        /// <summary>
        /// Llamafolio.
        /// </summary>
        Llamafolio
    }
}