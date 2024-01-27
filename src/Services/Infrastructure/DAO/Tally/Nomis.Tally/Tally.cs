// ------------------------------------------------------------------------------------------------------
// <copyright file="Tally.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Tally.Extensions;
using Nomis.Tally.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Tally
{
    /// <summary>
    /// Tally service registrar.
    /// </summary>
    public sealed class Tally :
        ITallyServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddTallyService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<ITallyService>();
        }
    }
}