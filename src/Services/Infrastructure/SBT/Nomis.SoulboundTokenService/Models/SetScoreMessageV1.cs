// ------------------------------------------------------------------------------------------------------
// <copyright file="SetScoreMessageV1.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nomis.SoulboundTokenService.Models
{
    /// <summary>
    /// Set score message for v0.1 smart-contract.
    /// </summary>
    [Struct(nameof(SetScoreMessage))]
    public class SetScoreMessageV1
    {
        /// <summary>
        /// Score value.
        /// </summary>
        [Parameter("uint16", "score", 1)]
        public ushort Score { get; set; }

        /// <summary>
        /// To address.
        /// </summary>
        [Parameter("address", "to", 2)]
        public string? To { get; set; }

        /// <summary>
        /// Nonce.
        /// </summary>
        [Parameter("uint256", "nonce", 3)]
        public BigInteger Nonce { get; set; }

        /// <summary>
        /// Deadline.
        /// </summary>
        [Parameter("uint256", "deadline", 4)]
        public BigInteger Deadline { get; set; }
    }
}