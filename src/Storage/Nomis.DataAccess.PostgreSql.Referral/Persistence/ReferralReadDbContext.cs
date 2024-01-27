// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralReadDbContext.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nomis.DataAccess.Referral.Interfaces.Contexts;
using Nomis.Domain.Referral.Entities;

namespace Nomis.DataAccess.PostgreSql.Referral.Persistence
{
    /// <inheritdoc cref="IReferralReadDbContext"/>
    internal sealed class ReferralReadDbContext :
        IReferralReadDbContext
    {
        private readonly IReferralDbContext _dbContext;

        /// <summary>
        /// Initialize <see cref="ReferralReadDbContext"/>.
        /// </summary>
        /// <param name="dbContext"><see cref="IReferralDbContext"/>.</param>
        public ReferralReadDbContext(
            IReferralDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public IQueryable<ReferralWallet> ReferralWallets => _dbContext.ReferralWallets.AsNoTracking();

        /// <inheritdoc />
        public IQueryable<ReferralData> ReferralDatas => _dbContext.ReferralDatas.AsNoTracking();

        /// <inheritdoc />
        public IQueryable<RewardData> RewardDatas => _dbContext.RewardDatas.AsNoTracking();
    }
}