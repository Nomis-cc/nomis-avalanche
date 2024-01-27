// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralDbContext.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nomis.CurrentUserService.Interfaces;
using Nomis.DataAccess.Interfaces.EventLogging;
using Nomis.DataAccess.PostgreSql.Persistence.Abstractions;
using Nomis.DataAccess.PostgreSql.Referral.Extensions;
using Nomis.DataAccess.Referral.Interfaces.Contexts;
using Nomis.Domain.Referral.Entities;
using Nomis.Domain.Settings;

namespace Nomis.DataAccess.PostgreSql.Referral.Persistence
{
    /// <inheritdoc cref="IReferralDbContext"/>
    internal sealed class ReferralDbContext :
        AuditableDbContext,
        IReferralDbContext
    {
        /// <summary>
        /// Initialize <see cref="ReferralDbContext"/>.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="publisher"><see cref="IPublisher"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ReferralDbContext(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            DbContextOptions<ReferralDbContext> options,
            IEventLogger eventLogger,
            ICurrentUserService currentUserService,
            IPublisher publisher,
            IOptionsSnapshot<EntitySettings> entitySettings)
        : base(options, eventLogger, currentUserService, publisher, entitySettings)
        {
        }

        /// <inheritdoc/>
        public DbSet<ReferralWallet> ReferralWallets { get; set; }

        /// <inheritdoc/>
        public DbSet<ReferralData> ReferralDatas { get; set; }

        /// <inheritdoc/>
        public DbSet<RewardData> RewardDatas { get; set; }

        /// <inheritdoc/>
        protected override string Schema => "Referral";

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyReferralConfiguration();
        }
    }
}