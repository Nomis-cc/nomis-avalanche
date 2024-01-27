// ------------------------------------------------------------------------------------------------------
// <copyright file="IReferralDbContext.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nomis.DataAccess.Interfaces.Contexts;
using Nomis.Domain;
using Nomis.Domain.Referral.Entities;

namespace Nomis.DataAccess.Referral.Interfaces.Contexts
{
    /// <summary>
    /// The database context for accessing referral-related data.
    /// </summary>
    public interface IReferralDbContext :
        IAuditableDbContext,
        IDbContextInterface
    {
        /// <summary>
        /// Collection with referral wallet data.
        /// </summary>
        public DbSet<ReferralWallet> ReferralWallets { get; set; }

        /// <summary>
        /// Collection with referral data.
        /// </summary>
        public DbSet<ReferralData> ReferralDatas { get; set; }

        /// <summary>
        /// Collection with reward data.
        /// </summary>
        public DbSet<RewardData> RewardDatas { get; set; }
    }
}