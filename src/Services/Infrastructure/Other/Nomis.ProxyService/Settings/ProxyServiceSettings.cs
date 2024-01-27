// ------------------------------------------------------------------------------------------------------
// <copyright file="ProxyServiceSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------

using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Proxy;

namespace Nomis.ProxyService.Settings
{
    /// <summary>
    /// Proxy service settings.
    /// </summary>
    public class ProxyServiceSettings :
        ISettings
    {
        /// <summary>
        /// List of Web-proxy data.
        /// </summary>
        public IList<WebProxyData> WebProxies { get; init; } = new List<WebProxyData>();
    }
}