// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectSubscribingsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.CyberConnect.Interfaces.Requests
{
    /// <summary>
    /// CyberConnect subscribings request.
    /// </summary>
    public class CyberConnectSubscribingsRequest
    {
        /// <summary>
        /// Address.
        /// </summary>
        /// <example>0x370CA01D7314e3EEa59d57E343323bB7e9De24C6</example>
        public string? Address { get; set; } = "0x370CA01D7314e3EEa59d57E343323bB7e9De24C6";

        /// <summary>
        /// Chain Id.
        /// </summary>
        /// <example>1</example>
        public ulong ChainId { get; set; } = 1;

        /// <summary>
        /// First items count.
        /// </summary>
        /// <example>1000</example>
        public int First { get; set; } = 1000;

        /// <summary>
        /// After cursor.
        /// </summary>
        /// <example>null</example>
        public string? After { get; set; }
    }
}