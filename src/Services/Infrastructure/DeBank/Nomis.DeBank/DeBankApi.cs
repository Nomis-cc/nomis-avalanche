// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankApi.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.DeBank.Extensions;
using Nomis.DeBank.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.DeBank
{
    /// <summary>
    /// DeBank API service registrar.
    /// </summary>
    public sealed class DeBankApi :
        IDeBankServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddDeBankService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IDeBankService>();
        }
    }
}