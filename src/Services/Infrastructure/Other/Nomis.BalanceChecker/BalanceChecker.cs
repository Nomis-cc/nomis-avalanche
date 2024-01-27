// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceChecker.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.BalanceChecker.Extensions;
using Nomis.BalanceChecker.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.BalanceChecker
{
    /// <summary>
    /// Balance checker service registrar.
    /// </summary>
    public sealed class BalanceChecker :
        IBalanceCheckerServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddBalanceCheckerService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IBalanceCheckerService>();
        }
    }
}