// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Utils.Contracts.Referrals
{
    /// <summary>
    /// Referrals data.
    /// </summary>
    public class ReferralsData
    {
        /// <summary>
        /// Referral count.
        /// </summary>
        public int ReferralCount { get; init; }

        /// <summary>
        /// Referral wallets.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<string>? ReferralWallets { get; set; }

        /// <summary>
        /// Referral wallets with minted SBT.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#pragma warning disable MA0016
        public List<string>? ReferralMinterWallets { get; set; }
#pragma warning restore MA0016

        /// <summary>
        /// Referral minter count.
        /// </summary>
        public int ReferralMinterCount => ReferralMinterWallets?.Count ?? 0;
    }
}