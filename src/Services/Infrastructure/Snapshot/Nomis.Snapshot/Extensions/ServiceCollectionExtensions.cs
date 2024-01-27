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
using Nomis.Snapshot.Interfaces;

using Nomis.Snapshot.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.Snapshot.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Snapshot service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSnapshotService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration.GetSettings<SnapshotSettings>();
            services.AddSingleton<ISnapshotGraphQLClient>(_ =>
            {
                var graphQlOptions = new GraphQLHttpClientOptions
                {
                    EndPoint = new(settings.ApiBaseUrl ?? "https://hub.snapshot.org/graphql")
                };
                return new SnapshotGraphQLClient(graphQlOptions, new SystemTextJsonSerializer());
            });

            return services.AddSingleton<ISnapshotService>(ctx =>
            {
                var graphQlClient = ctx.GetRequiredService<ISnapshotGraphQLClient>();
                return new SnapshotService(graphQlClient);
            });
        }
    }
}