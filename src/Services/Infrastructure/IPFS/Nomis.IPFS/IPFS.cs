// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFS.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.IPFS.Extensions;
using Nomis.IPFS.Interfaces;
using Nomis.Utils.Contracts.Services;

// ReSharper disable InconsistentNaming
namespace Nomis.IPFS
{
    /// <summary>
    /// IPFS service registrar.
    /// </summary>
    public sealed class IPFS :
        IIPFSServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddIPFSService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IIPFSService>();
        }
    }
}