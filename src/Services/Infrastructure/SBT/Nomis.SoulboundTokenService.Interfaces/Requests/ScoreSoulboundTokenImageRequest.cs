// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoreSoulboundTokenImageRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.SoulboundTokenService.Interfaces.Requests
{
    /// <summary>
    /// Score soulbound token image request
    /// </summary>
    public class ScoreSoulboundTokenImageRequest
    {
        /// <summary>
        /// Address.
        /// </summary>
        /// <example>0x0000000000000000000000000000000000000000</example>
        public string? Address { get; set; }

        /// <summary>
        /// The image size in pixels.
        /// </summary>
        /// <example>512</example>
        public uint Size { get; set; }

        /// <summary>
        /// Score type.
        /// </summary>
        /// <example>xdefi</example>
        public string? Type { get; set; }

        /// <summary>
        /// Score value.
        /// </summary>
        /// <example>14</example>
        public byte Score { get; set; }

        /// <summary>
        /// Blockchain id in which the score was calculated.
        /// </summary>
        /// <example>1</example>
        public ulong? ChainId { get; set; }
    }
}