// ------------------------------------------------------------------------------------------------------
// <copyright file="WalletStatsRequestExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts;
using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Extensions
{
    /// <summary>
    /// <see cref="WalletStatsRequest"/> extension methods.
    /// </summary>
    public static class WalletStatsRequestExtensions
    {
        /// <summary>
        /// Get blockchain id for calculation signature.
        /// </summary>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="settings"><see cref="IBlockchainSettings"/>.</param>
        /// <returns>Returns blockchain id for calculation signature.</returns>
        public static ulong? GetChainId(
            this WalletStatsRequest request,
            IBlockchainSettings settings)
        {
            return request.GetChainId(settings.BlockchainDescriptors);
        }

        /// <summary>
        /// Get blockchain id for calculation signature.
        /// </summary>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="blockchainDescriptors">Blockchain descriptors.</param>
        /// <returns>Returns blockchain id for calculation signature.</returns>
        public static ulong? GetChainId(
            this WalletStatsRequest request,
            IDictionary<BlockchainKind, BlockchainDescriptor> blockchainDescriptors)
        {
            return request.MintBlockchainType switch
            {
                MintChainType.Testnet when blockchainDescriptors.TryGetValue(BlockchainKind.Testnet, out var testnetBlockchainDescriptor) => testnetBlockchainDescriptor.ChainId,
                MintChainType.Mainnet when blockchainDescriptors.TryGetValue(BlockchainKind.Mainnet, out var mainnetBlockchainDescriptor) => mainnetBlockchainDescriptor.ChainId,
                _ => null
            };
        }

        /// <summary>
        /// Get soulbound token data for calculation signature.
        /// </summary>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="settings"><see cref="IBlockchainSettings"/>.</param>
        /// <returns>Returns soulbound token data for calculation signature.</returns>
        // ReSharper disable once InconsistentNaming
        public static IDictionary<ScoreType, NFTCommonData>? GetSBTData(
            this WalletStatsRequest request,
            IBlockchainSettings settings)
        {
            return request.GetSBTData(settings.BlockchainDescriptors);
        }

        /// <summary>
        /// Get soulbound token data for calculation signature.
        /// </summary>
        /// <param name="request"><see cref="WalletStatsRequest"/>.</param>
        /// <param name="blockchainDescriptors">Blockchain descriptors.</param>
        /// <returns>Returns soulbound token data for calculation signature.</returns>
        // ReSharper disable once InconsistentNaming
        public static IDictionary<ScoreType, NFTCommonData>? GetSBTData(
            this WalletStatsRequest request,
            IDictionary<BlockchainKind, BlockchainDescriptor> blockchainDescriptors)
        {
            return request.MintBlockchainType switch
            {
                MintChainType.Testnet when blockchainDescriptors.TryGetValue(BlockchainKind.Testnet, out var testnetBlockchainDescriptor) => testnetBlockchainDescriptor.SBTData,
                MintChainType.Mainnet when blockchainDescriptors.TryGetValue(BlockchainKind.Mainnet, out var mainnetBlockchainDescriptor) => mainnetBlockchainDescriptor.SBTData,
                _ => null
            };
        }
    }
}