// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralsWithLevelByReferrerWalletsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.ReferralService.Interfaces.Requests
{
    /// <summary>
    /// Referral with level by referrer wallets request.
    /// </summary>
    public sealed class ReferralsWithLevelByReferrerWalletsRequest
    {
        /// <summary>
        /// List of wallet address.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// [ "", "" ]
        /// ]]>
        /// </example>
        public IList<string> WalletsAddresses { get; set; } = new List<string>();

        /// <summary>
        /// Max referral level to retrieve.
        /// </summary>
        /// <example>1</example>
        public int MaxReferralLevel { get; set; } = 1;
    }
}