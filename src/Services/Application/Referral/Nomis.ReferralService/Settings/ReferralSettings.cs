// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.ReferralService.Settings
{
    /// <summary>
    /// Referral settings.
    /// </summary>
    internal class ReferralSettings :
        ISettings
    {
        /// <summary>
        /// Referral reward base amount.
        /// </summary>
        public decimal ReferralRewardBaseAmount { get; init; }

        /// <summary>
        /// Use multilevel referral system.
        /// </summary>
        public bool UseMultilevel { get; init; }

        /// <summary>
        /// Hide referrals data.
        /// </summary>
        public bool HideReferralsData { get; init; }
    }
}