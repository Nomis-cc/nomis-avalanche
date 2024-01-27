// ------------------------------------------------------------------------------------------------------
// <copyright file="TokensProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.DexProviderService.Interfaces.Enums
{
    /// <summary>
    /// Tokens provider.
    /// </summary>
    public enum TokensProvider :
        byte
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// 1inch.
        /// </summary>
        OneInch,

        /// <summary>
        /// Aave token list.
        /// </summary>
        Aave,

        /// <summary>
        /// Blockchain Association ERC20 SEC Action.
        /// </summary>
        BlockchainAssociation,

        /// <summary>
        /// CMC DeFi.
        /// </summary>
        CmcDeFi,

        /// <summary>
        /// CMC Stablecoin.
        /// </summary>
        CmcStablecoin,

        /// <summary>
        /// CMC200 ERC20.
        /// </summary>
        Cmc200Erc20,

        /// <summary>
        /// Agora dataFi Tokens.
        /// </summary>
        AgoraDataFi,

        /// <summary>
        /// CoinGecko.
        /// </summary>
        CoinGecko,

        /// <summary>
        /// Compound.
        /// </summary>
        Compound,

        /// <summary>
        /// Defiprime.
        /// </summary>
        Defiprime,

        /// <summary>
        /// Dharma Token List.
        /// </summary>
        Dharma,

        /// <summary>
        /// Furucombo.
        /// </summary>
        Furucombo,

        /// <summary>
        /// Gemini Token List.
        /// </summary>
        Gemini,

        /// <summary>
        /// Kleros Tokens.
        /// </summary>
        Kleros,

        /// <summary>
        /// Messari Verified.
        /// </summary>
        Messari,

        /// <summary>
        /// MyCrypto Token List.
        /// </summary>
        MyCrypto = 16,

        /// <summary>
        /// Optimism.
        /// </summary>
        Optimism,

        /// <summary>
        /// Roll Social Money
        /// </summary>
        RollSocialMoney,

        /// <summary>
        /// Set.
        /// </summary>
        Set,

        /// <summary>
        /// Synthetix.
        /// </summary>
        Synthetix,

        /// <summary>
        /// Testnet Token List.
        /// </summary>
        Testnet,

        /// <summary>
        /// Uniswap Labs Default.
        /// </summary>
        Uniswap,

        /// <summary>
        /// Uniswap Token Pools.
        /// </summary>
        UniswapPools,

        /// <summary>
        /// Uniswap Token Pairs.
        /// </summary>
        UniswapPairs,

        /// <summary>
        /// Wrapped Tokens.
        /// </summary>
        WrappedTokens,

        /// <summary>
        /// Zerion.
        /// </summary>
        Zerion,

        /// <summary>
        /// UMA.
        /// </summary>
        Uma,

        /// <summary>
        /// Balancer Vetted Token List.
        /// </summary>
        Balancer,

        /// <summary>
        /// QuickSwap Default Token List.
        /// </summary>
        QuickSwap,

        /// <summary>
        /// Tracer.
        /// </summary>
        Tracer,

        /// <summary>
        /// Netswap Top 100 Tokens.
        /// </summary>
        NetswapTop100,

        /// <summary>
        /// SonarWatch list.
        /// </summary>
        SonarWatch,

        /// <summary>
        /// Solana Token List.
        /// </summary>
        Solana,

        /// <summary>
        /// LibraX Extended.
        /// </summary>
        LibraX,

        /// <summary>
        /// Ubeswap.
        /// </summary>
        Ubeswap,

        /// <summary>
        /// PancakeSwap.
        /// </summary>
        PancakeSwap,

        /// <summary>
        /// Comethswap Default List.
        /// </summary>
        Comethswap,

        /// <summary>
        /// Jarvis Network.
        /// </summary>
        Jarvis,

        /// <summary>
        /// Dfyn Default list.
        /// </summary>
        Dfyn,

        /// <summary>
        /// Pangolin Token list.
        /// </summary>
        Pangolin,

        /// <summary>
        /// Trader Joe Default.
        /// </summary>
        TraderJoe,

        /// <summary>
        /// SpookySwap Default List.
        /// </summary>
        SpookySwap,

        /// <summary>
        /// Arb Whitelist Era.
        /// </summary>
        Arbitrum,

        /// <summary>
        /// ParaSwap Community Token Lists.
        /// </summary>
        ParaSwap,

        /// <summary>
        /// ParaSwap Community Stablecoin Lists.
        /// </summary>
        ParaSwapStablecoins,

        /// <summary>
        /// PowerSwap token List.
        /// </summary>
        PowerSwap,

        /// <summary>
        /// SushiSwap Menu.
        /// </summary>
        SushiSwap,

        /// <summary>
        /// SushiSwap Token Pools.
        /// </summary>
        SushiSwapPools,

        /// <summary>
        /// SushiSwap Token Pairs.
        /// </summary>
        SushiSwapPairs,

        /// <summary>
        /// Via Protocol.
        /// </summary>
        ViaProtocol,

        /// <summary>
        /// SoulSwap.
        /// </summary>
        SoulSwap,

        /// <summary>
        /// PlasmaSwap.
        /// </summary>
        PlasmaSwap,

        /// <summary>
        /// GoSwap.
        /// </summary>
        GoSwap,

        /// <summary>
        /// MochiSwap.
        /// </summary>
        MochiSwap,

        /// <summary>
        /// ShibaSwap
        /// </summary>
        ShibaSwap,

        /// <summary>
        /// FalconSwap.
        /// </summary>
        FalconSwap,

        /// <summary>
        /// Alvis Finance.
        /// </summary>
        AlvisFinance,

        /// <summary>
        /// Impossible Finance.
        /// </summary>
        ImpossibleFinance,

        /// <summary>
        /// WrappedFi.
        /// </summary>
        WrappedFi,

        /// <summary>
        /// YetiSwap.
        /// </summary>
        YetiSwap,

        /// <summary>
        /// Evmosis.
        /// </summary>
        Evmosis,

        /// <summary>
        /// VertoDex.
        /// </summary>
        VertoDex,

        /// <summary>
        /// DiffusionFi.
        /// </summary>
        DiffusionFi,

        /// <summary>
        /// OpenXswap.
        /// </summary>
        OpenXswap,

        /// <summary>
        /// ActaFi.
        /// </summary>
        ActaFi,

        /// <summary>
        /// ZappyFinance.
        /// </summary>
        ZappyFinance,

        /// <summary>
        /// LeetSwap.
        /// </summary>
        LeetSwap,

        /// <summary>
        /// Yearn.
        /// </summary>
        Yearn,

        /// <summary>
        /// Yearn Extended.
        /// </summary>
        YearnExtended,

        /// <summary>
        /// Wido.
        /// </summary>
        Wido,

        /// <summary>
        /// Tokenlistooor.
        /// </summary>
        Tokenlistooor,

        /// <summary>
        /// Portals.
        /// </summary>
        Portals,

        /// <summary>
        /// Ledger.
        /// </summary>
        Ledger,

        /// <summary>
        /// DefiLlama.
        /// </summary>
        DefiLlama,

        /// <summary>
        /// Curve.
        /// </summary>
        Curve,

        /// <summary>
        /// CoWSwap.
        /// </summary>
        CoWSwap,

        /// <summary>
        /// Euler.
        /// </summary>
        Euler,

        /// <summary>
        /// VenomSwap.
        /// </summary>
        VenomSwap,

        /// <summary>
        /// LootSwap.
        /// </summary>
        LootSwap,

        /// <summary>
        /// Increment.
        /// </summary>
        IncrementFi,

        /// <summary>
        /// Zircon.
        /// </summary>
        Zircon,

        /// <summary>
        /// OmniDex Default List.
        /// </summary>
        OmniDex,

        /// <summary>
        /// Bogged Finance.
        /// </summary>
        BoggedFinance,

        /// <summary>
        /// CoinMarketCap.
        /// </summary>
        CoinMarketCap,

        /// <summary>
        /// Matic Network.
        /// </summary>
        MaticNetwork,

        /// <summary>
        /// Venom Protocol.
        /// </summary>
        VenomProtocol,

        /// <summary>
        /// Chain Drop Org.
        /// </summary>
        ChainDropOrg,

        /// <summary>
        /// PancakeSwap Aptos.
        /// </summary>
        PancakeSwapAptos,

        /// <summary>
        /// ApeSwap Default List.
        /// </summary>
        ApeSwap,

        /// <summary>
        /// Telos Network.
        /// </summary>
        TelosNetwork,

        /// <summary>
        /// 1Hive.
        /// </summary>
        OneHive,

        /// <summary>
        /// SolarBeam.
        /// </summary>
        SolarBeam,

        /// <summary>
        /// Rainbow Wallet.
        /// </summary>
        RainbowWallet,

        /// <summary>
        /// Linea Mainnet Token Full List.
        /// </summary>
        Consensys,

        /// <summary>
        /// Combo Chain Token List.
        /// </summary>
        ComboLabs,

        /// <summary>
        /// Li Finance.
        /// </summary>
        LiFi,

        /// <summary>
        /// Mantle.
        /// </summary>
        Mantle,

        /// <summary>
        /// Etherspot.
        /// </summary>
        Etherspot,

        /// <summary>
        /// Elk Finance.
        /// </summary>
        ElkFinance,

        /// <summary>
        /// Zapper Token List.
        /// </summary>
        Zapper,

        /// <summary>
        /// FlowFans.
        /// </summary>
        FlowFans,

        /// <summary>
        /// Brave Solana.
        /// </summary>
        BraveSolana,

        /// <summary>
        /// Plasma Finance.
        /// </summary>
        PlasmaFinance,

        /// <summary>
        /// DFK Default.
        /// </summary>
        DFK,

        /// <summary>
        /// Meterio.
        /// </summary>
        Meterio,

        /// <summary>
        /// Voltswap V2.
        /// </summary>
        VoltswapV2,

        /// <summary>
        /// Meter Wallet Default List.
        /// </summary>
        MeterWallet,

        /// <summary>
        /// MilkySwap.
        /// </summary>
        MilkySwap,

        /// <summary>
        /// Camelot Labs.
        /// </summary>
        CamelotLabs,

        /// <summary>
        /// Thugs DeFi.
        /// </summary>
        ThugsDeFi,

        /// <summary>
        /// Solflare Wallet.
        /// </summary>
        SolflareWallet,

        /// <summary>
        /// Mask.
        /// </summary>
        Mask,

        /// <summary>
        /// Neon Labs.
        /// </summary>
        NeonLabs,

        /// <summary>
        /// Pro100skm.
        /// </summary>
        Pro100skm,

        /// <summary>
        /// CronaSwap.
        /// </summary>
        CronaSwap,

        /// <summary>
        /// NarwhalSwap.
        /// </summary>
        NarwhalSwap,

        /// <summary>
        /// Equilibre Token List.
        /// </summary>
        EquilibreFinance,

        /// <summary>
        /// DerpDex.
        /// </summary>
        DerpDex,

        /// <summary>
        /// Coin98.
        /// </summary>
        Coin98,

        /// <summary>
        /// SonarWatch Aptos.
        /// </summary>
        SonarWatchAptos,

        /// <summary>
        /// SonarWatch Solana.
        /// </summary>
        SonarWatchSolana,

        /// <summary>
        /// Hera DEX.
        /// </summary>
        Hera,

        /// <summary>
        /// ParaSwap Community Token Lists.
        /// </summary>
        ParaSwapCommunity,

        /// <summary>
        /// Sablier Labs.
        /// </summary>
        SablierLabs,

        /// <summary>
        /// MantleSwap.
        /// </summary>
        MantleSwap,

        /// <summary>
        /// Fuseio.
        /// </summary>
        Fuseio,

        /// <summary>
        /// DoBestMan.
        /// </summary>
        DoBestMan,

        /// <summary>
        /// Venus Protocol.
        /// </summary>
        VenusProtocol,

        /// <summary>
        /// Swene.
        /// </summary>
        Swene,

        /// <summary>
        /// Complus Network.
        /// </summary>
        ComplusNetwork,

        /// <summary>
        /// Harmony One.
        /// </summary>
        HarmonyOne,

        /// <summary>
        /// YuzuSwap.
        /// </summary>
        YuzuSwap,

        /// <summary>
        /// AtomicVM.
        /// </summary>
        AtomicVM,

        /// <summary>
        /// Embr Finance.
        /// </summary>
        EmbrFinance,

        /// <summary>
        /// Galxe.
        /// </summary>
        Galxe,

        /// <summary>
        /// Solyard Finance.
        /// </summary>
        SolyardFinance,

        /// <summary>
        /// Space Finance.
        /// </summary>
        SpaceFinance,

        /// <summary>
        /// Civitas.
        /// </summary>
        Civitas,

        /// <summary>
        /// Aerodrome.
        /// </summary>
        Aerodrome,

        /// <summary>
        /// SmolDapp.
        /// </summary>
        SmolDapp,

        /// <summary>
        /// Ajna.
        /// </summary>
        Ajna,

        /// <summary>
        /// Manta.
        /// </summary>
        Manta,

        /// <summary>
        /// Archly.
        /// </summary>
        ArchlyFi,

        /// <summary>
        /// Interport Finance.
        /// </summary>
        InterportFinance,

        /// <summary>
        /// Piteas.
        /// </summary>
        Piteas,

        /// <summary>
        /// CanaLabs.
        /// </summary>
        CanaLabs,

        /// <summary>
        /// CanaLabs Aptos.
        /// </summary>
        CanaLabsAptos,

        /// <summary>
        /// ParaSwap Extended.
        /// </summary>
        ParaSwapExtended
    }
}