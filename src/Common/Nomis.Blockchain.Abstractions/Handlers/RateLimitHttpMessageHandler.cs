// ------------------------------------------------------------------------------------------------------
// <copyright file="RateLimitHttpMessageHandler.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net;

using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Utils.Contracts.Settings;

namespace Nomis.Blockchain.Abstractions.Handlers
{
    internal class RateLimitHttpMessageHandler<TSettings> :
        DelegatingHandler
        where TSettings : class, IHttpClientRetryingSettings, IRateLimitSettings, new()
    {
        private static readonly ConcurrentDictionary<string, AsyncRateLimitedSemaphore> CallsPerSecondSemaphore = new();
        private static readonly Random Random = new();

        private readonly TSettings _settings;
        private readonly ILogger? _logger;
        private readonly string? _clientName;

        /// <summary>
        /// Initialize <see cref="RateLimitHttpMessageHandler{TSettings}"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/>.</param>
        /// <param name="settings"><see cref="IHttpClientRetryingSettings"/>.</param>
        /// <param name="clientName">Client name.</param>
        public RateLimitHttpMessageHandler(
            ILogger? logger,
            TSettings? settings = null,
            string? clientName = null)
        {
            _logger = logger;
            _clientName = clientName;
            _settings = settings ?? new TSettings
            {
                MaxApiCallsPerSecond = 5,
                UseHttpClientRetrying = true,
                MaxRetries = 3,
                UseDefaultRetryTimeout = true,
                DefaultRetryTimeout = TimeSpan.FromSeconds(1)
            };
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!_settings.UseHttpClientRetrying || request.RequestUri == null)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(true);
            }

            int retryAttempt = 0;
            HttpResponseMessage result;

