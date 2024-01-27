// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectHandleRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.CyberConnect.Interfaces.Requests
{
    /// <summary>
    /// CyberConnect handle request.
    /// </summary>
    public class CyberConnectHandleRequest
    {
        /// <summary>
        /// Address.
        /// </summary>
        /// <example>0x7C04786F04c522ca664Bb8b6804E0d182eec505F</example>
        public string? Address { get; set; } = "0x7C04786F04c522ca664Bb8b6804E0d182eec505F";
    }
}