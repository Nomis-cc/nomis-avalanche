// ------------------------------------------------------------------------------------------------------
// <copyright file="DexProviderData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// DEX-provider data.
    /// </summary>
    public class DexProviderData
    {
        /// <summary>
        /// Initialize <see cref="DexProviderData"/>.
        /// </summary>
        public DexProviderData()
        {
            // for serializers
        }

        /// <summary>
        /// Initialize <see cref="DexProviderData"/>.
        /// </summary>
        /// <param name="dexDescriptor"><see cref="IDexDescriptor"/>.</param>
        /// <param name="blockChain"><see cref="IBlockchainDescriptor"/>.</param>
        public DexProviderData(
            DexDescriptor? dexDescriptor,
            BlockchainDescriptor? blockChain)
        {
            DexDescriptor = dexDescriptor;
            BlockсhainDescriptor = blockChain;
            var endpoint = DexDescriptor?.Endpoints
                .FirstOrDefault(e => (ulong?)e?.Blockchain == BlockсhainDescriptor?.ChainId);

            if (BlockсhainDescriptor?.BlockExplorerUrls?.Any() == true)
            {
                if (!string.IsNullOrWhiteSpace(endpoint?.FactoryAddress))
                {
                    FactoryAddressUrl = $"{BlockсhainDescriptor?.BlockExplorerUrls[0]}address/{endpoint.FactoryAddress}";
                }

                if (!string.IsNullOrWhiteSpace(endpoint?.RouterAddress))
                {
                    RouterAddressUrl = $"{BlockсhainDescriptor?.BlockExplorerUrls[0]}address/{endpoint.RouterAddress}";
                }

                if (!string.IsNullOrWhiteSpace(endpoint?.OracleAddress))
                {
                    OracleAddressUrl = $"{BlockсhainDescriptor?.BlockExplorerUrls[0]}address/{endpoint.OracleAddress}";
                }
            }
        }

        /// <inheritdoc cref="IDexDescriptor"/>
        public DexDescriptor? DexDescriptor { get; set; }

        /// <inheritdoc cref="BlockchainDescriptor"/>
        public BlockchainDescriptor? BlockсhainDescriptor { get; set; }

        /// <summary>
        /// Factory smart-contract code in explorer URL.
        /// </summary>
        public string? FactoryAddressUrl { get; }

        /// <summary>
        /// Router smart-contract code in explorer URL.
        /// </summary>
        public string? RouterAddressUrl { get; }

        /// <summary>
        /// Oracle smart-contract code in explorer URL.
        /// </summary>
        public string? OracleAddressUrl { get; }
    }
}