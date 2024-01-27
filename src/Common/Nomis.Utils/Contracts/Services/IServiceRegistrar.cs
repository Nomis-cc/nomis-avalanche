// ------------------------------------------------------------------------------------------------------
// <copyright file="IServiceRegistrar.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nomis.Utils.Contracts.Services
{
    /// <summary>
    /// Service registrar.
    /// </summary>
    public interface IServiceRegistrar
    {
        /// <summary>
        /// Register the service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration);

        /// <summary>
        /// Get the service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns the service.</returns>
        public IInfrastructureService? GetService(
            IServiceCollection services) => default;
    }
}