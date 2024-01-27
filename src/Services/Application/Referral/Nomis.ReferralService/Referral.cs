// ------------------------------------------------------------------------------------------------------
// <copyright file="Referral.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.ReferralService.Extensions;
using Nomis.ReferralService.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.ReferralService
{
    /// <summary>
    /// Referral service registrar.
    /// </summary>
    public sealed class Referral :
        IReferralServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddReferralService(configuration);
        }

        /// <inheritdoc/>
        public IInfrastructureService? GetService(
            IServiceCollection services)
        {
            return null;
        }
    }
}