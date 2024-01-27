// ------------------------------------------------------------------------------------------------------
// <copyright file="BlockchainType.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Enums
{
    /// <summary>
    /// Blockchain type.
    /// </summary>
    public enum BlockchainType :
        byte
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// EVM-based.
        /// </summary>
        EVM,

        /// <summary>
        /// Solana.
        /// </summary>
        Solana,

        /// <summary>
        /// Cosmos.
        /// </summary>
        Cosmos,

        /// <summary>
        /// Dogecoin.
        /// </summary>
        Dogecoin,

        /// <summary>
        /// Waves.
        /// </summary>
        Waves,

        /// <summary>
        /// Tron.
        /// </summary>
        Tron,

        /// <summary>
        /// Aeternity.
        /// </summary>
        Aeternity,

        /// <summary>
        /// Algorand.
        /// </summary>
        Algorand,

        /// <summary>
        /// Aptos.
        /// </summary>
        Aptos,

        /// <summary>
        /// Concordium.
        /// </summary>
        Concordium,

        /// <summary>
        /// Flow.
        /// </summary>
        Flow,

        /// <summary>
        /// Hedera.
        /// </summary>
        Hedera,

        /// <summary>
        /// Near.
        /// </summary>
        Near,

        /// <summary>
        /// PiNetwork.
        /// </summary>
        PiNetwork,

        /// <summary>
        /// Ripple.
        /// </summary>
        Ripple,

        /// <summary>
        /// TON.
        /// </summary>
        TON,

        /// <summary>
        /// Other.
        /// </summary>
        Other,

        /// <summary>
        /// Starknet.
        /// </summary>
        Starknet
    }
}