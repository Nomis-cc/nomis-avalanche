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
using Nomis.LensProtocol.Interfaces;

using Nomis.LensProtocol.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.LensProtocol.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Lens Protocol service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        internal static IServiceCollection AddLensProtocolService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration.GetSettings<LensProtocolSettings>();
            services.AddSingleton<ILensProtocolGraphQLClient>(_ =>
            {
                var graphQlOptions = new GraphQLHttpClientOptions
                {
                    EndPoint = new(settings.ApiBaseUrl ?? "https://api.lens.dev")
                };
                return new LensProtocolGraphQLClient(graphQlOptions, new SystemTextJsonSerializer());
            });

            return services.AddSingleton<ILensProtocolService>(ctx =>
            {
                var graphQlClient = ctx.GetRequiredService<ILensProtocolGraphQLClient>();
                var logger = ctx.GetRequiredService<ILogger<LensProtocolService>>();
                return new LensProtocolService(graphQlClient, logger);
            });
        }
    }
}