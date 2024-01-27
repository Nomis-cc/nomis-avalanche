// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralWallet.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text;
using System.Text.Json.Serialization;

using Nomis.Domain.Abstractions;
using Nomis.Domain.Contracts;
using Nomis.Domain.Referral.Events;

namespace Nomis.Domain.Referral.Entities
{
    /// <summary>
    /// Referral wallet.
    /// </summary>
    public class ReferralWallet :
        AuditableAggregate,
        IVersionableByEvent<Guid, ReferralWallet, ReferralWalletAddedEvent>,
        IVersionableByEvent<Guid, ReferralWallet, ReferralWalletUpdatedEvent>,
        IVersionableByEvent<Guid, ReferralWallet, ReferralWalletRemovedEvent>
    {
        private const int ReferralCodeLength = 10;

        /// <summary>
        /// Initialize <see cref="ReferralWallet"/>.
        /// </summary>
        public ReferralWallet()
        {
            WalletAddress = string.Empty;
            ReferralCode = string.Empty;
            Referrals = new HashSet<ReferralData>();
            Reward = new RewardData();
        }

        /// <summary>
        /// Initialize <see cref="ReferralWallet"/>.
        /// </summary>
        /// <param name="walletAddress">Wallet address.</param>
        public ReferralWallet(
            string walletAddress)
        {
            WalletAddress = walletAddress;
            ReferralCode = GenerateRandomReferralCode();
            Referrals = new HashSet<ReferralData>();
            Reward = new RewardData(Id);
        }

        /// <summary>
        /// Request address.
        /// </summary>
        [JsonInclude]
        public string WalletAddress { get; private set; }

        /// <summary>
        /// Referral code.
        /// </summary>
        [JsonInclude]
        public string ReferralCode { get; private set; }

        /// <summary>
        /// Referral data list.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ReferralData> Referrals { get; private set; }

        /// <summary>
        /// Reward id.
        /// </summary>
        [JsonInclude]
        public Guid RewardId { get; private set; }

        /// <summary>
        /// Reward data.
        /// </summary>
        [JsonIgnore]
        public virtual RewardData Reward { get; private set; }

        #region IVersionableByEvent

        /// <inheritdoc/>
        public void When(ReferralWalletAddedEvent @event)
        {
            WalletAddress = @event.WalletAddress;
            ReferralCode = @event.ReferralCode;
            RewardId = @event.RewardId;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(ReferralWalletUpdatedEvent @event)
        {
            WalletAddress = @event.WalletAddress;
            ReferralCode = @event.ReferralCode;
            RewardId = @event.RewardId;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(ReferralWalletRemovedEvent @event)
        {
            IncrementVersion();
        }

        #endregion IVersionableByEvent

        /// <summary>
        /// Set reward
        /// </summary>
        /// <param name="rewardId">Reward id.</param>
        /// <returns>Return <see cref="ReferralWallet"/> with reward.</returns>
        public ReferralWallet SetReward(
            Guid rewardId)
        {
            RewardId = rewardId;
            return this;
        }

        /// <summary>
        /// Add referral.
        /// </summary>
        /// <param name="referral">Referal.</param>
        /// <returns>Returns <see cref="ReferralWallet"/> with added referral.</returns>
        public ReferralWallet AddReferral(ReferralData referral)
        {
            Referrals.Add(referral);
            return this;
        }

        /// <summary>
        /// Refresh referral code.
        /// </summary>
        /// <returns>Returns <see cref="ReferralWallet"/> with refreshed referral code.</returns>
        public ReferralWallet RefreshReferralCode()
        {
            ReferralCode = GenerateRandomReferralCode();
            return this;
        }

        /// <inheritdoc/>
        protected override void Apply(IDomainEvent @event)
        {
            When((dynamic)@event);
        }

        private static string GenerateRandomReferralCode()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var referralCode = new StringBuilder(ReferralCodeLength);

            for (int i = 0; i < ReferralCodeLength; i++)
            {
                int randomIndex = random.Next(allowedChars.Length);
                referralCode.Append(allowedChars[randomIndex]);
            }

            return referralCode.ToString();
        }
    }
}