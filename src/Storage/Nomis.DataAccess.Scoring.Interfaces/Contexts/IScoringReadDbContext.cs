// ------------------------------------------------------------------------------------------------------
// <copyright file="IScoringReadDbContext.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Domain;
using Nomis.Domain.Scoring.Entities;

namespace Nomis.DataAccess.Scoring.Interfaces.Contexts
{
    /// <summary>
    /// The database read context for accessing scoring-related data.
    /// </summary>
    public interface IScoringReadDbContext :
        IDbContextInterface
    {
        /// <inheritdoc cref="IScoringDbContext.ScoringDatas"/>
        public IQueryable<ScoringData> ScoringDatas { get; }
    }
}