// ------------------------------------------------------------------------------------------------------
// <copyright file="RewardDataUpdatedEvent.cs" company="Nomis">
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
    /// Update reward data domain event.
    /// </summary>
    public class RewardDataUpdatedEvent :
        DomainEvent<RewardData>
    {
        /// <summary>
        /// Initialize <see cref="RewardDataUpdatedEvent"/>.
        /// </summary>
        public RewardDataUpdatedEvent()
            : base(Guid.Empty, string.Empty, null)
        {
            RewardedWalletId = Guid.Empty;
        }

        /// <summary>
        /// Initialize <see cref="RewardDataUpdatedEvent"/>.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="eventDescription">Event description.</param>
        public RewardDataUpdatedEvent(
            RewardData entity,
            string eventDescription)
            : base(
                entity.Id,
                eventDescription,
                entity.Version)
        {
            Id = entity.Id;
            RewardedWalletId = entity.RewardedWalletId;
            TotalAmount = entity.TotalAmount;
            PaidAmount = entity.PaidAmount;
            LastPaidTime = entity.LastPaidTime;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="RewardData.RewardedWalletId"/>
        [JsonInclude]
        public Guid RewardedWalletId { get; private set; }

        /// <inheritdoc cref="RewardData.TotalAmount"/>
        [JsonInclude]
        public decimal TotalAmount { get; private set; }

        /// <inheritdoc cref="RewardData.PaidAmount"/>
        [JsonInclude]
        public decimal PaidAmount { get; private set; }

        /// <inheritdoc cref="RewardData.LastPaidTime"/>
        [JsonInclude]
        public DateTime? LastPaidTime { get; private set; }
    }
}