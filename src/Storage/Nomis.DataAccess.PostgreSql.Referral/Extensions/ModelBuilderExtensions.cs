// ------------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nomis.DataAccess.PostgreSql.Referral.Persistence.Configurations;

namespace Nomis.DataAccess.PostgreSql.Referral.Extensions
{
    /// <summary>
    /// <see cref="ModelBuilder"/> extension methods.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Apply referral-related data access context configuration.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/>.</param>
        public static void ApplyReferralConfiguration(this ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ReferralWalletConfiguration())
                .ApplyConfiguration(new ReferralDataConfiguration())
                .ApplyConfiguration(new RewardDataConfiguration());
        }
    }
}