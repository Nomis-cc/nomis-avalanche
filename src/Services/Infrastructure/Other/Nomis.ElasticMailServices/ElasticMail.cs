// ------------------------------------------------------------------------------------------------------
// <copyright file="ElasticMail.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.ElasticMailServices.Extensions;
using Nomis.MailingService.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.ElasticMailServices
{
    /// <summary>
    /// Elastic Mail mailer service registrar.
    /// </summary>
    public sealed class ElasticMail :
        IMailServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddElasticMailServices(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IMailingService>();
        }
    }
}