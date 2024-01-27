// ------------------------------------------------------------------------------------------------------
// <copyright file="IDexProviderService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;
using Nomis.Dex.Abstractions.Responses;
using Nomis.DexProviderService.Interfaces.Contracts;
using Nomis.DexProviderService.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.DexProviderService.Interfaces
{
    /// <summary>
    /// DEX provider service.
    /// </summary>
    public interface IDexProviderService :
        IInfrastructureService
    {
        /// <summary>
        /// Get the list of stablecoins.
        /// </summary>
        /// <param name="blockchain">Blockchain.</param>
        /// <returns>Returns the list of stablecoins.</returns>
        Result<List<StableCoinData>> StablecoinsData(
            Chain blockchain = Chain.None);

        /// <summary>
        /// Get the list of supported DEX-providers.
        /// </summary>
        /// <param name="provider">DEX provider.</param>
        /// <param name="blockchain">Blockchain.</param>
        /// <param name="isEnabled">Is enabled.</param>
        /// <returns>Returns the list of supported DEX-providers.</returns>
        Result<List<DexProviderData>> ProvidersData(
            DexProvider provider = DexProvider.None,
            Chain blockchain = Chain.None,
            bool? isEnabled = null);

        /// <summary>
        /// Get the list of tokens.
        /// </summary>
        /// <param name="request">Request for getting the list of tokens from supported tokens providers by blockchain.</param>
        /// <returns>Returns the list of tokens.</returns>
        Task<Result<List<DexTokenData>>> TokensDataAsync(
            TokensDataRequest request);

        /// <summary>
        /// Get the list of all supported blockchains.
        /// </summary>
        /// <param name="type">Blockchain type.</param>
        /// <param name="isEnabled">Is enabled.</param>
        /// <returns>Returns the list of all supported blockchains.</returns>
        Result<List<IBlockchainDescriptor>> Blockchains(
            BlockchainType type = BlockchainType.None,
            bool? isEnabled = null);

        /// <summary>
        /// Get the list of swap pairs from all supported DEXes by blockchain.
        /// </summary>
        /// <param name="request">Request for getting the list of swap pairs from all supported DEXes by blockchain.</param>
        /// <returns>Returns <see cref="SwapPairDataResponse"/>.</returns>
        public Task<Result<SwapPairDataResponse>> BlockchainSwapPairsAsync(
            BlockchainSwapPairsRequest request);
    }
}