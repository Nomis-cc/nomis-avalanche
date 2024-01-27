// ------------------------------------------------------------------------------------------------------
// <copyright file="MigrateMessage.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nomis.SoulboundTokenService.Models
{
    /// <summary>
    /// Migrate message.
    /// </summary>
    [Struct(nameof(MigrateMessage))]
    public class MigrateMessage
    {
        /// <summary>
        /// To address.
        /// </summary>
        [Parameter("address", "to", 1)]
        public string? To { get; set; }

        /// <summary>
        /// Nonce.
        /// </summary>
        [Parameter("uint256", "nonce", 2)]
        public BigInteger Nonce { get; set; }

        /// <summary>
        /// Deadline.
        /// </summary>
        [Parameter("uint256", "deadline", 3)]
        public BigInteger Deadline { get; set; }

        /// <summary>
        /// Referral code.
        /// </summary>
        [Parameter("bytes32", "referralCode", 4)]
        public byte[]? ReferralCode { get; set; }

        /// <summary>
        /// Referrer code.
        /// </summary>
        [Parameter("bytes32", "referrerCode", 5)]
        public byte[]? ReferrerCode { get; set; }
    }
}