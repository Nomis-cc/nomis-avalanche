// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Aave.Interfaces;
using Nomis.Aave.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.Aave.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Aave service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAaveService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<AaveSettings>(configuration);
            return services
                .AddSingletonInfrastructureService<IAaveService, AaveService>();
        }
    }
}