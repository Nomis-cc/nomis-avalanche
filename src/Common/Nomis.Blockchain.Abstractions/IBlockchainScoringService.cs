// ------------------------------------------------------------------------------------------------------
// <copyright file="IBlockchainScoringService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Wrapper;

namespace Nomis.Blockchain.Abstractions
{
    /// <summary>
    /// Blockchain scoring service.
    /// </summary>
    public interface IBlockchainScoringService :
        IService
    {
        /// <summary>
        /// <see cref="ILogger"/>.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Get blockchain wallet stats by address.
        /// </summary>
        /// <typeparam name="TWalletStatsRequest">The request type.</typeparam>
        /// <typeparam name="TWalletScore">The wallet score type.</typeparam>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <typeparam name="TTransactionIntervalData"><see cref="ITransactionIntervalData"/>.</typeparam>
        /// <param name="request">Request for getting the wallet stats.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns the wallet score result.</returns>
        Task<Result<TWalletScore>> GetWalletStatsAsync<TWalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(
            TWalletStatsRequest request,
            CancellationToken cancellationToken = default)
            where TWalletStatsRequest : WalletStatsRequest
            where TWalletScore : IWalletScore<TWalletStats, TTransactionIntervalData>, new()
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData;

        /// <summary>
        /// Get blockchain wallets stats by addresses.
        /// </summary>
        /// <typeparam name="TWalletStatsRequest">The request type.</typeparam>
        /// <typeparam name="TWalletScore">The wallet score type.</typeparam>
        /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
        /// <typeparam name="TTransactionIntervalData"><see cref="ITransactionIntervalData"/>.</typeparam>
        /// <param name="requests">The list of requests for getting the wallets stats.</param>
        /// <param name="concurrentRequestCount">Concurrent request count.</param>
        /// <param name="delayInMilliseconds">Delay in milliseconds between calls.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns a list of wallets score results.</returns>
        public async Task<Result<List<TWalletScore>>> GetWalletsStatsAsync<TWalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(
            IList<TWalletStatsRequest> requests,
            int concurrentRequestCount = 1,
            int delayInMilliseconds = 500,
            CancellationToken cancellationToken = default)
            where TWalletStatsRequest : WalletStatsRequest
            where TWalletScore : class, IWalletScore<TWalletStats, TTransactionIntervalData>, new()
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData
        {
            int counter = 0;

            var throttler = new SemaphoreSlim(concurrentRequestCount);

            var result = new List<TWalletScore>();
            var tasks = requests
                .Select(async request =>
                {
                    await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);

                    var task = GetWalletStatsAsync<WalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(request, cancellationToken);
                    _ = task.ContinueWith(
                        async _ =>
                        {
                            Interlocked.Increment(ref counter);

                            Logger.LogWarning("{Counter} - Stats for {Wallet} calculated!", counter, request.Address);
                            await Task.Delay(delayInMilliseconds, cancellationToken).ConfigureAwait(false);
                            throttler.Release();
                        }, cancellationToken);

                    try
                    {
                        var response = await task.ConfigureAwait(false);

                        Logger.LogInformation("{Counter} - Score for {Wallet} = {Score}", counter, request.Address, response.Data.Score);
                        return response;
                    }
                    catch (HttpRequestException)
                    {
                        return await task.ConfigureAwait(false);
                    }
                });
            var statsResults = await Task.WhenAll(tasks).ConfigureAwait(false);
            result.AddRange(statsResults
                .Where(r => r.Succeeded)
                .Select(r => r.Data)
                .Where(x => x != null));

            return await Result<List<TWalletScore>>.SuccessAsync(result, "Got blockchain wallets score.").ConfigureAwait(false);
        }
    }
}