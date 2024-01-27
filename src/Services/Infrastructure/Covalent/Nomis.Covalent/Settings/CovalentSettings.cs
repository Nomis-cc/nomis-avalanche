// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Settings;

namespace Nomis.Covalent.Settings
{
    /// <summary>
    /// Covalent settings.
    /// </summary>
    internal class CovalentSettings :
        ISettings,
        IRateLimitSettings,
        IHttpClientRetryingSettings
    {
        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://www.covalenthq.com/docs/unified-api/"/>
        /// </remarks>
        public string? ApiBaseUrl { get; init; }

        /// <summary>
        /// API key.
        /// </summary>
        public string? ApiKey { get; init; }

        /// <inheritdoc/>
        public uint MaxApiCallsPerSecond { get; init; }

        /// <summary>
        /// Use caching for wallet token balances.
        /// </summary>
        public bool UseTokenBalancesCaching { get; init; }

        /// <summary>
        /// Wallet tokens balances cache duration.
        /// </summary>
        public TimeSpan TokenBalancesCacheDuration { get; init; }

        /// <summary>
        /// Use caching for wallet NFT token balances.
        /// </summary>
        public bool UseNftBalancesCaching { get; init; }

        /// <summary>
        /// Wallet NFT tokens balances cache duration.
        /// </summary>
        public TimeSpan NftTokenBalancesCacheDuration { get; init; }

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