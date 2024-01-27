// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralData.cs" company="Nomis">
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
    /// Referral data.
    /// </summary>
    public class ReferralData :
        AuditableAggregate,
        IVersionableByEvent<Guid, ReferralData, ReferralDataAddedEvent>,
        IVersionableByEvent<Guid, ReferralData, ReferralDataUpdatedEvent>,
        IVersionableByEvent<Guid, ReferralData, ReferralDataRemovedEvent>
    {
        /// <summary>
        /// Initialize <see cref="ReferralData"/>.
        /// </summary>
        public ReferralData()
        {
            ReferredWalletId = Guid.Empty;
            ReferringWalletId = Guid.Empty;
            ReferralLevel = 0;
        }

        /// <summary>
        /// Initialize <see cref="ReferralData"/>.
        /// </summary>
        /// <param name="referredWalletId">Referred wallet id.</param>
        /// <param name="referringWalletId">Referring wallet id.</param>
        /// <param name="referralLevel">Referral level.</param>
        public ReferralData(
            Guid referredWalletId,
            Guid referringWalletId,
            int referralLevel)
        {
            ReferredWalletId = referredWalletId;
            ReferringWalletId = referringWalletId;
            ReferralLevel = referralLevel;
        }

        /// <summary>
        /// Referred wallet id.
        /// </summary>
        [JsonInclude]
        public Guid ReferredWalletId { get; private set; }

        /// <summary>
        /// Referred wallet.
        /// </summary>
        public virtual ReferralWallet ReferredWallet { get; private set; } = null!;

        /// <summary>
        /// Referring wallet id.
        /// </summary>
        [JsonInclude]
        public Guid ReferringWalletId { get; private set; }

        /// <summary>
        /// Referring wallet.
        /// </summary>
        [JsonIgnore]
        public virtual ReferralWallet ReferringWallet { get; private set; } = null!;

        /// <summary>
        /// Referral level.
        /// </summary>
        [JsonInclude]
        public int ReferralLevel { get; private set; }

        #region IVersionableByEvent

        /// <inheritdoc/>
        public void When(ReferralDataAddedEvent @event)
        {
            ReferredWalletId = @event.ReferredWalletId;
            ReferringWalletId = @event.ReferringWalletId;
            ReferralLevel = @event.ReferralLevel;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(ReferralDataUpdatedEvent @event)
        {
            ReferredWalletId = @event.ReferredWalletId;
            ReferringWalletId = @event.ReferringWalletId;
            ReferralLevel = @event.ReferralLevel;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(ReferralDataRemovedEvent @event)
        {
            IncrementVersion();
        }

        #endregion IVersionableByEvent

        /// <inheritdoc/>
        protected override void Apply(IDomainEvent @event)
        {
            When((dynamic)@event);
        }
    }
}