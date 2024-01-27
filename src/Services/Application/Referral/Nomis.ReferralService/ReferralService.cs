// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.DataAccess.Referral.Interfaces.Contexts;
using Nomis.Domain.Referral.Entities;
using Nomis.ReferralService.Interfaces;
using Nomis.ReferralService.Interfaces.Contracts;
using Nomis.ReferralService.Settings;
using Nomis.Utils.Attributes.Logging;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.ReferralService
{
    /// <inheritdoc cref="IReferralService"/>
    internal sealed class ReferralService :
        IReferralService,
        ITransientService
    {
        private readonly IReferralDbContext _dbContext;
        private readonly IReferralReadDbContext _readDbContext;
        private readonly ReferralSettings _settings;
        private readonly ILogger<ReferralService> _logger;

        /// <summary>
        /// Initialize <see cref="ReferralService"/>.
        /// </summary>
        /// <param name="dbContext"><see cref="IReferralDbContext"/>.</param>
        /// <param name="readDbContext"><see cref="IReferralReadDbContext"/>.</param>
        /// <param name="options"><see cref="ReferralSettings"/>.</param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/>.</param>
        public ReferralService(
            IReferralDbContext dbContext,
            IReferralReadDbContext readDbContext,
            IOptions<ReferralSettings> options,
            ILogger<ReferralService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _readDbContext = readDbContext;
            _settings = options.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<Result<ReferralWallet>> GetReferralWalletByReferralCodeAsync(
            string referralCode)
        {
            // get referral wallet
            var wallet = await _readDbContext.ReferralWallets
                .FirstOrDefaultAsync(w => w.ReferralCode == referralCode)
                .ConfigureAwait(false);
            if (wallet == null)
            {
                return await Result<ReferralWallet>.FailAsync($"There is no referral wallet with referral code {referralCode}.").ConfigureAwait(false);
            }

            return await Result<ReferralWallet>.SuccessAsync(wallet, $"Successfully got referral wallet by referral code {referralCode}.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<ReferralWallet>> GetOrCreateReferralWalletAsync(
            string walletAddress,
            CancellationToken cancellationToken = default)
        {
            try
            {
                walletAddress = walletAddress.Trim().ToLower();

                // get or add referral wallet
                var wallet = await _dbContext.ReferralWallets
                    .FirstOrDefaultAsync(w => w.WalletAddress.ToLower() == walletAddress, cancellationToken)
                    .ConfigureAwait(false);
                if (wallet == null)
                {
                    wallet = new ReferralWallet(walletAddress);
                    bool referralCodeExists = await _dbContext.ReferralWallets
                        .AnyAsync(w => w.ReferralCode == wallet.ReferralCode, cancellationToken)
                        .ConfigureAwait(false);
                    while (referralCodeExists)
                    {
                        wallet.RefreshReferralCode();
                        referralCodeExists = await _dbContext.ReferralWallets
                            .AnyAsync(w => w.ReferralCode == wallet.ReferralCode, cancellationToken)
                            .ConfigureAwait(false);
                    }

                    var walletReward = new RewardData(wallet.Id);
                    _dbContext.RewardDatas.Add(walletReward);
                    wallet.SetReward(walletReward.Id);
                    _dbContext.ReferralWallets.Add(wallet);
                }
                else
                {
                    return await Result<ReferralWallet>.SuccessAsync(wallet, "Successfully got referral wallet by address.").ConfigureAwait(false);
                }

                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation("Successfully create referral code {ReferralCode} for wallet {Address}.", wallet.ReferralCode, wallet.WalletAddress.ToLower());
                return await Result<ReferralWallet>.SuccessAsync(wallet, "Successfully got referral wallet by address.").ConfigureAwait(false);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogCritical(e, "A timeout error occurred while getting or creating referral wallet by address.");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An error occurred while getting or creating referral wallet by address.");
            }

            return await Result<ReferralWallet>.FailAsync("An error occurred while getting or creating referral wallet.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<string?>> GetReferrerCodeByReferralWalletAsync(
            string walletAddress,
            CancellationToken cancellationToken = default)
        {
            walletAddress = walletAddress.Trim().ToLower();

            var referralWallet = await _readDbContext.ReferralWallets
                .Include(x => x.Referrals)
                .Select(x => new
                {
                    Wallets = x.Referrals.Select(r => r.ReferredWallet.WalletAddress.ToLower()), x.ReferralCode
                })
                .FirstOrDefaultAsync(w => w.Wallets.Any(x => x.ToLower() == walletAddress), cancellationToken)
                .ConfigureAwait(false);
            if (referralWallet == null)
            {
                return await Result<string?>.FailAsync("There is no referrer wallet.").ConfigureAwait(false);
            }

            return await Result<string?>.SuccessAsync(referralWallet.ReferralCode, "Successfully got referrer code.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<List<WalletCode>>> GetReferrerCodesByReferralWalletsAsync(
            IEnumerable<string> walletAddresses,
            CancellationToken cancellationToken = default)
        {
            walletAddresses = walletAddresses.Select(x => x.Trim().ToLower());

            var referralWallets = await _readDbContext.ReferralDatas
                .Include(x => x.ReferringWallet)
                .Include(x => x.ReferredWallet)
                .Where(x => walletAddresses.Contains(x.ReferredWallet.WalletAddress.ToLower()))
                .Select(x => new WalletCode
                {
                    Address = x.ReferredWallet.WalletAddress.ToLower(),
                    Code = x.ReferringWallet.ReferralCode
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            if (!referralWallets.Any())
            {
                return await Result<List<WalletCode>>.FailAsync("There is no referrer codes.").ConfigureAwait(false);
            }

            return await Result<List<WalletCode>>.SuccessAsync(referralWallets.DistinctBy(x => x.Address).ToList(), "Successfully got referrer codes.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<string?>> GetReferralCodeByReferralWalletAsync(
            string walletAddress,
            CancellationToken cancellationToken = default)
        {
            walletAddress = walletAddress.Trim().ToLower();

            string? referralCode = await _readDbContext.ReferralWallets
                .Include(x => x.Referrals)
                .Where(x => x.WalletAddress.ToLower() == walletAddress)
                .Select(x => x.ReferralCode)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
            if (referralCode == null)
            {
                return await Result<string?>.FailAsync("There is no referral code.").ConfigureAwait(false);
            }

            return await Result<string?>.SuccessAsync(referralCode, "Successfully got referral code.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<List<WalletCode>>> GetReferralCodesByReferralWalletsAsync(
            IEnumerable<string> walletAddresses,
            CancellationToken cancellationToken = default)
        {
            walletAddresses = walletAddresses.Select(x => x.Trim().ToLower());

            var referralCodes = await _readDbContext.ReferralWallets
                .Include(x => x.Referrals)
                .Where(x => walletAddresses.Contains(x.WalletAddress.ToLower()))
                .Select(x => new WalletCode
                {
                    Address = x.WalletAddress,
                    Code = x.ReferralCode
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            if (!referralCodes.Any())
            {
                return await Result<List<WalletCode>>.FailAsync("There is no referral codes.").ConfigureAwait(false);
            }

            return await Result<List<WalletCode>>.SuccessAsync(referralCodes, "Successfully got referral codes.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<ReferralData>> AddReferralAsync(
            string referredWalletAddress,
            string referringWalletAddress,
            CancellationToken cancellationToken = default)
        {
            referredWalletAddress = referredWalletAddress.Trim().ToLower();
            referringWalletAddress = referringWalletAddress.Trim().ToLower();

            if (referringWalletAddress.Equals(referredWalletAddress, StringComparison.InvariantCultureIgnoreCase))
            {
                return await Result<ReferralData>.FailAsync("An error occurred while adding referral.").ConfigureAwait(false);
            }

            bool referralDataExists = await _readDbContext.ReferralDatas
                .Include(r => r.ReferredWallet)
                .AnyAsync(r => r.ReferredWallet.WalletAddress.ToLower() == referredWalletAddress, cancellationToken)
                .ConfigureAwait(false);
            if (referralDataExists)
            {
                return await Result<ReferralData>.FailAsync("The referral already added before.").ConfigureAwait(false);
            }

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                // get or add referring wallet
                var referringWallet = (await GetOrCreateReferralWalletAsync(referringWalletAddress, cancellationToken).ConfigureAwait(false)).Data;

                // get or add referred wallet
                var referredWallet = (await GetOrCreateReferralWalletAsync(referredWalletAddress, cancellationToken).ConfigureAwait(false)).Data;

                // get or add referral data
                var referralData = await _dbContext.ReferralDatas
                    .FirstOrDefaultAsync(d => d.ReferredWalletId == referredWallet.Id && d.ReferringWalletId == referringWallet.Id, cancellationToken)
                    .ConfigureAwait(false);
                int referralLevel = await GetReferralLevelAsync(referringWallet.Id).ConfigureAwait(false);
                if (referralData == null)
                {
                    referralData = new ReferralData(referredWallet.Id, referringWallet.Id, referralLevel);
                    _dbContext.ReferralDatas.Add(referralData);
                }

                // get or add reward data
                var rewardData = await _dbContext.RewardDatas
                    .FirstOrDefaultAsync(d => d.RewardedWalletId == referringWallet.Id, cancellationToken)
                    .ConfigureAwait(false);
                if (rewardData == null)
                {
                    rewardData = new RewardData(referringWallet.Id);
                    _dbContext.RewardDatas.Add(rewardData);
                }

                referringWallet.SetReward(rewardData.Id);

                rewardData.IncreaseTotalAmount(_settings.ReferralRewardBaseAmount, _settings.UseMultilevel, referralLevel);
                if (_settings.UseMultilevel)
                {
                    // add reward distribution for parent referrals?
                }

                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                    return await Result<ReferralData>.SuccessAsync(referralData, "Referral successfully added.")
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "An error occurred while adding referral.");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException e)
            {
                _logger.LogCritical(e, "An timeout error occurred while adding referral.");
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An error occurred while adding referral.");
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            }

            return await Result<ReferralData>.FailAsync("An error occurred while adding referral.").ConfigureAwait(false);
        }

        private string? MaskWalletAddress(string address)
        {
            var maskedAttribute = new LogMaskedAttribute(showFirst: 4, showLast: 4, preserveLength: true);
            object? maskedAddress = maskedAttribute.MaskValue(address);
            return maskedAddress as string;
        }

        private async Task<int> GetReferralLevelAsync(
            Guid referredWalletId)
        {
            var referralData = await _readDbContext.ReferralDatas
                .FirstOrDefaultAsync(d => d.ReferredWalletId == referredWalletId)
                .ConfigureAwait(false);
            if (referralData == null)
            {
                return 0;
            }

            if (referralData.ReferralLevel == 5)
            {
                return 5;
            }

            return referralData.ReferralLevel + 1;
        }
    }
}