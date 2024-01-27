// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.ElasticMailServices.Settings;
using Nomis.MailingService.Interfaces;
using Nomis.Utils.Extensions;

namespace Nomis.ElasticMailServices.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Elastic Mail service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        internal static IServiceCollection AddElasticMailServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration.GetSettings<ElasticMailServiceSettings>();
            services.AddSettings<ElasticMailServiceSettings>(configuration);
            services.AddHttpClient<ElasticMailClient>(client =>
            {
                client.BaseAddress = new Uri(settings.BaseUrl ?? "https://api.elasticemail.com/v4/");
                client.DefaultRequestHeaders.Add("X-ElasticEmail-ApiKey", settings.ElasticMailApiKey);
            });

            return services.AddSingletonInfrastructureService<IMailingService, ElasticMailServices>();
        }
    }
}