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
using Nomis.CacheProviderService.Interfaces;
using Nomis.Covalent.Interfaces;
using Nomis.Covalent.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.Covalent.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Covalent API service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddCovalentService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<CovalentSettings>(configuration);
            var settings = configuration.GetSettings<CovalentSettings>();
            services
                .AddHttpClient<CovalentService>(client =>
                {
                    client.BaseAddress = new(settings.ApiBaseUrl ?? "https://api.covalenthq.com/");
                    client.Timeout = settings.HttpClientTimeout;
                })
                .AddRateLimitHandler(settings);

            return services
                .AddSingleton<ICovalentService, CovalentService>(provider =>
                {
                    var logger = provider.GetRequiredService<ILogger<CovalentService>>();
                    var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(CovalentService));
                    var cacheProviderService = provider.GetRequiredService<ICacheProviderService>();
                    return new CovalentService(settings, client, cacheProviderService, logger);
                });
        }
    }
}