// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralWalletRemovedEvent.cs" company="Nomis">
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
    /// Remove referral wallet domain event.
    /// </summary>
    public class ReferralWalletRemovedEvent :
        DomainEvent<ReferralWallet>
    {
        /// <summary>
        /// Initialize <see cref="ReferralWalletRemovedEvent"/>.
        /// </summary>
        public ReferralWalletRemovedEvent()
            : base(Guid.Empty, string.Empty, null)
        {
        }

        /// <summary>
        /// Initialize <see cref="ReferralWalletRemovedEvent"/>.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="version">Aggregate current version.</param>
        /// <param name="eventDescription">Event description.</param>
        public ReferralWalletRemovedEvent(
            Guid id,
            int? version,
            string eventDescription)
            : base(
                id,
                eventDescription,
                version)
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }
    }
}