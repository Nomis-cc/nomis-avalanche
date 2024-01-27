// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralDataUpdatedEvent.cs" company="Nomis">
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
    /// Update referral data domain event.
    /// </summary>
    public class ReferralDataUpdatedEvent :
        DomainEvent<ReferralData>
    {
        /// <summary>
        /// Initialize <see cref="ReferralDataUpdatedEvent"/>.
        /// </summary>
        public ReferralDataUpdatedEvent()
            : base(Guid.Empty, string.Empty, null)
        {
            ReferredWalletId = Guid.Empty;
            ReferringWalletId = Guid.Empty;
            ReferralLevel = 0;
        }

        /// <summary>
        /// Initialize <see cref="ReferralDataUpdatedEvent"/>.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="eventDescription">Event description.</param>
        public ReferralDataUpdatedEvent(
            ReferralData entity,
            string eventDescription)
            : base(
                entity.Id,
                eventDescription,
                entity.Version)
        {
            Id = entity.Id;
            ReferredWalletId = entity.ReferredWalletId;
            ReferringWalletId = entity.ReferringWalletId;
            ReferralLevel = entity.ReferralLevel;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="ReferralData.ReferredWalletId"/>
        [JsonInclude]
        public Guid ReferredWalletId { get; private set; }

        /// <inheritdoc cref="ReferralData.ReferringWalletId"/>
        [JsonInclude]
        public Guid ReferringWalletId { get; private set; }

        /// <inheritdoc cref="ReferralData.ReferralLevel"/>
        [JsonInclude]
        public int ReferralLevel { get; private set; }
    }
}