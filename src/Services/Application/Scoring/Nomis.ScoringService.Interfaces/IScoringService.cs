// ------------------------------------------------------------------------------------------------------
// <copyright file="IScoringService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Domain.Scoring.Entities;
using Nomis.ScoringService.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Enums;

namespace Nomis.ScoringService.Interfaces
{
    /// <summary>
    /// Scoring service.
    /// </summary>
    public interface IScoringService :
        IApplicationService
    {
        /// <summary>
        /// Save scoring data to database.
        /// </summary>
        /// <param name="scoringData">Wallet scoring data.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        public Task SaveScoringDataToDatabaseAsync(
            ScoringData scoringData,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get last scoring data from database.
        /// </summary>
        /// <param name="wallet">Wallet address.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="scoringCalculationModel">Scoring calculation model.</param>
        /// <param name="timeLimit">Time limit for get data.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns <see cref="StatDataResponse"/> with scoring stats data and created date from database.</returns>
        public Task<StatDataResponse?> GetLastScoringStatsDataFromDatabaseAsync(
            string wallet,
            ulong chainId,
            ScoringCalculationModel scoringCalculationModel,
            TimeSpan timeLimit,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get scoring data from database.
        /// </summary>
        /// <param name="wallet">Wallet address.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <param name="scoringCalculationModel">Scoring calculation model.</param>
        /// <param name="timeStart">How long ago to start getting data.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns list of <see cref="StatDataResponse"/> with scoring stats data from database.</returns>
        public Task<IList<StatDataResponse>?> GetScoringStatsDataFromDatabaseAsync(
            string wallet,
            ulong chainId,
            ScoringCalculationModel scoringCalculationModel,
            TimeSpan? timeStart,
            CancellationToken cancellationToken = default);
    }
}