// ------------------------------------------------------------------------------------------------------
// <copyright file="BlockchainDescriptor.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Enums;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Contracts
{
    /// <inheritdoc cref="IBlockchainDescriptor"/>
    public class BlockchainDescriptor :
        IBlockchainDescriptor
    {
        /// <summary>
        /// Initialize <see cref="BlockchainDescriptor"/>.
        /// </summary>
        public BlockchainDescriptor()
        {
            // for serializers
        }

        /// <summary>
        /// Initialize <see cref="BlockchainDescriptor"/>.
        /// </summary>
        /// <param name="blockchainDescriptor"><see cref="IBlockchainDescriptor"/>.</param>
        public BlockchainDescriptor(
            IBlockchainDescriptor? blockchainDescriptor)
        {
            IsTestnet = blockchainDescriptor?.IsTestnet ?? false;
            IsEnabled = blockchainDescriptor?.IsEnabled ?? false;
            ChainId = blockchainDescriptor?.ChainId ?? 0;
            ChainName = blockchainDescriptor?.ChainName;
            BlockchainName = blockchainDescriptor?.BlockchainName;
            BlockchainSlug = blockchainDescriptor?.BlockchainSlug;
            BlockExplorerUrls = blockchainDescriptor?.BlockExplorerUrls;
            RPCUrls = blockchainDescriptor?.RPCUrls;
            IsEVMCompatible = blockchainDescriptor?.IsEVMCompatible ?? false;
            SBTData = blockchainDescriptor?.SBTData;
            NativeCurrency = blockchainDescriptor?.NativeCurrency;
            Order = blockchainDescriptor?.Order ?? 1;
            Icon = blockchainDescriptor?.Icon;
            LabelIcon = blockchainDescriptor?.LabelIcon;
            IsHided = blockchainDescriptor?.IsHided ?? false;
            Type = blockchainDescriptor?.Type ?? BlockchainType.None;
            PlatformIds = blockchainDescriptor?.PlatformIds;
            Oracles = blockchainDescriptor?.Oracles;
            AdditionalContracts = blockchainDescriptor?.AdditionalContracts;
        }

        /// <inheritdoc cref="IBlockchainDescriptor.IsTestnet"/>
        [JsonInclude]
        public bool IsTestnet { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.IsEnabled"/>
        [JsonInclude]
        public bool IsEnabled { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.ChainId"/>
        [JsonInclude]
        public ulong ChainId { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.HexChainId"/>
        [JsonInclude]
        public string? HexChainId => IsEVMCompatible ? $"0x{ChainId:X}" : null;

        /// <inheritdoc cref="IBlockchainDescriptor.ChainName"/>
        [JsonInclude]
        public string? ChainName { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.BlockchainName"/>
        [JsonInclude]
        public string? BlockchainName { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.BlockExplorerUrls"/>
        [JsonInclude]
        public IList<string>? BlockExplorerUrls { get; set; } = new List<string>();

        /// <inheritdoc cref="IBlockchainDescriptor.RPCUrls"/>
        [JsonInclude]
        public IList<string>? RPCUrls { get; set; } = new List<string>();

        /// <inheritdoc cref="IBlockchainDescriptor.BlockchainSlug"/>
        [JsonInclude]
        public string? BlockchainSlug { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.IsEVMCompatible"/>
        [JsonInclude]
        public bool IsEVMCompatible { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.SBTData"/>
        [JsonInclude]
        public IDictionary<ScoreType, NFTCommonData>? SBTData { get; set; } = new Dictionary<ScoreType, NFTCommonData>();

        /// <inheritdoc cref="IBlockchainDescriptor.NativeCurrency"/>
        [JsonInclude]
        public TokenData? NativeCurrency { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.Order"/>
        [JsonInclude]
        public int Order { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.Icon"/>
        [JsonInclude]
        public string? Icon { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.LabelIcon"/>
        [JsonInclude]
        public string? LabelIcon { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.IsHided"/>
        [JsonIgnore]
        public bool IsHided { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.Type"/>
        [JsonInclude]
        public BlockchainType Type { get; set; }

        /// <inheritdoc cref="IBlockchainDescriptor.PlatformIds"/>
        [JsonInclude]
        public IDictionary<BlockchainPlatform, string>? PlatformIds { get; set; } = new Dictionary<BlockchainPlatform, string>();

        /// <inheritdoc />
        [JsonInclude]
        public OffchainOraclesData? Oracles { get; set; }

        /// <inheritdoc />
        [JsonInclude]
        public AdditionalContractsData? AdditionalContracts { get; set; }
    }
}