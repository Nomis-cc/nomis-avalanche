// ------------------------------------------------------------------------------------------------------
// <copyright file="SetScoreMessageV6.cs" company="Nomis">
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
    public class SetScoreMessageV6 :
        SetScoreMessageV5
    {
        /// <summary>
        /// Blockchain id.
        /// </summary>
        [Parameter("uint256", "chainId", 7)]
        public BigInteger ChainId { get; set; }
    }
}