// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralWalletUpdatedEvent.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Domain.Abstractions;
using Nomis.Domain.Referral.Entities;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Domain.Referral.Events
{
    /// <summary>
    /// Update referral wallet domain event.
    /// </summary>
    public class ReferralWalletUpdatedEvent :
        DomainEvent<ReferralWallet>
    {
        /// <summary>
        /// Initialize <see cref="ReferralWalletUpdatedEvent"/>.
        /// </summary>
        public ReferralWalletUpdatedEvent()
            : base(Guid.Empty, string.Empty, null)
        {
            WalletAddress = string.Empty;
            ReferralCode = string.Empty;
            RewardId = Guid.Empty;
        }

        /// <summary>
        /// Initialize <see cref="ReferralWalletUpdatedEvent"/>.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="eventDescription">Event description.</param>
        public ReferralWalletUpdatedEvent(
            ReferralWallet entity,
            string eventDescription)
            : base(
                entity.Id,
                eventDescription,
                entity.Version)
        {
            Id = entity.Id;
            WalletAddress = entity.WalletAddress;
            ReferralCode = entity.ReferralCode;
            RewardId = entity.RewardId;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="ReferralWallet.WalletAddress"/>
        [JsonInclude]
        public string WalletAddress { get; private set; }

        /// <inheritdoc cref="ReferralWallet.ReferralCode"/>
        [JsonInclude]
        public string ReferralCode { get; private set; }

        /// <inheritdoc cref="ReferralWallet.RewardId"/>
        [JsonInclude]
        public Guid RewardId { get; private set; }
    }
}