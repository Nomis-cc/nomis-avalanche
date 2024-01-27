// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nomis.BalanceChecker.Interfaces;
using Nomis.BalanceChecker.Settings;
using Nomis.Covalent.Interfaces;
using Nomis.DeFi.Interfaces;
using Nomis.DefiLlama.Interfaces;
using Nomis.Utils.Extensions;

namespace Nomis.BalanceChecker.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Balance checker service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddBalanceCheckerService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<BalanceCheckerSettings>(configuration);
            var settings = configuration.GetSettings<BalanceCheckerSettings>();
            return services
                .AddSingleton<IBalanceCheckerService, BalanceCheckerService>(provider =>
                {
                    var logger = provider.GetRequiredService<ILogger<BalanceCheckerService>>();
                    var covalentService = provider.GetRequiredService<ICovalentService>();
                    var defillamaService = provider.GetRequiredService<IDefiLlamaService>();
                    var diFiService = provider.GetRequiredService<IDeFiService>();
                    return new BalanceCheckerService(settings, covalentService, defillamaService, diFiService, logger);
                });
        }
    }
}