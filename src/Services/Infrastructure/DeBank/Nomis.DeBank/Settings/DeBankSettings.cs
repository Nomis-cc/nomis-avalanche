// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Settings;

namespace Nomis.DeBank.Settings
{
    /// <summary>
    /// DeBank settings.
    /// </summary>
    internal class DeBankSettings :
        ISettings,
        IRateLimitSettings,
        IHttpClientRetryingSettings
    {
        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://docs.cloud.debank.com/en/readme/api-pro-reference/"/>
        /// </remarks>
        public string? ApiBaseUrl { get; init; }

        /// <summary>
        /// API key.
        /// </summary>
        public string? ApiKey { get; init; }

        /// <inheritdoc/>
        public uint MaxApiCallsPerSecond { get; init; }

        /// <summary>
        /// Use DeBank caching.
        /// </summary>
        public bool UseDeBankCaching { get; init; }

        /// <summary>
        /// DeBank hold tokens cache duration.
        /// </summary>
        public TimeSpan DeBankHoldTokensCacheDuration { get; init; }

        /// <summary>
        /// DeBank tokens data cache duration.
        /// </summary>
        public TimeSpan DeBankTokensDataCacheDuration { get; init; }

        /// <summary>
        /// Http client timeout.
        /// </summary>
        public TimeSpan HttpClientTimeout { get; init; }

        #region IHttpClientRetryingSettings

        /// <inheritdoc/>
        public bool UseHttpClientRetrying { get; init; }

        /// <inheritdoc/>
        public int MaxRetries { get; init; }

        /// <inheritdoc/>
        public bool UseDefaultRetryTimeout { get; init; }

        /// <inheritdoc/>
        public TimeSpan DefaultRetryTimeout { get; init; }

        /// <inheritdoc/>
        public IDictionary<HttpStatusCode, TimeSpan> RetryTimeouts { get; init; } = new Dictionary<HttpStatusCode, TimeSpan>();

        #endregion #region IHttpClientRetryingSettings
    }
}