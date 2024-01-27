// ------------------------------------------------------------------------------------------------------
// <copyright file="DefiLlamaSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Settings;

namespace Nomis.DefiLlama.Settings
{
    /// <summary>
    /// DefiLlama settings.
    /// </summary>
    internal class DefiLlamaSettings :
        ISettings,
        IRateLimitSettings,
        IHttpClientRetryingSettings
    {
        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://defillama.com/docs/api"/>
        /// </remarks>
        public string? ApiBaseUrl { get; init; }

        /// <inheritdoc/>
        public uint MaxApiCallsPerSecond { get; init; }

        /// <summary>
        /// Use caching for tokens prices.
        /// </summary>
        public bool TokenPriceUseCaching { get; init; }

        /// <summary>
        /// Tokens prices cache duration.
        /// </summary>
        public TimeSpan TokenPriceCacheDuration { get; init; }

        /// <summary>
        /// Request tokens batch size.
        /// </summary>
        public int RequestTokensBatchSize { get; init; }

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