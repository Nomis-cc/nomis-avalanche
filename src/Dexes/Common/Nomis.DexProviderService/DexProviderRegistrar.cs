// ------------------------------------------------------------------------------------------------------
// <copyright file="DexProviderRegistrar.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.DexProviderService.Extensions;
using Nomis.DexProviderService.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.DexProviderService
{
    /// <summary>
    /// DEX provider service registrar.
    /// </summary>
    public sealed class DexProviderRegistrar :
        IDexProviderServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddDexProviderService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IDexProviderService>();
        }
    }
}