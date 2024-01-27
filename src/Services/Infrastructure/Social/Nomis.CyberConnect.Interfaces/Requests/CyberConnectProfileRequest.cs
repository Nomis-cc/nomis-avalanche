// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectProfileRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.CyberConnect.Interfaces.Requests
{
    /// <summary>
    /// CyberConnect Protocol profile request.
    /// </summary>
    public class CyberConnectProfileRequest
    {
        /// <summary>
        /// Handle.
        /// </summary>
        /// <example>ryan</example>
        public string? Handle { get; set; } = "ryan";

        /// <summary>
        /// Chain Id.
        /// </summary>
        /// <example>1</example>
        public ulong ChainId { get; set; } = 1;
    }
}