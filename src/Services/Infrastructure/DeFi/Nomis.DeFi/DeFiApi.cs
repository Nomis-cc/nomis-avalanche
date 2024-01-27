// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiApi.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.DeFi.Extensions;
using Nomis.DeFi.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.DeFi
{
    /// <summary>
    /// De.Fi API service registrar.
    /// </summary>
    public sealed class DeFiApi :
        IDeFiServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddDeFiService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IDeFiService>();
        }
    }
}