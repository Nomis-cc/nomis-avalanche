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
using Nomis.Tally.Interfaces;

using Nomis.Tally.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.Tally.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Tally service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTallyService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<TallySettings>(configuration);
            var settings = configuration.GetSettings<TallySettings>();
            services.AddSingleton<ITallyGraphQLClient>(_ =>
            {
                var graphQlOptions = new GraphQLHttpClientOptions
                {
                    EndPoint = new(settings.ApiBaseUrl ?? "https://api.withtally.com/query")
                };
                var client = new TallyGraphQLClient(graphQlOptions, new SystemTextJsonSerializer());
                client.HttpClient.DefaultRequestHeaders.Add("Api-key", settings.ApiKey);

                return client;
            });

            return services.AddSingleton<ITallyService, TallyService>();
        }
    }
}