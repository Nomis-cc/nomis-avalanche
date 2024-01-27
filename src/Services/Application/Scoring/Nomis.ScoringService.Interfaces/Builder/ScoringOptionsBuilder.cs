// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoringOptionsBuilder.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Extensions;

namespace Nomis.ScoringService.Interfaces.Builder
{
    /// <inheritdoc cref="IScoringOptionsBuilder"/>
    public sealed class ScoringOptionsBuilder :
        IScoringOptionsBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly List<IApiSettings> _settings = new();
        private readonly List<IServiceRegistrar> _registrars = new();

        /// <inheritdoc />
        public IList<IInfrastructureService> InfrastructureServices { get; } = new List<IInfrastructureService>();

        /// <summary>
        /// Initialize <see cref="ScoringOptionsBuilder"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        internal ScoringOptionsBuilder(
            IServiceCollection services,
            IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        /// <inheritdoc />
        public IEnumerable<IApiSettings> Settings => _settings;

        /// <inheritdoc />
        public IScoringOptionsBuilder RegisterServices<TSettings, TServiceRegistrar>(
            TServiceRegistrar registrar)
            where TSettings : class, IApiSettings, new()
            where TServiceRegistrar : IServiceRegistrar
        {
            var settings = _configuration.GetSettings<TSettings>();
            if (!_settings.Contains(settings))
            {
                _settings.Add(settings);
            }

            if (!_registrars.Contains(registrar))
            {
                _registrars.Add(registrar);
            }

            return this;
        }

        /// <inheritdoc />
        public ScoringOptionsBuilder Build()
        {
            _registrars.ForEach(_ => _.RegisterService(_services, _configuration));
            _registrars.ForEach(_ =>
            {
                var service = _.GetService(_services);
                if (service != null && !InfrastructureServices.Contains(service))
                {
                    InfrastructureServices.Add(service);
                }
            });

            return this;
        }
    }
}