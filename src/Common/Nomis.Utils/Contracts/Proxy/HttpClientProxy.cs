// ------------------------------------------------------------------------------------------------------
// <copyright file="HttpClientProxy.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Contracts.Proxy
{
    /// <summary>
    /// Http client proxy with API keys.
    /// </summary>
    public class HttpClientProxy :
        IDisposable
    {
        /// <summary>
        /// Proxy id.
        /// </summary>
        public string ProxyId { get; init; } = null!;

        /// <summary>
        /// Proxy URI.
        /// </summary>
        /// <remarks>
        /// Example: http://localhost:8888.
        /// </remarks>
        public Uri? ProxyUri { get; set; }

        /// <summary>
        /// Explorer API keys for proxy.
        /// </summary>
        public IList<string> ApiKeys { get; init; } = new List<string>();

        /// <summary>
        /// API keys pool.
        /// </summary>
        public ValuePool<string>? ApiKeysPool { get; set; }

        /// <summary>
        /// Http client with proxy.
        /// </summary>
        /// <returns>Returns <see cref="HttpClient"/>.</returns>
        public HttpClient? Client { get; set; }

        /// <inheritdoc />
        public void Dispose()
        {
            Client?.Dispose();
            Client = null!;
            ApiKeysPool = null!;
        }
    }
}