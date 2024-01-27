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
using Nomis.DefiLlama.Interfaces;
using Nomis.DefiLlama.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.DefiLlama.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add DefiLlama service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddDefiLlamaService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<DefiLlamaSettings>(configuration);
            services.AddSettings<LlamafolioSettings>(configuration);
            var settings = configuration.GetSettings<DefiLlamaSettings>();
            var llamafolioSettings = configuration.GetSettings<LlamafolioSettings>();
            services
                .AddHttpClient<DefiLlamaService>(client =>
                {
                    client.BaseAddress = new(settings.ApiBaseUrl ?? "https://coins.llama.fi/");
                })
                .AddRateLimitHandler(settings);

            services
                .AddHttpClient(llamafolioSettings.LlamafolioApi, client =>
                {
                    client.BaseAddress = new(llamafolioSettings.ApiBaseUrl ?? "https://api.llamafolio.com/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Referer", "https://llamafolio.com/");
                    client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.33.0");
                })
                .AddRateLimitHandler(llamafolioSettings);

            return services
                .AddSingleton<IDefiLlamaService, DefiLlamaService>(provider =>
                {
                    var logger = provider.GetRequiredService<ILogger<DefiLlamaService>>();
                    var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(DefiLlamaService));
                    var llamafolioClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(llamafolioSettings.LlamafolioApi);
                    var cacheProviderService = provider.GetRequiredService<ICacheProviderService>();
                    return new DefiLlamaService(settings, llamafolioSettings, client, llamafolioClient, cacheProviderService, logger);
                });
        }
    }
}