// ------------------------------------------------------------------------------------------------------
// <copyright file="ICoingeckoService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Coingecko.Interfaces.Models;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Coingecko.Interfaces
{
    /// <summary>
    /// Coingecko service.
    /// </summary>
    public interface ICoingeckoService :
        IInfrastructureService
    {
        /// <summary>
        /// Get the token data by contract.
        /// </summary>
        /// <param name="assetPlatformId" example="celo">The asset platform id.</param>
        /// <param name="tokenContractAddress">The token contract address.</param>
        /// <returns>Returns <see cref="CoingeckoTokenContractDataResponse"/>.</returns>
        Task<CoingeckoTokenContractDataResponse?> GetTokenDataAsync(
            string assetPlatformId,
            string tokenContractAddress);

        /// <summary>
        /// Get USD balance oh the token.
        /// </summary>
        /// <param name="balance">The token balance in native currency.</param>
        /// <param name="tokenId">The token id.</param>
        /// <returns>Returns USD balance oh the token.</returns>
        public Task<decimal> GetUsdBalanceAsync(decimal balance, string tokenId);
    }
}