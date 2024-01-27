// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nomis.CyberConnect.Interfaces;

using Nomis.CyberConnect.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.CyberConnect.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add CyberConnect service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCyberConnectService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration.GetSettings<CyberConnectSettings>();
            services.AddSingleton<ICyberConnectGraphQLClient>(_ =>
            {
                var graphQlOptions = new GraphQLHttpClientOptions
                {
                    EndPoint = new(settings.ApiBaseUrl ?? "https://api.cyberconnect.dev/")
                };
                return new CyberConnectGraphQLClient(graphQlOptions, new SystemTextJsonSerializer());
            });

            return services.AddSingleton<ICyberConnectService>(ctx =>
            {
                var graphQlClient = ctx.GetRequiredService<ICyberConnectGraphQLClient>();
                var logger = ctx.GetRequiredService<ILogger<CyberConnectService>>();
                return new CyberConnectService(graphQlClient, logger);
            });
        }
    }
}