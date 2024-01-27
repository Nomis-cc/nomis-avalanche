// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectLikesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.CyberConnect.Interfaces.Requests
{
    /// <summary>
    /// CyberConnect likes request.
    /// </summary>
    public class CyberConnectLikesRequest
    {
        /// <summary>
        /// Address.
        /// </summary>
        /// <example>0x370CA01D7314e3EEa59d57E343323bB7e9De24C6</example>
        public string? Address { get; set; } = "0x370CA01D7314e3EEa59d57E343323bB7e9De24C6";

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