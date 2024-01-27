// ------------------------------------------------------------------------------------------------------
// <copyright file="ScorerType.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.Discord.Bots.Interfaces.Enums
{
    /// <summary>
    /// Scorer type.
    /// </summary>
    public enum ScorerType :
        byte
    {
        /// <summary>
        /// Multichain reputation score.
        /// </summary>
        Multichain,

        /// <summary>
        /// zkSync Era reputation score.
        /// </summary>
        ZkSync,

        /// <summary>
        /// L0 reputation score.
        /// </summary>
        LayerZero,

        /// <summary>
        /// DeFi reputation score by Rubic.
        /// </summary>
        DeFi,

        /// <summary>
        /// Polygon zkEVM reputation score.
        /// </summary>
        ZkEVM,

        /// <summary>
        /// Linea reputation score.
        /// </summary>
        Linea,

        /// <summary>
        /// Reputation score by Strateg.
        /// </summary>
        Strateg,

        /// <summary>
        /// Mantle reputation score.
        /// </summary>
        Mantle,

        /// <summary>
        /// opBNB reputation score.
        /// </summary>
        OpBnb
    }
}