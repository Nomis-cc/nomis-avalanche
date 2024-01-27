// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions.Extensions;
using Nomis.DexProviderService.Interfaces;
using Nomis.Snowtrace.Interfaces;
using Nomis.Snowtrace.Settings;
using Nomis.Utils.Contracts;
using Nomis.Utils.Extensions;

namespace Nomis.Snowtrace.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Snowtrace service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSnowtraceService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.CheckServiceDependencies(typeof(SnowtraceService), typeof(IDexProviderService));
            services.AddSettings<SnowtraceSettings>(configuration);
            var settings = configuration.GetSettings<SnowtraceSettings>();
            services
                .AddSingleton<IValuePool<SnowtraceService, string>>(_ => new ValuePool<SnowtraceService, string>(settings.ApiKeys));
            services
                .AddHttpClient<SnowtraceClient>(client =>
                {
                    client.BaseAddress = new(settings.ApiBaseUrl ?? "https://api.routescan.io");
                })
                .AddRateLimitHandler();

            // .AddTraceLogHandler(_ => Task.FromResult(settings.UseHttpClientLogging));
            return services
                .AddTransient<ISnowtraceClient, SnowtraceClient>(provider =>
                {
                    var apiKeysPool = provider.GetRequiredService<IValuePool<SnowtraceService, string>>();
                    var logger = provider.GetRequiredService<ILogger<SnowtraceClient>>();
                    var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(SnowtraceClient));
                    return new SnowtraceClient(settings, apiKeysPool, client, logger);
                })
                .AddTransientInfrastructureService<IAvalancheScoringService, SnowtraceService>();
        }
    }
}