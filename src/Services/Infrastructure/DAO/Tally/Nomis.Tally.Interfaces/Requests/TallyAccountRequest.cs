// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyAccountRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Tally.Interfaces.Requests
{
    /// <summary>
    /// Tally account request.
    /// </summary>
    public class TallyAccountRequest
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        /// <example>0x0000000000000000000000000000000000000000</example>
        public string Address { get; set; } = "0x0000000000000000000000000000000000000000";

        /// <summary>
        /// Blockchain id.
        /// </summary>
        /// <example>1</example>
        public ulong ChainId { get; set; }
    }
}