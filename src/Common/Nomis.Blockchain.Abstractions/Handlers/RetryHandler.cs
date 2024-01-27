// ------------------------------------------------------------------------------------------------------
// <copyright file="RetryHandler.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Nomis.Utils.Contracts.Settings;

namespace Nomis.Blockchain.Abstractions.Handlers
{
    internal class RetryHandler :
            DelegatingHandler
    {
        private sealed class DefaultSettings :
            IHttpClientRetryingSettings
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
        }

        private readonly IHttpClientRetryingSettings _settings;

        /// <summary>
        /// Initialize <see cref="RetryHandler"/>.
        /// </summary>
        public RetryHandler()
        {
            _settings = new DefaultSettings
            {
                DefaultRetryTimeout = TimeSpan.FromSeconds(3),
                MaxRetries = 5,
                UseDefaultRetryTimeout = false,
                UseHttpClientRetrying = true
            };
        }

        /// <summary>
        /// Initialize <see cref="RetryHandler"/>.
        /// </summary>
        /// <param name="settings"><see cref="IHttpClientRetryingSettings"/>.</param>
        public RetryHandler(
            IHttpClientRetryingSettings settings)
        {
            _settings = settings;
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!_settings.UseHttpClientRetrying)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            var response = new HttpResponseMessage();
            for (int i = 0; i <= _settings.MaxRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string resString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    bool fakeOk = resString.Contains("Max rate limit reached");
                    if (!fakeOk)
                    {
                        return response;
                    }
                }

                if (_settings.RetryTimeouts.TryGetValue(response.StatusCode, out var retryTimeout))
                {
                    await Task.Delay(retryTimeout, cancellationToken).ConfigureAwait(false);
                }
                else if (_settings.UseDefaultRetryTimeout)
                {
                    await Task.Delay(_settings.DefaultRetryTimeout, cancellationToken).ConfigureAwait(false);
                }
            }

            return response;
        }
    }
}