// ------------------------------------------------------------------------------------------------------
// <copyright file="SetScoreMessageV5.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nomis.SoulboundTokenService.Models
{
    /// <summary>
    /// Set score message.
    /// </summary>
    [Struct(nameof(SetScoreMessage))]
    public class SetScoreMessageV5
    {
        /// <summary>
        /// Score value.
        /// </summary>
        [Parameter("uint16", "score", 1)]
        public ushort Score { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        [Parameter("uint16", "calculationModel", 2)]
        public ushort CalculationModel { get; set; }

        /// <summary>
        /// To address.
        /// </summary>
        [Parameter("address", "to", 3)]
        public string? To { get; set; }

        /// <summary>
        /// Nonce.
        /// </summary>
        [Parameter("uint256", "nonce", 4)]
        public BigInteger Nonce { get; set; }

        /// <summary>
        /// Deadline.
        /// </summary>
        [Parameter("uint256", "deadline", 5)]
        public BigInteger Deadline { get; set; }

        /// <summary>
        /// Metadata URL hash.
        /// </summary>
        [Parameter("bytes32", "metadataUrl", 6)]
        public byte[]? MetadataUrl { get; set; }
    }
}