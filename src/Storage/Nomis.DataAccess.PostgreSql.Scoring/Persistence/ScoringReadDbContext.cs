// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoringReadDbContext.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nomis.DataAccess.Scoring.Interfaces.Contexts;
using Nomis.Domain.Scoring.Entities;

namespace Nomis.DataAccess.PostgreSql.Scoring.Persistence
{
    /// <inheritdoc cref="IScoringReadDbContext"/>
    internal sealed class ScoringReadDbContext :
        IScoringReadDbContext
    {
        private readonly IScoringDbContext _dbContext;

        /// <summary>
        /// Initialize <see cref="ScoringReadDbContext"/>.
        /// </summary>
        /// <param name="dbContext"><see cref="IScoringDbContext"/>.</param>
        public ScoringReadDbContext(
            IScoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public IQueryable<ScoringData> ScoringDatas => _dbContext.ScoringDatas.AsNoTracking();
    }
}