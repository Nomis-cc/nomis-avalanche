// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralDataConfiguration.cs" company="Nomis">
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
    /// Database Model Configuration for <see cref="ReferralData"/>.
    /// </summary>
    public class ReferralDataConfiguration :
        IEntityTypeConfiguration<ReferralData>
    {
        /// <inheritdoc/>
        public void Configure(
            EntityTypeBuilder<ReferralData> entity)
        {
            entity
                .HasOne(rd => rd.ReferredWallet)
                .WithMany()
                .HasForeignKey(rd => rd.ReferredWalletId);

            // TODO - configure
        }
    }
}