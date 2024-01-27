// ------------------------------------------------------------------------------------------------------
// <copyright file="EvmScoreSoulboundToken.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.SoulboundTokenService.Extensions;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.SoulboundTokenService
{
    /// <summary>
    /// Score soulbound token registrar.
    /// </summary>
    /// <remarks>
    /// Is EVM-compatible.
    /// </remarks>
    public sealed class EvmScoreSoulboundToken :
        IEvmScoreSoulboundTokenServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddEvmScoreSoulboundTokenService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService GetService(
            IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IEvmScoreSoulboundTokenService>();
        }
    }
}