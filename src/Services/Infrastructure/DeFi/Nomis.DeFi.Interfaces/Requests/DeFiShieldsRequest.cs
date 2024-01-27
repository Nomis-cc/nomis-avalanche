// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiShieldsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.DeFi.Interfaces.Requests
{
    /// <summary>
    /// De.Fi shields request.
    /// </summary>
    public class DeFiShieldsRequest
    {
        /// <summary>
        /// Contracts addresses.
        /// </summary>
        public IList<string> Addresses { get; set; } = new List<string>();

        /// <summary>
        /// Chain id.
        /// </summary>
        /// <example>1</example>
        public int ChainId { get; set; } = 1;
    }
}