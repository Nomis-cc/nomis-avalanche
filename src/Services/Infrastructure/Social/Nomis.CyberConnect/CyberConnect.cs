// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnect.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.CyberConnect.Extensions;
using Nomis.CyberConnect.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.CyberConnect
{
    /// <summary>
    /// CyberConnect service registrar.
    /// </summary>
    public sealed class CyberConnect :
        ICyberConnectServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddCyberConnectService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<ICyberConnectService>();
        }
    }
}