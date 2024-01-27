// ------------------------------------------------------------------------------------------------------
// <copyright file="DexProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.Dex.Abstractions.Enums
{
    /// <summary>
    /// DEX aggregator DEX provider.
    /// </summary>
    public enum DexProvider
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// BiSwap (BSC).
        /// </summary>
        BiSwap,

        /// <summary>
        /// JetSwap.
        /// </summary>
        JetSwap,

        /// <summary>
        /// KyberSwap.
        /// </summary>
        KyberSwap,

        /// <summary>
        /// SushiSwap.
        /// </summary>
        SushiSwap,

        /// <summary>
        /// SummitSwap.
        /// </summary>
        SummitSwap,

        /// <summary>
        /// PancakeSwap V2.
        /// </summary>
        PancakeSwapV2,

        /// <summary>
        /// KwikSwap.
        /// </summary>
        KwikSwap,

        /// <summary>
        /// Trader Joe.
        /// </summary>
        TraderJoe,

        /// <summary>
        /// Ubeswap.
        /// </summary>
        Ubeswap,

        /// <summary>
        /// SpookySwap.
        /// </summary>
        SpookySwap,

        /// <summary>
        /// Dfyn.
        /// </summary>
        Dfyn,

        /// <summary>
        /// FraxSwap.
        /// </summary>
        FraxSwap,

        /// <summary>
        /// Levinswap.
        /// </summary>
        Levinswap,

        /// <summary>
        /// Huckleberry Finance.
        /// </summary>
        Huckleberry,

        /// <summary>
        /// Uniswap V2.
        /// </summary>
        UniswapV2,

        /// <summary>
        /// WhaleSwap.
        /// </summary>
        WhaleSwap,

        /// <summary>
        /// MistSwap.
        /// </summary>
        MistSwap,

        /// <summary>
        /// Unifi Protocol.
        /// </summary>
        Unifi,

        /// <summary>
        /// StellaSwap.
        /// </summary>
        StellaSwap,

        /// <summary>
        /// SpiritSwap.
        /// </summary>
        SpiritSwap,

        /// <summary>
        /// SakeSwap.
        /// </summary>
        SakeSwap,

        /// <summary>
        /// Definix.
        /// </summary>
        Definix,

        /// <summary>
        /// Swapsicle.
        /// </summary>
        Swapsicle,

        /// <summary>
        /// MDEX.
        /// </summary>
        MDEX,

        /// <summary>
        /// Polycat Finance.
        /// </summary>
        Polycat,

        /// <summary>
        /// Impossible Finance.
        /// </summary>
        ImpossibleFinance,

        /// <summary>
        /// BeamSwap.
        /// </summary>
        BeamSwap,

        /// <summary>
        /// BakerySwap.
        /// </summary>
        BakerySwap,

        /// <summary>
        /// ShibaSwap.
        /// </summary>
        ShibaSwap,

        /// <summary>
        /// OpenSwap.
        /// </summary>
        OpenSwap,

        /// <summary>
        /// WingSwap.
        /// </summary>
        WingSwap,

        /// <summary>
        /// ApeSwap.
        /// </summary>
        ApeSwap,

        /// <summary>
        /// PYESwap.
        /// </summary>
        PYESwap,

        /// <summary>
        /// SparkSwap.
        /// </summary>
        SparkSwap,

        /// <summary>
        /// FirebirdSwap.
        /// </summary>
        FirebirdSwap,

        /// <summary>
        /// CoinSwap.
        /// </summary>
        CoinSwap,

        /// <summary>
        /// BurgerSwap.
        /// </summary>
        BurgerSwap,

        /// <summary>
        /// BridgeSwap.
        /// </summary>
        BridgeSwap,

        /// <summary>
        /// AlitaSwap.
        /// </summary>
        AlitaSwap,

        /// <summary>
        /// HoneySwap.
        /// </summary>
        HoneySwap,

        /// <summary>
        /// LuaSwap.
        /// </summary>
        LuaSwap,

        /// <summary>
        /// MochiSwap.
        /// </summary>
        MochiSwap,

        /// <summary>
        /// HurricaneSwap.
        /// </summary>
        HurricaneSwap,

        /// <summary>
        /// Nomiswap.
        /// </summary>
        Nomiswap,

        /// <summary>
        /// ConvX.
        /// </summary>
        ConvX,

        /// <summary>
        /// PantherSwap.
        /// </summary>
        PantherSwap,

        /// <summary>
        /// WakaSwap.
        /// </summary>
        WakaSwap,

        /// <summary>
        /// Excalibur.
        /// </summary>
        Excalibur,

        /// <summary>
        /// KokomoSwap.
        /// </summary>
        KokomoSwap,

        /// <summary>
        /// Cyberswap.
        /// </summary>
        Cyberswap,

        /// <summary>
        /// PlexSwap.
        /// </summary>
        PlexSwap,

        /// <summary>
        /// Degen Haus.
        /// </summary>
        DegenHaus,

        /// <summary>
        /// LatteSwap.
        /// </summary>
        LatteSwap,

        /// <summary>
        /// WraithSwap.
        /// </summary>
        WraithSwap,

        /// <summary>
        /// Just Money.
        /// </summary>
        JustMoney,

        /// <summary>
        /// Foodcourt.
        /// </summary>
        Foodcourt,

        /// <summary>
        /// PinkSwap.
        /// </summary>
        PinkSwap,

        /// <summary>
        /// NarwhalSwap.
        /// </summary>
        NarwhalSwap,

        /// <summary>
        /// Benswap.
        /// </summary>
        BenSwap,

        /// <summary>
        /// KnightSwap.
        /// </summary>
        KnightSwap,

        /// <summary>
        /// ZooDex.
        /// </summary>
        ZooDex,

        /// <summary>
        /// WigoSwap.
        /// </summary>
        WigoSwap,

        /// <summary>
        /// Autoshark Finance.
        /// </summary>
        Autoshark,

        /// <summary>
        /// FuseSwap.
        /// </summary>
        FuseSwap,

        /// <summary>
        /// BabySwap.
        /// </summary>
        BabySwap,

        /// <summary>
        /// DDDXSwap.
        /// </summary>
        DDDXSwap,

        /// <summary>
        /// AliumSwap.
        /// </summary>
        AliumSwap,

        /// <summary>
        /// JulSwap.
        /// </summary>
        JulSwap,

        /// <summary>
        /// Swapr.
        /// </summary>
        Swapr,

        /// <summary>
        /// SmexSwap.
        /// </summary>
        SmexSwap,

        /// <summary>
        /// WardenSwap.
        /// </summary>
        WardenSwap,

        /// <summary>
        /// SoulSwap.
        /// </summary>
        SoulSwap,

        /// <summary>
        /// PolyDEX.
        /// </summary>
        PolyDEX,

        /// <summary>
        /// Jswap.
        /// </summary>
        Jswap,

        /// <summary>
        /// LinkSwap.
        /// </summary>
        LinkSwap,

        /// <summary>
        /// FeSwap.
        /// </summary>
        FeSwap,

        /// <summary>
        /// CorgiSwap.
        /// </summary>
        CorgiSwap,

        /// <summary>
        /// Capital DEX.
        /// </summary>
        CapitalDEX,

        /// <summary>
        /// YetiSwap.
        /// </summary>
        YetiSwap,

        /// <summary>
        /// PlasmaSwap.
        /// </summary>
        PlasmaSwap,

        /// <summary>
        /// ApeX.
        /// </summary>
        ApeX,

        /// <summary>
        /// GIBXSwap.
        /// </summary>
        GIBXSwap,

        /// <summary>
        /// Nomiswap Stable.
        /// </summary>
        NomiswapStable,

        /// <summary>
        /// W3Swap.
        /// </summary>
        W3Swap,

        /// <summary>
        /// Crypto.com DeFi Swap
        /// </summary>
        CroDefiSwap,

        /// <summary>
        /// Pangolin.
        /// </summary>
        Pangolin,

        /// <summary>
        /// PancakeSwap.
        /// </summary>
        PancakeSwap,

        /// <summary>
        /// Mad Meerkat Finance.
        /// </summary>
        MadMeerkat,

        /// <summary>
        /// FstSwap.
        /// </summary>
        FstSwap,

        /// <summary>
        /// BabyDoge.
        /// </summary>
        BabyDoge,

        /// <summary>
        /// Swych.
        /// </summary>
        Swych,

        /// <summary>
        /// RadioShack.
        /// </summary>
        RadioShack,

        /// <summary>
        /// SaitaSwap.
        /// </summary>
        SaitaSwap,

        /// <summary>
        /// Planet Finance.
        /// </summary>
        PlanetFinance,

        /// <summary>
        /// Pandora.
        /// </summary>
        Pandora,

        /// <summary>
        /// MarsSwap.
        /// </summary>
        MarsSwap,

        /// <summary>
        /// VulcanSwap.
        /// </summary>
        VulcanSwap,

        /// <summary>
        /// LuchowSwap.
        /// </summary>
        LuchowSwap,

        /// <summary>
        /// LeonicornSwap.
        /// </summary>
        LeonicornSwap,

        /// <summary>
        /// WinerySwap.
        /// </summary>
        WinerySwap,

        /// <summary>
        /// Elk Finance.
        /// </summary>
        ElkFinance,

        /// <summary>
        /// Gravis Finance.
        /// </summary>
        GSwap,

        /// <summary>
        /// DinosaurSwap.
        /// </summary>
        DinosaurSwap,

        /// <summary>
        /// Annex Finance.
        /// </summary>
        Annex,

        /// <summary>
        /// DinoSwap.
        /// </summary>
        DinoSwap,

        /// <summary>
        /// ComethSwap.
        /// </summary>
        ComethSwap,

        /// <summary>
        /// DecaSwap.
        /// </summary>
        DecaSwap,

        /// <summary>
        /// AmpleSwap.
        /// </summary>
        AmpleSwap,

        /// <summary>
        /// CafeSwap.
        /// </summary>
        CafeSwap,

        /// <summary>
        /// PureSwap.
        /// </summary>
        PureSwap,

        /// <summary>
        /// KyotoSwap.
        /// </summary>
        KyotoSwap,

        /// <summary>
        /// CanarySwap.
        /// </summary>
        CanarySwap,

        /// <summary>
        /// BoxSwap.
        /// </summary>
        BoxSwap,

        /// <summary>
        /// USDFI.
        /// </summary>
        USDFI,

        /// <summary>
        /// PolyZap.
        /// </summary>
        PolyZap,

        /// <summary>
        /// Twindex.
        /// </summary>
        Twindex,

        /// <summary>
        /// ThugSwap.
        /// </summary>
        ThugSwap,

        /// <summary>
        /// CheeseSwap.
        /// </summary>
        CheeseSwap,

        /// <summary>
        /// JavaSwap.
        /// </summary>
        JavaSwap,

        /// <summary>
        /// BorgSwap.
        /// </summary>
        BorgSwap,

        /// <summary>
        /// DigiSwap.
        /// </summary>
        DigiSwap,

        /// <summary>
        /// EDDASwap.
        /// </summary>
        EDDASwap,

        /// <summary>
        /// ButterSwap.
        /// </summary>
        ButterSwap,

        /// <summary>
        /// SumSwap.
        /// </summary>
        SumSwap,

        /// <summary>
        /// ChickenSwap.
        /// </summary>
        ChickenSwap,

        /// <summary>
        /// AleSwap.
        /// </summary>
        AleSwap,

        /// <summary>
        /// Famososwap.
        /// </summary>
        Famososwap,

        /// <summary>
        /// VaporDEX.
        /// </summary>
        VaporDEX,

        /// <summary>
        /// VeneraSwap.
        /// </summary>
        VeneraSwap,

        /// <summary>
        /// MoonDogeSwap.
        /// </summary>
        MoonDogeSwap,

        /// <summary>
        /// FastSwap.
        /// </summary>
        FastSwap,

        /// <summary>
        /// WakandaSwap.
        /// </summary>
        WakandaSwap,

        /// <summary>
        /// Tomb Finance.
        /// </summary>
        TombFinance,

        /// <summary>
        /// ProtoFi.
        /// </summary>
        ProtoFi,

        /// <summary>
        /// MorpheusSwap.
        /// </summary>
        MorpheusSwap,

        /// <summary>
        /// HyperSwap.
        /// </summary>
        HyperSwap,

        /// <summary>
        /// PartySwap.
        /// </summary>
        PartySwap,

        /// <summary>
        /// ViralataSwap.
        /// </summary>
        ViralataSwap,

        /// <summary>
        /// NinjaSwap.
        /// </summary>
        NinjaSwap,

        /// <summary>
        /// SwapFish Protocol.
        /// </summary>
        SwapFish,

        /// <summary>
        /// Verse.
        /// </summary>
        Verse,

        /// <summary>
        /// Redemption.
        /// </summary>
        Redemption,

        /// <summary>
        /// SealightSwap.
        /// </summary>
        SealightSwap,

        /// <summary>
        /// Complus Network Exchange.
        /// </summary>
        Complus,

        /// <summary>
        /// DooarSwap.
        /// </summary>
        DooarSwap,

        /// <summary>
        /// BeGlobal Finance.
        /// </summary>
        BeGlobal,

        /// <summary>
        /// AuraSwap.
        /// </summary>
        AuraSwap,

        /// <summary>
        /// HunnySwap.
        /// </summary>
        HunnySwap,

        /// <summary>
        /// Camelot.
        /// </summary>
        Camelot,

        /// <summary>
        /// OreoSwap.
        /// </summary>
        OreoSwap,

        /// <summary>
        /// ThreeXCalibur.
        /// </summary>
        ThreeXCalibur,

        /// <summary>
        /// HakuSwap.
        /// </summary>
        HakuSwap,

        /// <summary>
        /// Trisolaris.
        /// </summary>
        Trisolaris,

        /// <summary>
        /// AuroraSwap.
        /// </summary>
        AuroraSwap,

        /// <summary>
        /// SafeMoon Exchange.
        /// </summary>
        SafeMoonSwap,

        /// <summary>
        /// Yoshi Exchange.
        /// </summary>
        YoshiExchange,

        /// <summary>
        /// BombSwap.
        /// </summary>
        BombSwap,

        /// <summary>
        /// LunarDEX.
        /// </summary>
        LunarDEX,

        /// <summary>
        /// Zenlink.
        /// </summary>
        Zenlink,

        /// <summary>
        /// Zircon.
        /// </summary>
        Zircon,

        /// <summary>
        /// ZipSwap.
        /// </summary>
        ZipSwap,

        /// <summary>
        /// NachoSwap.
        /// </summary>
        NachoSwap,

        /// <summary>
        /// MakiSwap.
        /// </summary>
        MakiSwap,

        /// <summary>
        /// JamonSwap.
        /// </summary>
        JamonSwap,

        /// <summary>
        /// Solidly.
        /// </summary>
        Solidly,

        /// <summary>
        /// Farmtom.
        /// </summary>
        Farmtom,

        /// <summary>
        /// Velodrome.
        /// </summary>
        Velodrome,

        /// <summary>
        /// Juggler Red.
        /// </summary>
        JugglerRed,

        /// <summary>
        /// ONI Exchange.
        /// </summary>
        ONIExchange,

        /// <summary>
        /// KACO Finance.
        /// </summary>
        KACOFinance,

        /// <summary>
        /// PADSwap.
        /// </summary>
        PADSwap,

        /// <summary>
        /// Titano.
        /// </summary>
        Titano,

        /// <summary>
        /// ScarySwap.
        /// </summary>
        ScarySwap,

        /// <summary>
        /// Alligator.
        /// </summary>
        Alligator,

        /// <summary>
        /// Tarina.
        /// </summary>
        Tarina,

        /// <summary>
        /// OnAVAX.
        /// </summary>
        OnAVAX,

        /// <summary>
        /// Voltage Finance.
        /// </summary>
        VoltageFinance,

        /// <summary>
        /// Alphadex.
        /// </summary>
        Alphadex,

        /// <summary>
        /// NearPad.
        /// </summary>
        NearPad
    }
}