// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyCategory.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.HapiExplorer.Interfaces.Enums
{
    /// <summary>
    /// HAPI proxy risk category.
    /// </summary>
    public enum HapiProxyCategory :
        byte
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Wallet service - custodial or mixed wallets.
        /// </summary>
        WalletService = 1,

        /// <summary>
        /// Merchant service.
        /// </summary>
        MerchantService = 2,

        /// <summary>
        /// Mining pool.
        /// </summary>
        MiningPool = 3,

        /// <summary>
        /// Exchange with high KYC standards.
        /// </summary>
        Exchange = 4,

        /// <summary>
        /// DeFi application.
        /// </summary>
        DeFi = 5,

        /// <summary>
        /// OTC Broker.
        /// </summary>
        OTCBroker = 6,

        /// <summary>
        /// Cryptocurrency ATM.
        /// </summary>
        ATM = 7,

        /// <summary>
        /// Gambling.
        /// </summary>
        Gambling = 8,

        /// <summary>
        /// Illicit organization.
        /// </summary>
        IllicitOrganization = 9,

        /// <summary>
        /// Mixer.
        /// </summary>
        Mixer = 10,

        /// <summary>
        /// Darknet market or service.
        /// </summary>
        DarknetService = 11,

        /// <summary>
        /// Scam.
        /// </summary>
        Scam = 12,

        /// <summary>
        /// Ransomware.
        /// </summary>
        Ransomware = 13,

        /// <summary>
        /// Theft - stolen funds.
        /// </summary>
        Theft = 14,

        /// <summary>
        /// Counterfeit - fake assets.
        /// </summary>
        Counterfeit = 15,

        /// <summary>
        /// Terrorist financing.
        /// </summary>
        TerroristFinancing = 16,

        /// <summary>
        /// Sanctions.
        /// </summary>
        Sanctions = 17,

        /// <summary>
        /// Child abuse and porn materials.
        /// </summary>
        ChildAbuse = 18
    }
}