// ------------------------------------------------------------------------------------------------------
// <copyright file="IHttpClientRetryingSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

namespace Nomis.Utils.Contracts.Settings
{
    /// <summary>
    /// <see cref="HttpClient"/> retrying settings.
    /// </summary>
    public interface IHttpClientRetryingSettings
    {
        /// <summary>
        /// Use <see cref="HttpClient"/> requests retrying.
        /// </summary>
        public bool UseHttpClientRetrying { get; init; }

        /// <summary>
        /// Max retries count.
        /// </summary>
        public int MaxRetries { get; init; }

        /// <summary>
        /// Use default retry timeout.
        /// </summary>
        public bool UseDefaultRetryTimeout { get; init; }

        /// <summary>
        /// Default retry timeout.
        /// </summary>
        public TimeSpan DefaultRetryTimeout { get; init; }

        /// <summary>
        /// Retry timeouts for specific response status codes.
        /// </summary>
        public IDictionary<HttpStatusCode, TimeSpan> RetryTimeouts { get; init; }
    }
}