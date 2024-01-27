// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectEssencesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.CyberConnect.Interfaces.Requests
{
    /// <summary>
    /// CyberConnect essences request.
    /// </summary>
    public class CyberConnectEssencesRequest
    {
        /// <summary>
        /// Handle.
        /// </summary>
        /// <example>ryan</example>
        public string? Handle { get; set; } = "ryan";

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