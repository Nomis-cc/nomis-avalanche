// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralWalletConfiguration.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nomis.Domain.Referral.Entities;

namespace Nomis.DataAccess.PostgreSql.Referral.Persistence.Configurations
{
    /// <summary>
    /// Database Model Configuration for <see cref="ReferralWallet"/>.
    /// </summary>
    public class ReferralWalletConfiguration :
        IEntityTypeConfiguration<ReferralWallet>
    {
        /// <inheritdoc/>
        public void Configure(
            EntityTypeBuilder<ReferralWallet> entity)
        {
            entity
                .HasOne(rw => rw.Reward)
                .WithOne(rd => rd.RewardedWallet)
                .HasForeignKey<RewardData>(rd => rd.RewardedWalletId);

            // TODO - configure
        }
    }
}