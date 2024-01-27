// ------------------------------------------------------------------------------------------------------
// <copyright file="Proxy.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.ProxyService.Extensions;
using Nomis.ProxyService.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.ProxyService
{
    /// <summary>
    /// Proxy service registrar.
    /// </summary>
    public sealed class Proxy :
        IProxyServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddProxyService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IProxyService>();
        }
    }
}