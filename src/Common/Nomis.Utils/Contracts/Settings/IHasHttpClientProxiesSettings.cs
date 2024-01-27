// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasHttpClientProxiesSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Proxy;

namespace Nomis.Utils.Contracts.Settings
{
    /// <summary>
    /// <see cref="HttpClient"/> proxies with API keys settings.
    /// </summary>
    public interface IHasHttpClientProxiesSettings
    {
        /// <summary>
        /// Use proxies.
        /// </summary>
        public bool UseProxies { get; init; }

        /// <summary>
        /// List of proxy with API keys.
        /// </summary>
        public IList<HttpClientProxy> HttpClientProxies { get; init; }
    }
}