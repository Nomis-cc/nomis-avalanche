// ------------------------------------------------------------------------------------------------------
// <copyright file="IProxyService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.ProxyService.Interfaces;

/// <summary>
/// Proxy service.
/// </summary>
public interface IProxyService :
    ISingletonService,
    IInfrastructureService
{
    /// <summary>
    /// Get Web-proxy by id.
    /// </summary>
    /// <param name="id">Proxy id.</param>
    /// <returns>Returns Web-proxy.</returns>
    public Result<WebProxy> GetWebProxy(
        string? id);
}