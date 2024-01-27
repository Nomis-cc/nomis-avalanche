// ------------------------------------------------------------------------------------------------------
// <copyright file="IScoringOptionsBuilder.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Services;

namespace Nomis.ScoringService.Interfaces.Builder
{
    /// <summary>
    /// Scoring options builder.
    /// </summary>
    public interface IScoringOptionsBuilder
    {
        /// <summary>
        /// Collection of API settings.
        /// </summary>
        public IEnumerable<IApiSettings> Settings { get; }

        /// <summary>
        /// Registered integration services.
        /// </summary>
        public IList<IInfrastructureService> InfrastructureServices { get; }

        /// <summary>
        /// Create the builder instance.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        public static IScoringOptionsBuilder Create(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return new ScoringOptionsBuilder(services, configuration);
        }

        /// <summary>
        /// Register the services by service registrar.
        /// </summary>
        /// <typeparam name="TSettings">The API settings type.</typeparam>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="registrar"><see cref="IServiceRegistrar"/>.</param>
        // ReSharper disable once UnusedTypeParameter
        public IScoringOptionsBuilder RegisterServices<TSettings, TServiceRegistrar>(
            TServiceRegistrar registrar)
            where TSettings : class, IApiSettings, new()
            where TServiceRegistrar : IServiceRegistrar;

        /// <summary>
        /// Build scoring options.
        /// </summary>
        /// <returns>Returns <see cref="ScoringOptionsBuilder"/>.</returns>
        public ScoringOptionsBuilder Build();
    }
}