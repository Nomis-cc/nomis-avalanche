// ------------------------------------------------------------------------------------------------------
// <copyright file="BlacklistType.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Enums
{
    /// <summary>
    /// Blacklist type.
    /// </summary>
    public enum BlacklistType :
        byte
    {
        /// <summary>
        /// Wallets that violated the rules of the service or tried to circumvent the restrictions.
        /// </summary>
        Breaker,

        /// <summary>
        /// Zigzag sybil source.
        /// </summary>
        /// <remarks>
        /// <see href="https://twitter.com/zigzagkedar/status/1630093636733124608?t=LCJg74Bxu1Og4BSHQu8UgQ"/>
        /// </remarks>
        ZigzagSybil,

        /// <summary>
        /// Hop Protocol source.
        /// </summary>
        /// <see href="https://github.com/hop-protocol/hop-airdrop/blob/master/src/data/eliminatedSybilAttackers.csv"/>
        HopProtocolSybil,
    }
}