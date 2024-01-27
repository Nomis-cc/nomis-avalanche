// ------------------------------------------------------------------------------------------------------
// <copyright file="IReferralService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Domain.Referral.Entities;
using Nomis.ReferralService.Interfaces.Contracts;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.ReferralService.Interfaces
{
    /// <summary>
    /// Referral service.
    /// </summary>
    public interface IReferralService :
        IApplicationService
    {
        /// <summary>
        /// Get referral wallet by referral code.
        /// </summary>
        /// <param name="referralCode">Referral code.</param>
        /// <returns>Return existed <see cref="ReferralWallet"/> by referral code.</returns>
        public Task<Result<ReferralWallet>> GetReferralWalletByReferralCodeAsync(
            string referralCode);

        /// <summary>
        /// Get or create referral wallet.
        /// </summary>
        /// <param name="walletAddress">Wallet address.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Return existed or created <see cref="ReferralWallet"/>.</returns>
        public Task<Result<ReferralWallet>> GetOrCreateReferralWalletAsync(
            string walletAddress,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get referrer code by referral wallet address.
        /// </summary>
        /// <param name="walletAddress">Wallet address.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns referrer code by referral wallet address.</returns>
        public Task<Result<string?>> GetReferrerCodeByReferralWalletAsync(
            string walletAddress,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get referrer codes by referral wallet addresses.
        /// </summary>
        /// <param name="walletAddresses">Wallet addresses.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns referrer codes by referral wallet addresses.</returns>
        public Task<Result<List<WalletCode>>> GetReferrerCodesByReferralWalletsAsync(
            IEnumerable<string> walletAddresses,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get referral code by referral wallet address.
        /// </summary>
        /// <param name="walletAddress">Wallet address.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns referral code by referral wallet address.</returns>
        public Task<Result<string?>> GetReferralCodeByReferralWalletAsync(
            string walletAddress,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get referral codes by referral wallet addresses.
        /// </summary>
        /// <param name="walletAddresses">Wallet addresses.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns referral codes by referral wallet addresses.</returns>
        public Task<Result<List<WalletCode>>> GetReferralCodesByReferralWalletsAsync(
            IEnumerable<string> walletAddresses,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Add referral.
        /// </summary>
        /// <param name="referredWalletAddress">Referred wallet address.</param>
        /// <param name="referringWalletAddress">Referring wallet address.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns <see cref="ReferralData"/> result with added referral.</returns>
        public Task<Result<ReferralData>> AddReferralAsync(
            string referredWalletAddress,
            string referringWalletAddress,
            CancellationToken cancellationToken = default);
    }
}