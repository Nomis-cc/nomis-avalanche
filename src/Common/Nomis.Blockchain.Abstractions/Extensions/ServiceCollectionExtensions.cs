// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Blockchain.Abstractions.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Check service dependencies registration.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="mainServiceType">Main service type to check.</param>
        /// <param name="excludedTypes">Excluded types for checking.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection CheckServiceDependencies(
            this IServiceCollection services,
            Type mainServiceType,
            params Type[] excludedTypes)
        {
            var serviceProvider = services.BuildServiceProvider();
            var constructorParametersTypes = mainServiceType
                .GetConstructors()
                .SelectMany(c => c.GetParameters()
                    .Where(p => p.ParameterType is { IsInterface: true, IsGenericType: false }))
                .Where(p => p.ParameterType.IsAssignableTo(typeof(IApplicationService)) || p.ParameterType.IsAssignableTo(typeof(IInfrastructureService)))
                .Select(p => p.ParameterType)
                .Except(excludedTypes);

            foreach (var constructorParameterType in constructorParametersTypes)
            {
                serviceProvider.GetRequiredService(constructorParameterType);
            }

            return services;
        }
    }
}