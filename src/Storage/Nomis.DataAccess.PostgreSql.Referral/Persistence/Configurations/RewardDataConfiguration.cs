// ------------------------------------------------------------------------------------------------------
// <copyright file="RewardDataConfiguration.cs" company="Nomis">
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
    /// Database Model Configuration for <see cref="RewardData"/>.
    /// </summary>
    public class RewardDataConfiguration :
        IEntityTypeConfiguration<RewardData>
    {
        /// <inheritdoc/>
        public void Configure(
            EntityTypeBuilder<RewardData> entity)
        {
            // TODO - configure
        }
    }
}