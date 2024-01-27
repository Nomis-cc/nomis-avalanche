// ------------------------------------------------------------------------------------------------------
// <copyright file="Chain.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

namespace Nomis.Dex.Abstractions.Enums
{
    /// <summary>
    /// Blockchain.
    /// </summary>
    public enum Chain :
        long
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Ethereum Mainnet.
        /// </summary>
        Ethereum = 1,

        /// <summary>
        /// Optimism.
        /// </summary>
        Optimism = 10,

        /// <summary>
        /// Flare Mainnet.
        /// </summary>
        Flare = 14,

        /// <summary>
        /// Songbird Canary-Network.
        /// </summary>
        Songbird = 19,

        /// <summary>
        /// Elastos.
        /// </summary>
        Elastos = 20,

        /// <summary>
        /// Cronos Mainnet Beta.
        /// </summary>
        Cronos = 25,

        /// <summary>
        /// RSK Mainnet.
        /// </summary>
        RSK = 30,

        /// <summary>
        /// Telos.
        /// </summary>
        Telos = 40,

        /// <summary>
        /// XinFin XDC Network.
        /// </summary>
        XDC = 50,

        /// <summary>
        /// CoinEx Smart Chain Mainnet.
        /// </summary>
        CSC = 52,

        /// <summary>
        /// Binance Smart Chain Mainnet.
        /// </summary>
        BSC = 56,

        /// <summary>
        /// Ethereum Classic Mainnet.
        /// </summary>
        EthereumClassic = 61,

        /// <summary>
        /// OKXChain Mainnet.
        /// </summary>
        OKXChain = 66,

        /// <summary>
        /// Meter Mainnet.
        /// </summary>
        Meter = 82,

        /// <summary>
        /// TomoChain.
        /// </summary>
        TomoChain = 88,

        /// <summary>
        /// POA Network Core.
        /// </summary>
        POACore = 99,

        /// <summary>
        /// Gnosis.
        /// </summary>
        Gnosis = 100,

        /// <summary>
        /// Velas EVM Mainnet.
        /// </summary>
        Velas = 106,

        /// <summary>
        /// ThunderCore Mainnet.
        /// </summary>
        ThunderCore = 108,

        /// <summary>
        /// Shibarium Mainnet.
        /// </summary>
        Shibarium = 109,

        /// <summary>
        /// Fuse Mainnet.
        /// </summary>
        Fuse = 122,

        /// <summary>
        /// Huobi ECO Chain Mainnet.
        /// </summary>
        Huobi = 128,

        /// <summary>
        /// Polygon Mainnet.
        /// </summary>
        Polygon = 137,

        /// <summary>
        /// Manta Pacific Mainnet.
        /// </summary>
        Manta = 169,

        /// <summary>
        /// BitTorrent Chain Mainnet.
        /// </summary>
        BitTorrent = 199,

        /// <summary>
        /// opBNB.
        /// </summary>
        OpBNB = 204,

        /// <summary>
        /// Vinuchain.
        /// </summary>
        Vinuchain = 207,

        /// <summary>
        /// Oasys Mainnet.
        /// </summary>
        Oasys = 248,

        /// <summary>
        /// Fantom Opera.
        /// </summary>
        Fantom = 250,

        /// <summary>
        /// Kroma.
        /// </summary>
        Kroma = 255,

        /// <summary>
        /// Boba Network.
        /// </summary>
        Boba = 288,

        /// <summary>
        /// Hedera networks.
        /// </summary>
        Hedera = 295,

        /// <summary>
        /// Filecoin.
        /// </summary>
        Filecoin = 314,

        /// <summary>
        /// KCC.
        /// </summary>
        KCC = 321,

        /// <summary>
        /// zkSync Era Mainnet.
        /// </summary>
        ZkSync = 324,

        /// <summary>
        /// Shiden.
        /// </summary>
        Shiden = 336,

        /// <summary>
        /// Theta Mainnet.
        /// </summary>
        Theta = 361,

        /// <summary>
        /// Pulse.
        /// </summary>
        Pulse = 369,

        /// <summary>
        /// Rollux.
        /// </summary>
        Rollux = 570,

        /// <summary>
        /// Astar.
        /// </summary>
        Astar = 592,

        /// <summary>
        /// Karura Network.
        /// </summary>
        Karura = 686,

        /// <summary>
        /// Acala Network.
        /// </summary>
        Acala = 787,

        /// <summary>
        /// Patex.
        /// </summary>
        Patex = 789,

        /// <summary>
        /// Wanchain.
        /// </summary>
        Wanchain = 888,

        /// <summary>
        /// 5ire.
        /// </summary>
        Fire = 997,

        /// <summary>
        /// Conflux.
        /// </summary>
        Conflux = 1030,

        /// <summary>
        /// Metis Andromeda Mainnet.
        /// </summary>
        Metis = 1088,

        /// <summary>
        /// Polygon zkEVM.
        /// </summary>
        ZkEvm = 1101,

        /// <summary>
        /// Core.
        /// </summary>
        Core = 1116,

        /// <summary>
        /// Ultron.
        /// </summary>
        Ultron = 1231,

        /// <summary>
        /// Step Network.
        /// </summary>
        Step = 1234,

        /// <summary>
        /// Moonbeam.
        /// </summary>
        Moonbeam = 1284,

