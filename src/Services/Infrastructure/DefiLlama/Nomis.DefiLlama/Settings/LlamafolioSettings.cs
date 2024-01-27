// ------------------------------------------------------------------------------------------------------
// <copyright file="LlamafolioSettings.cs" company="Nomis">
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
    /// Llamafolio settings.
    /// </summary>
    internal class LlamafolioSettings :
        ISettings,
        IRateLimitSettings,
        IHttpClientRetryingSettings
    {
        /// <summary>
        /// Llamafolio API name.
        /// </summary>
        public string LlamafolioApi => nameof(LlamafolioApi);

        /// <summary>
        /// API base URL.
        /// </summary>
        public string? ApiBaseUrl { get; init; }

        /// <inheritdoc/>
        public uint MaxApiCallsPerSecond { get; init; }

        /// <summary>
        /// Use caching for tokens balances.
        /// </summary>
        public bool UseTokenBalancesCaching { get; init; }

        /// <summary>
        /// Tokens balances cache duration.
        /// </summary>
        public TimeSpan TokenBalancesCacheDuration { get; init; }

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