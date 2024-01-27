// ------------------------------------------------------------------------------------------------------
// <copyright file="RewardData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Domain.Abstractions;
using Nomis.Domain.Contracts;
using Nomis.Domain.Referral.Events;

namespace Nomis.Domain.Referral.Entities
{
    /// <summary>
    /// Reward data.
    /// </summary>
    public class RewardData :
        AuditableAggregate,
        IVersionableByEvent<Guid, RewardData, RewardDataAddedEvent>,
        IVersionableByEvent<Guid, RewardData, RewardDataUpdatedEvent>,
        IVersionableByEvent<Guid, RewardData, RewardDataRemovedEvent>
    {
        /// <summary>
        /// Initialize <see cref="RewardData"/>.
        /// </summary>
        public RewardData()
        {
            RewardedWalletId = Guid.Empty;
            TotalAmount = 0;
            PaidAmount = 0;
        }

        /// <summary>
        /// Initialize <see cref="RewardData"/>.
        /// </summary>
        /// <param name="rewardedWalletId">Rewarded wallet id.</param>
        public RewardData(
            Guid rewardedWalletId)
        {
            RewardedWalletId = rewardedWalletId;
            TotalAmount = 0;
            PaidAmount = 0;
        }

        /// <summary>
        /// Rewarded wallet id.
        /// </summary>
        [JsonInclude]
        public Guid RewardedWalletId { get; private set; }

        /// <summary>
        /// Rewarded wallet.
        /// </summary>
        [JsonIgnore]
        public virtual ReferralWallet RewardedWallet { get; private set; } = null!;

        /// <summary>
        /// Total reward amount.
        /// </summary>
        [JsonInclude]
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Amount already paid.
        /// </summary>
        [JsonInclude]
        public decimal PaidAmount { get; private set; }

        /// <summary>
        /// Reward is fully paid.
        /// </summary>
        public bool IsFullyPaid => TotalAmount - PaidAmount == 0;

        /// <summary>
        /// Last paid date and time.
        /// </summary>
        [JsonInclude]
        public DateTime? LastPaidTime { get; private set; }

        #region IVersionableByEvent

        /// <inheritdoc/>
        public void When(RewardDataAddedEvent @event)
        {
            RewardedWalletId = @event.RewardedWalletId;
            TotalAmount = @event.TotalAmount;
            PaidAmount = @event.PaidAmount;
            LastPaidTime = @event.LastPaidTime;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(RewardDataUpdatedEvent @event)
        {
            RewardedWalletId = @event.RewardedWalletId;
            TotalAmount = @event.TotalAmount;
            PaidAmount = @event.PaidAmount;
            LastPaidTime = @event.LastPaidTime;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(RewardDataRemovedEvent @event)
        {
            IncrementVersion();
        }

        #endregion IVersionableByEvent

        /// <summary>
        /// Set last paid date and time.
        /// </summary>
        /// <param name="dateTime">Date and time.</param>
        /// <returns>Returns <see cref="RewardData"/>.</returns>
        public RewardData SetLastPaidTime(
            DateTime dateTime)
        {
            LastPaidTime = dateTime;
            return this;
        }

        /// <summary>
        /// Increase paid amount.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <returns>Returns <see cref="RewardData"/> with increased paid amount.</returns>
        public RewardData IncreasePaidAmount(decimal amount)
        {
            PaidAmount += amount;
            return this;
        }

        /// <summary>
        /// Decrease paid amount.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <returns>Returns <see cref="RewardData"/> with decreased paid amount.</returns>
        public RewardData DecreasePaidAmount(decimal amount)
        {
            PaidAmount -= amount;
            return this;
        }

        /// <summary>
        /// Increase total amount.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="useMultilevel">Use multilevel referral system.</param>
        /// <param name="referralLevel">Referral level.</param>
        /// <returns>Returns <see cref="RewardData"/> with increased total amount.</returns>
        public RewardData IncreaseTotalAmount(
            decimal amount,
            bool useMultilevel,
            int referralLevel)
        {
            decimal calculatedAmount = amount;

            if (useMultilevel)
            {
                switch (referralLevel)
                {
                    case 0:
                        calculatedAmount = amount;
                        break;
                    case 1:
                        calculatedAmount -= amount * 0.25m;
                        break;
                    case 2:
                        calculatedAmount -= amount * 0.4m;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    default:
                        calculatedAmount -= amount * 0.5m;
                        break;
                }
            }

            TotalAmount += calculatedAmount;
            return this;
        }

        /// <summary>
        /// Increase total amount.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <returns>Returns <see cref="RewardData"/> with increased total amount.</returns>
        public RewardData IncreaseTotalAmount(
            decimal amount)
        {
            TotalAmount += amount;
            return this;
        }

        /// <inheritdoc/>
        protected override void Apply(IDomainEvent @event)
        {
            When((dynamic)@event);
        }
    }
}