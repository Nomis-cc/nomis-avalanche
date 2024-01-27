// ------------------------------------------------------------------------------------------------------
// <copyright file="IBalanceCheckerService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.BalanceChecker.Interfaces.Contracts;
using Nomis.BalanceChecker.Interfaces.Requests;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.BalanceChecker.Interfaces
{
    /// <summary>
    /// Balance checker service.
    /// </summary>
    public interface IBalanceCheckerService :
        IInfrastructureService
    {
        /// <summary>
        /// Get token balances by given wallet address and blockchain.
        /// </summary>
        /// <param name="request">Token balances request.</param>
        /// <param name="tokenBalanceFunc">Function for getting token balance by wallet address and token contract address.</param>
        /// <returns>Returns a collection of <see cref="BalanceCheckerTokenInfo"/>.</returns>
        public Task<Result<IEnumerable<BalanceCheckerTokenInfo>>> TokenBalancesAsync(
            TokenBalancesRequest request,
            Func<string, string, Task<TokenDataBalance?>>? tokenBalanceFunc = null);
    }
}