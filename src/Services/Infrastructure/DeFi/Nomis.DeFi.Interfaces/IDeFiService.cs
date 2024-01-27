// ------------------------------------------------------------------------------------------------------
// <copyright file="IDeFiService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.DeFi.Interfaces.Models;
using Nomis.DeFi.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.DeFi.Interfaces
{
    /// <summary>
    /// De.Fi service.
    /// </summary>
    public interface IDeFiService :
        IInfrastructureService
    {
        /// <summary>
        /// Get the De.Fi chains.
        /// </summary>
        /// <returns>Returns De.Fi chains response.</returns>
        public Task<Result<IEnumerable<DeFiChainData>?>> ChainsAsync();

        /// <summary>
        /// Get the De.Fi shields.
        /// </summary>
        /// <param name="request">De.Fi shields request.</param>
        /// <returns>Returns De.Fi shield advanced response.</returns>
        public Task<Result<DeFiShieldAdvancedData?>> ShieldsAsync(
            DeFiShieldsRequest request);

        /// <summary>
        /// Get the De.Fi wallet asset balances.
        /// </summary>
        /// <param name="request">De.Fi wallet asset balances request.</param>
        /// <returns>Returns De.Fi wallet asset balances response.</returns>
        public Task<Result<DeFiAssetBalances?>> AssetBalancesAsync(
            DeFiAssetBalancesRequest request);
    }
}