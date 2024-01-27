// ------------------------------------------------------------------------------------------------------
// <copyright file="PolygonIdApi.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.PolygonId.Extensions;
using Nomis.PolygonId.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.PolygonId
{
    /// <summary>
    /// PolygonID API service registrar.
    /// </summary>
    public sealed class PolygonIdApi :
        IPolygonIdServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddPolygonIdService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IPolygonIdService>();
        }
    }
}