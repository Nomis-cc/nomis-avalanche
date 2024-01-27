// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralDataRemovedEvent.cs" company="Nomis">
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
    /// Remove referral data domain event.
    /// </summary>
    public class ReferralDataRemovedEvent :
        DomainEvent<ReferralData>
    {
        /// <summary>
        /// Initialize <see cref="ReferralDataRemovedEvent"/>.
        /// </summary>
        public ReferralDataRemovedEvent()
            : base(Guid.Empty, string.Empty, null)
        {
        }

        /// <summary>
        /// Initialize <see cref="ReferralDataRemovedEvent"/>.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="version">Aggregate current version.</param>
        /// <param name="eventDescription">Event description.</param>
        public ReferralDataRemovedEvent(
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