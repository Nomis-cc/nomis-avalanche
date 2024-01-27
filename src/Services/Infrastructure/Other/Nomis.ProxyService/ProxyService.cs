// ------------------------------------------------------------------------------------------------------
// <copyright file="ProxyService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.ProxyService.Interfaces;
using Nomis.ProxyService.Settings;
using Nomis.Utils.Wrapper;

namespace Nomis.ProxyService
{
    /// <inheritdoc />
    public class ProxyService :
        IProxyService
    {
        private readonly ILogger<ProxyService> _logger;
        private readonly ProxyServiceSettings _settings;

        /// <summary>
        /// Initialize <see cref="ProxyService"/>.
        /// </summary>
        /// <param name="settings"><see cref="ProxyServiceSettings"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ProxyService(
            IOptions<ProxyServiceSettings> settings,
            ILogger<ProxyService> logger)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        /// <inheritdoc />
        public Result<WebProxy> GetWebProxy(
            string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Result<WebProxy>.Fail("Passed Web-proxy id is empty.");
            }

            var proxyData = _settings.WebProxies.FirstOrDefault(x => id.Equals(x.Id.ToString(), StringComparison.OrdinalIgnoreCase));
            if (proxyData == null)
            {
                _logger.LogWarning("Web-proxy not found: {id}", id);
                return Result<WebProxy>.Fail("Web-proxy not found.");
            }

            if (!proxyData.IsEnabled)
            {
                _logger.LogWarning("Web-proxy is disabled: {id}", id);
                return Result<WebProxy>.Fail("eb-proxy is disabled.");
            }

            var proxy = new WebProxy(proxyData.Uri, false);

            if (proxyData.UseCredentials)
            {
                proxy.Credentials = new NetworkCredential(proxyData.UserName, proxyData.Password);
            }

            return Result<WebProxy>.Success(proxy, "Web-proxy found by id.");
        }
    }
}