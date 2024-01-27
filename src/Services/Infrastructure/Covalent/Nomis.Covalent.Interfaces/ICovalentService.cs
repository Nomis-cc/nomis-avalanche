// ------------------------------------------------------------------------------------------------------
// <copyright file="ICovalentService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Covalent.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Covalent.Interfaces
{
    /// <summary>
    /// Covalent service.
    /// </summary>
    public interface ICovalentService :
        IInfrastructureService
    {
        /// <summary>
        /// Get all blockchains supported by Covalent.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns balances of tokens for wallet address.</returns>
        public Task<CovalentChainsResponse> AllChainsAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get balances of tokens for wallet address.
        /// </summary>
        /// <param name="chain">Covalent blockchain internal id.</param>
        /// <param name="address">Wallet address.</param>
        /// <param name="fetchNft">Fetch NFTs.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns balances of tokens for wallet address.</returns>
        public Task<CovalentTokenBalancesResponse> WalletBalancesAsync(
            string chain,
            string address,
            bool fetchNft = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get balance of NFT tokens for wallet address.
        /// </summary>
        /// <param name="chain">Covalent blockchain internal id.</param>
        /// <param name="address">Wallet address.</param>
        /// <param name="noSpam">No spam.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns balance of NFT tokens for wallet address.</returns>
        public Task<CovalentNftsResponse> WalletNftsAsync(
            string chain,
            string address,
            bool noSpam = true,
            CancellationToken cancellationToken = default);
    }
}