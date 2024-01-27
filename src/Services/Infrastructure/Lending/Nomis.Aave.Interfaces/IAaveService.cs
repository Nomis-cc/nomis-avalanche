// ------------------------------------------------------------------------------------------------------
// <copyright file="IAaveService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Aave.Interfaces.Enums;
using Nomis.Aave.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Aave.Interfaces
{
    /// <summary>
    /// Aave Protocol service.
    /// </summary>
    public interface IAaveService :
        IInfrastructureService
    {
        /// <summary>
        /// Get Aave user account data by wallet address.
        /// </summary>
        /// <param name="blockchain">Blockchain.</param>
        /// <param name="address">Wallet address</param>
        /// <returns>Returns <see cref="AaveUserAccountDataResponse"/>.</returns>
        public Task<Result<AaveUserAccountDataResponse>> GetAaveUserAccountDataAsync(
            AaveChain blockchain,
            string address);
    }
}