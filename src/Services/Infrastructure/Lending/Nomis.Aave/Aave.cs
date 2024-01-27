﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="Aave.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Aave.Extensions;
using Nomis.Aave.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Aave
{
    /// <summary>
    /// Aave service registrar.
    /// </summary>
    public sealed class Aave :
        IAaveServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddAaveService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IAaveService>();
        }
    }
}