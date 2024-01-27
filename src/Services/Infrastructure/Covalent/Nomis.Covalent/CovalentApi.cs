// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentApi.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Covalent.Extensions;
using Nomis.Covalent.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Covalent
{
    /// <summary>
    /// Covalent API service registrar.
    /// </summary>
    public sealed class CovalentApi :
        ICovalentServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddCovalentService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<ICovalentService>();
        }
    }
}