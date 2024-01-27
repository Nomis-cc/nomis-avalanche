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
using Nomis.DeBank.Interfaces;
using Nomis.DeBank.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.DeBank.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add DeBank API service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddDeBankService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<DeBankSettings>(configuration);
            var settings = configuration.GetSettings<DeBankSettings>();
            services
                .AddHttpClient<DeBankService>(client =>
                {
                    client.BaseAddress = new(settings.ApiBaseUrl ?? "https://pro-openapi.debank.com/");
                    client.DefaultRequestHeaders.Add("AccessKey", settings.ApiKey);
                    client.Timeout = settings.HttpClientTimeout;
                })
                .AddRateLimitHandler(settings);

            return services
                .AddSingleton<IDeBankService, DeBankService>(provider =>
                {
                    var logger = provider.GetRequiredService<ILogger<DeBankService>>();
                    var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(DeBankService));
                    var cacheProviderService = provider.GetRequiredService<ICacheProviderService>();
                    return new DeBankService(settings, client, cacheProviderService, logger);
                });
        }
    }
}