// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoringService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nomis.DataAccess.Scoring.Interfaces.Contexts;
using Nomis.Domain.Scoring.Entities;
using Nomis.Domain.Scoring.Events;
using Nomis.ScoringService.Interfaces;
using Nomis.ScoringService.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Enums;

namespace Nomis.ScoringService
{
    /// <inheritdoc cref="IScoringService"/>
    internal sealed class ScoringService :
        IScoringService,
        ITransientService
    {
        private readonly IScoringDbContext _dbContext;
        private readonly IScoringReadDbContext _readDbContext;
        private readonly ILogger<ScoringService> _logger;

        /// <summary>
        /// Initialize <see cref="ScoringService"/>.
        /// </summary>
        /// <param name="dbContext"><see cref="IScoringDbContext"/>.</param>
        /// <param name="readDbContext"><see cref="IScoringReadDbContext"/>.</param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/>.</param>
        public ScoringService(
            IScoringDbContext dbContext,
            IScoringReadDbContext readDbContext,
            ILogger<ScoringService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _readDbContext = readDbContext ?? throw new ArgumentNullException(nameof(readDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task SaveScoringDataToDatabaseAsync(
            ScoringData scoringData,
            CancellationToken cancellationToken = default)
        {
            scoringData
                .AddDomainEvent(new ScoringDataAddedEvent(scoringData, $"Scoring data for {scoringData.RequestAddress} wallet added."));
            _dbContext.ScoringDatas.Add(scoringData);

            try
            {
                _logger.LogDebug("Calculated {Score} data for {Wallet} wallet and {ChainId} blockchain with {CalculationModel} and stats {Stats}", scoringData.Score, scoringData.RequestAddress, scoringData.Blockchain, scoringData.CalculationModel.ToString(), scoringData.StatData);
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {
                try
                {
                    await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, "An error occurred while saving scoring data to database.");
                }
            }
        }

        /// <inheritdoc />
        public async Task<StatDataResponse?> GetLastScoringStatsDataFromDatabaseAsync(
            string wallet,
            ulong chainId,
            ScoringCalculationModel scoringCalculationModel,
            TimeSpan timeLimit,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var scoringData = await _readDbContext
                    .ScoringDatas
                    .Where(d => d.Blockchain == chainId && d.CalculationModel == scoringCalculationModel && (d.RequestAddress.Equals(wallet) || (d.ResolvedAddress != null && d.ResolvedAddress!.Equals(wallet))))
                    .OrderByDescending(d => d.CreatedOn)
                    .Select(x => new StatDataResponse(x.StatData, x.DeletedOn, x.DeletedBy, x.CreatedOn))
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (scoringData?.CreatedOn != null && DateTime.UtcNow.Subtract(scoringData.CreatedOn ?? DateTime.UtcNow - timeLimit.Add(new TimeSpan(1))) >= timeLimit)
                {
                    return null;
                }

                return scoringData;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An error occurred while getting last scoring data.");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<IList<StatDataResponse>?> GetScoringStatsDataFromDatabaseAsync(
            string wallet,
            ulong chainId,
            ScoringCalculationModel scoringCalculationModel,
            TimeSpan? timeStart,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var scoringData = await _readDbContext
                    .ScoringDatas
                    .Where(d => timeStart == null || d.CreatedOn > DateTime.UtcNow - timeStart)
                    .Where(d => d.Blockchain == chainId && d.CalculationModel == scoringCalculationModel && (d.RequestAddress.Equals(wallet) || (d.ResolvedAddress != null && d.ResolvedAddress!.Equals(wallet))))
                    .OrderByDescending(d => d.CreatedOn)
                    .Select(x => new StatDataResponse(x.StatData, x.DeletedOn, x.DeletedBy, x.CreatedOn))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                return scoringData;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An error occurred while getting scoring data for wallet.");
                return null;
            }
        }
    }
}