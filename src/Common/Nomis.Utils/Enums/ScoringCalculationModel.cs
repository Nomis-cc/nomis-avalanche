// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoringCalculationModel.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

using System.ComponentModel;

namespace Nomis.Utils.Enums
{
    /// <summary>
    /// Scoring calculation model.
    /// </summary>
    public enum ScoringCalculationModel :
        ushort
    {
        /// <summary>
        /// Common V1.
        /// </summary>
        [Description("Common V1 scoring calculation model")]
        CommonV1 = 0,

        /// <summary>
        /// Symbiosis.
        /// </summary>
        [Description("Scoring calculation model for Symbosis Finance")]
        Symbiosis = 1,

        /// <summary>
        /// XDEFI.
        /// </summary>
        [Description("Scoring calculation model for XDEFI wallet")]
        XDEFI = 2,

        /// <summary>
        /// Halo.
        /// </summary>
        [Description("Scoring calculation model for Halo wallet")]
        Halo = 3,

        /// <summary>
        /// Common V2.
        /// </summary>
        [Description("Common V2 scoring calculation model")]
        CommonV2 = 4,

        /// <summary>
        /// Rubic.
        /// </summary>
        [Description("Scoring calculation model for Rubic")]
        Rubic = 5,

        /// <summary>
        /// Hedera Sybil Prevention.
        /// </summary>
        [Description("Hedera Sybil Prevention Scoring calculation model")]
        HederaSybilPrevention = 6,

        /// <summary>
        /// Hedera NFT.
        /// </summary>
        [Description("Hedera NFT Scoring calculation model")]
        HederaNFT = 7,

        /// <summary>
        /// Hedera DeFi.
        /// </summary>
        [Description("Hedera DeFi Scoring calculation model")]
        HederaDeFi = 8,

        /// <summary>
        /// Hedera Reputation.
        /// </summary>
        [Description("Hedera Reputation Scoring calculation model")]
        HederaReputation = 9,

        /// <summary>
        /// zkSync Era Reputation.
        /// </summary>
        [Description("zkSync Era Reputation Scoring calculation model")]
        ZkSyncEra = 10,

        /// <summary>
        /// Common V3.
        /// </summary>
        [Description("Common V3 scoring calculation model")]
        CommonV3 = 11,

        /// <summary>
        /// LayerZero (L0).
        /// </summary>
        [Description("LayerZero scoring calculation model")]
        LayerZero = 12,

        /// <summary>
        /// Meso.
        /// </summary>
        [Description("Meso scoring calculation model")]
        Meso = 13,

        /// <summary>
        /// Linea.
        /// </summary>
        [Description("Linea calculation model")]
        Linea = 14,

        /// <summary>
        /// Strateg.
        /// </summary>
        [Description("Strateg calculation model")]
        Strateg = 15,

        /// <summary>
        /// Scroll.
        /// </summary>
        [Description("Scroll calculation model")]
        Scroll = 16,

        /// <summary>
        /// Radiant.
        /// </summary>
        [Description("Radiant calculation model")]
        Radiant = 17,

        /// <summary>
        /// Galxe.
        /// </summary>
        [Description("Galxe calculation model")]
        Galxe = 18,

        /// <summary>
        /// Manta.
        /// </summary>
        [Description("Manta calculation model")]
        Manta = 19
    }
}