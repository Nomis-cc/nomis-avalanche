// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocol.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.LensProtocol.Extensions;
using Nomis.LensProtocol.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.LensProtocol
{
    /// <summary>
    /// Lens Protocol service registrar.
    /// </summary>
    public sealed class LensProtocol :
        ILensProtocolServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddLensProtocolService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<ILensProtocolService>();
        }
    }
}