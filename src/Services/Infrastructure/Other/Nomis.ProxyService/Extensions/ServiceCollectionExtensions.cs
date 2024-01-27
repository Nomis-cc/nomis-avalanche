// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.ProxyService.Interfaces;
using Nomis.ProxyService.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.ProxyService.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Proxy service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddProxyService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<ProxyServiceSettings>(configuration);
            return services.AddSingletonInfrastructureService<IProxyService, ProxyService>();
        }
    }
}