// ------------------------------------------------------------------------------------------------------
// <copyright file="IBlockchainDescriptor.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions
{
    /// <summary>
    /// Blockchain descriptor.
    /// </summary>
    public interface IBlockchainDescriptor
    {
        /// <summary>
        /// Is testnet.
        /// </summary>
        public bool IsTestnet { get; }

        /// <summary>
        /// Is enabled.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Blockchain id.
        /// </summary>
        /// <remarks>
        /// EVM chain id or sequential number like 11111x.
        /// </remarks>
        public ulong ChainId { get; }

        /// <summary>
        /// Hexadecimal blockchain id.
        /// </summary>
        public string? HexChainId { get; }

        /// <summary>
        /// Blockchain name.
        /// </summary>
        public string? ChainName { get; }

        /// <summary>
        /// Short blockchain name.
        /// </summary>
        public string? BlockchainName { get; }

        /// <summary>
        /// Blockchain slug.
        /// </summary>
        public string? BlockchainSlug { get; }

        /// <summary>
        /// Blockchain explorer block URLs.
        /// </summary>
        public IList<string>? BlockExplorerUrls { get; }

        /// <summary>
        /// RPC provider URLs.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IList<string>? RPCUrls { get; }

        /// <summary>
        /// Is the blockchain compatible with the EVM.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool IsEVMCompatible { get; }

        /// <summary>
        /// Soulbound tokens common data.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IDictionary<ScoreType, NFTCommonData>? SBTData { get; }

        /// <summary>
        /// Native currency token data.
        /// </summary>
        public TokenData? NativeCurrency { get; set; }

        /// <summary>
        /// Order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Icon.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Label icon.
        /// </summary>
        public string? LabelIcon { get; set; }

        /// <summary>
        /// Is hided.
        /// </summary>
        public bool IsHided { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public BlockchainType Type { get; set; }

        /// <summary>
        /// Platform ids.
        /// </summary>
        public IDictionary<BlockchainPlatform, string>? PlatformIds { get; set; }

        /// <summary>
        /// Offchain oracles data.
        /// </summary>
        public OffchainOraclesData? Oracles { get; set; }

        /// <summary>
        /// Additional contracts data.
        /// </summary>
        public AdditionalContractsData? AdditionalContracts { get; set; }
    }
}