            try
            {
                // uncomment for checking API and client with proxy rotation
                // string apiKey = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("apiKey") ?? "none";
                // Debug.WriteLine($"API key: {apiKey}");
                // Debug.WriteLine($"Client name: {_clientName}");

                var semaphore = CallsPerSecondSemaphore.GetOrAdd(_clientName ?? "default", new AsyncRateLimitedSemaphore((int)_settings.MaxApiCallsPerSecond, TimeSpan.FromSeconds(1)));

                await semaphore.WaitAsync().ConfigureAwait(true);

                do
                {
                    // string walletAddress = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("address") ?? "none";
                    result = await base.SendAsync(request, cancellationToken).ConfigureAwait(true);
                    if (result.StatusCode != HttpStatusCode.TooManyRequests && result.IsSuccessStatusCode)
                    {
                        string resString = await result.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(true);
                        bool fakeOk = resString.Contains("Max rate limit reached", StringComparison.OrdinalIgnoreCase) || resString.Contains("NOTOK", StringComparison.OrdinalIgnoreCase);
                        if (!fakeOk)
                        {
                            return result;
                        }

                        // Debug.WriteLine($"{_clientName}: Fake OK for API key {apiKey} and wallet {walletAddress}. Response: {resString}");
                        // _logger?.LogWarning("{ClientName}: Fake OK for wallet {WalletAddress}. Response: {Response}", _clientName, walletAddress, resString);
                    }

                    // Debug.WriteLine($"{_clientName}: Not success for API key {apiKey} and wallet {walletAddress}. Status code: {result.StatusCode.ToString()}");
                    // _logger?.LogWarning("{ClientName}: Not success for wallet {WalletAddress}. StatusCode: {StatusCode}", _clientName, walletAddress, result.StatusCode);

                    retryAttempt++;

                    if (_settings.RetryTimeouts.TryGetValue(result.StatusCode, out var retryTimeout))
                    {
                        // Debug.WriteLine($"{_clientName} (attempt: {retryAttempt}): Delay for {retryTimeout + TimeSpan.FromMilliseconds(100 * (Math.Pow(2, retryAttempt - 1) - 1))} for API key {apiKey}.");
                        await Task.Delay(retryTimeout + GetDelay(retryAttempt), cancellationToken).ConfigureAwait(true);
                    }
                    else if (_settings.UseDefaultRetryTimeout)
                    {
                        // Debug.WriteLine($"{_clientName} (attempt: {retryAttempt}): Default delay for {_settings.DefaultRetryTimeout + TimeSpan.FromMicroseconds(100 * (Math.Pow(2, retryAttempt - 1) - 1))} for API key {apiKey}.");
                        await Task.Delay(_settings.DefaultRetryTimeout + GetDelay(retryAttempt), cancellationToken).ConfigureAwait(true);
                    }

                    await semaphore.WaitAsync().ConfigureAwait(true);
                }
                while (retryAttempt <= _settings.MaxRetries);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "There is an error while rate limiting external API requests.");
                throw;
            }

            return result;
        }

        private static TimeSpan GetDelay(int retryAttempt) =>
            TimeSpan.FromMicroseconds(Random.Next(0, 100) * (Math.Pow(2, retryAttempt - 1) - 1));

        private sealed class AsyncRateLimitedSemaphore
        {
            private readonly int _maxCount;

            private readonly object _resetTimeLock = new();
            private readonly TimeSpan _resetTimeSpan;

            private readonly SemaphoreSlim _semaphore;
            private long _nextResetTimeTicks;

            public AsyncRateLimitedSemaphore(
                int maxCount,
                TimeSpan resetTimeSpan)
            {
                _maxCount = maxCount;
                _resetTimeSpan = resetTimeSpan;

                _semaphore = new SemaphoreSlim(maxCount, maxCount);
                _nextResetTimeTicks = (DateTimeOffset.UtcNow + _resetTimeSpan).UtcTicks;
            }

            public async Task WaitAsync()
            {
                // attempt a reset in case it's been some time since the last wait
                TryResetSemaphore();

                var semaphoreTask = _semaphore.WaitAsync();

                // if there are no slots, need to keep trying to reset until one opens up
                while (!semaphoreTask.IsCompleted)
                {
                    long ticks = Interlocked.Read(ref _nextResetTimeTicks);
                    var nextResetTime = new DateTimeOffset(new DateTime(ticks, DateTimeKind.Utc));
                    var delayTime = nextResetTime - DateTimeOffset.UtcNow;

                    // delay until the next reset period
                    // can't delay a negative time so if it's already passed just continue with a completed task
                    var delayTask = delayTime >= TimeSpan.Zero ? Task.Delay(delayTime) : Task.CompletedTask;

                    await Task.WhenAny(semaphoreTask, delayTask).ConfigureAwait(true);

                    TryResetSemaphore();
                }
            }

            private void TryResetSemaphore()
            {
                // quick exit if before the reset time, no need to lock
                if (DateTimeOffset.UtcNow.UtcTicks <= Interlocked.Read(ref _nextResetTimeTicks))
                {
                    return;
                }

                // take a lock so only one reset can happen per period
                lock (_resetTimeLock)
                {
                    // need to check again in case a reset has already happened in this period
                    if (DateTimeOffset.UtcNow.UtcTicks <= Interlocked.Read(ref _nextResetTimeTicks))
                    {
                        return;
                    }

                    int releaseCount = _maxCount - _semaphore.CurrentCount;
                    if (releaseCount > 0)
                    {
                        _semaphore.Release(releaseCount);
                    }

                    long newResetTimeTicks = (DateTimeOffset.UtcNow + _resetTimeSpan).UtcTicks;
                    Interlocked.Exchange(ref _nextResetTimeTicks, newResetTimeTicks);
                }
            }
        }
    }

    internal sealed class DefaultSettings :
        IHttpClientRetryingSettings,
        IRateLimitSettings
    {
        /// <inheritdoc/>
        public bool UseHttpClientRetrying { get; init; }

        /// <inheritdoc/>
        public int MaxRetries { get; init; }

        /// <inheritdoc/>
        public bool UseDefaultRetryTimeout { get; init; }

        /// <inheritdoc/>
        public TimeSpan DefaultRetryTimeout { get; init; }

        /// <inheritdoc/>
        public IDictionary<HttpStatusCode, TimeSpan> RetryTimeouts { get; init; } =
            new Dictionary<HttpStatusCode, TimeSpan> { { HttpStatusCode.OK, TimeSpan.FromSeconds(3) }, { HttpStatusCode.TooManyRequests, TimeSpan.FromSeconds(3) } };

        /// <inheritdoc/>
        public uint MaxApiCallsPerSecond { get; init; }
    }
}