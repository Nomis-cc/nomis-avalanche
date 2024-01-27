// ------------------------------------------------------------------------------------------------------
// <copyright file="IReferralReadDbContext.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Domain;
using Nomis.Domain.Referral.Entities;

namespace Nomis.DataAccess.Referral.Interfaces.Contexts
{
    /// <summary>
    /// The database read context for accessing referral-related data.
    /// </summary>
    public interface IReferralReadDbContext :
        IDbContextInterface
    {
        /// <inheritdoc cref="IReferralDbContext.ReferralWallets"/>
        public IQueryable<ReferralWallet> ReferralWallets { get; }

        /// <inheritdoc cref="IReferralDbContext.ReferralDatas"/>
        public IQueryable<ReferralData> ReferralDatas { get; }

        /// <inheritdoc cref="IReferralDbContext.RewardDatas"/>
        public IQueryable<RewardData> RewardDatas { get; }
    }
}