        /// <summary>
        /// Moonriver.
        /// </summary>
        Moonriver = 1285,

        /// <summary>
        /// Moonbase.
        /// </summary>
        Moonbase = 1287,

        /// <summary>
        /// Tenet Mainnet.
        /// </summary>
        Tenet = 1559,

        /// <summary>
        /// Cube Chain Mainnet.
        /// </summary>
        Cube = 1818,

        /// <summary>
        /// Dogechain Mainnet.
        /// </summary>
        Dogechain = 2000,

        /// <summary>
        /// Milkomeda.
        /// </summary>
        Milkomeda = 2001,

        /// <summary>
        /// CloudWalk Mainnet.
        /// </summary>
        Cloudwalk = 2009,

        /// <summary>
        /// Kava EVM.
        /// </summary>
        Kava = 2222,

        /// <summary>
        /// IOTEX.
        /// </summary>
        IOTEX = 4689,

        /// <summary>
        /// Mantle.
        /// </summary>
        Mantle = 5000,

        /// <summary>
        /// ZetaChain.
        /// </summary>
        ZetaChain = 7000,

        /// <summary>
        /// Planq.
        /// </summary>
        Planq = 7070,

        /// <summary>
        /// Canto.
        /// </summary>
        Canto = 7700,

        /// <summary>
        /// Klaytn Mainnet Cypress.
        /// </summary>
        Klaytn = 8217,

        /// <summary>
        /// Base.
        /// </summary>
        Base = 8453,

        /// <summary>
        /// Evmos.
        /// </summary>
        Evmos = 9001,

        /// <summary>
        /// Carbon.
        /// </summary>
        Carbon = 9790,

        /// <summary>
        /// Combo Chain.
        /// </summary>
        Combo = 9980,

        /// <summary>
        /// Haqq Network.
        /// </summary>
        HAQQ = 11235,

        /// <summary>
        /// Trust EVM Testnet.
        /// </summary>
        TrustEVM = 15555,

        /// <summary>
        /// Aster zkEVM.
        /// </summary>
        AstarZkEvm = 12611,

        /// <summary>
        /// Immutable zkEvm.
        /// </summary>
        ImmutableZkEvm = 13371,

        /// <summary>
        /// OasisChain Mainnet.
        /// </summary>
        Oasis = 26863,

        /// <summary>
        /// Fusion.
        /// </summary>
        Fusion = 32659,

        /// <summary>
        /// Zilliqa.
        /// </summary>
        Zilliqa = 32769,

        /// <summary>
        /// Q.
        /// </summary>
        Q = 35441,

        /// <summary>
        /// Bitgert.
        /// </summary>
        Bitgert = 32520,

        /// <summary>
        /// Arbitrum One.
        /// </summary>
        ArbitrumOne = 42161,

        /// <summary>
        /// Arbitrum Nova.
        /// </summary>
        ArbitrumNova = 42170,

        /// <summary>
        /// Celo Mainnet.
        /// </summary>
        Celo = 42220,

        /// <summary>
        /// Oasys Emerald.
        /// </summary>
        OasysEmerald = 42262,

        /// <summary>
        /// ZkFair.
        /// </summary>
        ZkFair = 42766,

        /// <summary>
        /// Avalanche C-Chain.
        /// </summary>
        Avalanche = 43114,

        /// <summary>
        /// DFK.
        /// </summary>
        DFK = 53935,

        /// <summary>
        /// Linea Mainnet.
        /// </summary>
        Linea = 59144,

        /// <summary>
        /// vechain.
        /// </summary>
        Vechain = 100009,

        /// <summary>
        /// Solana.
        /// </summary>
        Solana = 111111,

        /// <summary>
        /// Æternity.
        /// </summary>
        Aeternity = 111112,

        /// <summary>
        /// Ripple.
        /// </summary>
        Ripple = 111114,

        /// <summary>
        /// TRON.
        /// </summary>
        Tron = 111115,

        /// <summary>
        /// NEAR Protocol.
        /// </summary>
        Near = 111116,

        /// <summary>
        /// Aptos.
        /// </summary>
        Aptos = 111117,

        /// <summary>
        /// Waves.
        /// </summary>
        Waves = 111118,

        /// <summary>
        /// Ton.
        /// </summary>
        Ton = 111119,

        /// <summary>
        /// Algorand.
        /// </summary>
        Algorand = 111120,

        /// <summary>
        /// Flow.
        /// </summary>
        Flow = 111121,

        /// <summary>
        /// Starknet.
        /// </summary>
        Starknet = 111127,

        /// <summary>
        /// Scroll.
        /// </summary>
        Scroll = 534352,

        /// <summary>
        /// Vision.
        /// </summary>
        Vision = 888888,

        /// <summary>
        /// Zora.
        /// </summary>
        Zora = 7777777,

        /// <summary>
        /// Neon EVM MainNet.
        /// </summary>
        NeonEvm = 245022934,

        /// <summary>
        /// Aurora Mainnet.
        /// </summary>
        Aurora = 1313161554,

        /// <summary>
        /// Harmony Mainnet Shard 0.
        /// </summary>
        Harmony = 1666600000,

        /// <summary>
        /// Palm.
        /// </summary>
        Palm = 11297108109
    }
}