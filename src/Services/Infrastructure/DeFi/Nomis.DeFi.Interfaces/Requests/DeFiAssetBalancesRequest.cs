// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiAssetBalancesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.DeFi.Interfaces.Requests
{
    /// <summary>
    /// De.Fi asset balances request.
    /// </summary>
    public class DeFiAssetBalancesRequest
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string Address { get; set; } = null!;

        /// <summary>
        /// Chain id.
        /// </summary>
        /// <example>1</example>
        public int ChainId { get; set; } = 1;
    }
}