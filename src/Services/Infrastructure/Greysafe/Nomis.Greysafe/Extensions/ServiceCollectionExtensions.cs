// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Greysafe.Interfaces;
using Nomis.Greysafe.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.Greysafe.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Greysafe scam reporting service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddGreysafeService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<GreysafeSettings>(configuration);
            return services
                .AddTransientInfrastructureService<IGreysafeService, GreysafeService>();
        }
    }
}