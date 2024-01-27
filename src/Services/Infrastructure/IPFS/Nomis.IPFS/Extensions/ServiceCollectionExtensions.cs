// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.IPFS.Interfaces;
using Nomis.IPFS.Settings;
using Nomis.Utils.Extensions;

// ReSharper disable InconsistentNaming
namespace Nomis.IPFS.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add IPFS service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddIPFSService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<IPFSSettings>(configuration);
            return services
                .AddSingletonInfrastructureService<IIPFSService, IPFSService>();
        }
    }
}