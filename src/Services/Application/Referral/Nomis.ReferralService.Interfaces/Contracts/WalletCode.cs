// ------------------------------------------------------------------------------------------------------
// <copyright file="WalletCode.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.ReferralService.Interfaces.Contracts
{
    /// <summary>
    /// Wallet code.
    /// </summary>
    public class WalletCode
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string Address { get; init; } = null!;

        /// <summary>
        /// Code.
        /// </summary>
        public string? Code { get; init; }
    }
}