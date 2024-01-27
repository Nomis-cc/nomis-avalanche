// ------------------------------------------------------------------------------------------------------
// <copyright file="Snowtrace.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Snowtrace.Extensions;
using Nomis.Snowtrace.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Snowtrace
{
    /// <summary>
    /// Snowtrace service registrar.
    /// </summary>
    public sealed class Snowtrace :
        IAvalancheServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddSnowtraceService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IAvalancheScoringService>();
        }
    }
}