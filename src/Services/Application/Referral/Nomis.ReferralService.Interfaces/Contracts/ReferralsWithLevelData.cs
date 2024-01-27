// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralsWithLevelData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.ReferralService.Interfaces.Contracts
{
    /// <summary>
    /// Referrals with level data.
    /// </summary>
    public sealed class ReferralsWithLevelData
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string WalletAddress { get; set; } = null!;

        /// <summary>
        /// Referrals data by level.
        /// </summary>
        public IDictionary<int, ReferralsDataExtended> ReferralsData { get; set; } = new Dictionary<int, ReferralsDataExtended>();

        /// <summary>
        /// Errors.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<string>? Errors { get; set; }
    }
}