// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.SoulboundTokenService.Settings;
using Nomis.Utils.Extensions;

// ReSharper disable InconsistentNaming
namespace Nomis.SoulboundTokenService.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add score soulbound token service.
        /// </summary>
        /// <remarks>
        /// Is EVM-compatible.
        /// </remarks>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEvmScoreSoulboundTokenService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<ScoreSoulboundTokenSettings>(configuration);
            return services
                .AddTransientInfrastructureService<IEvmScoreSoulboundTokenService, EvmScoreSoulboundTokenService>();
        }

        /// <summary>
        /// Add score soulbound token service.
        /// </summary>
        /// <remarks>
        /// Is not EVM-compatible.
        /// </remarks>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        internal static IServiceCollection AddNonEvmScoreSoulboundTokenService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<ScoreSoulboundTokenSettings>(configuration);
            return services
                .AddTransientInfrastructureService<INonEvmScoreSoulboundTokenService, NonEvmScoreSoulboundTokenService>();
        }
    }
}