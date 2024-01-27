// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.DataAccess.PostgreSql.Extensions;
using Nomis.DataAccess.PostgreSql.Referral.Persistence;
using Nomis.DataAccess.Referral.Interfaces.Contexts;
using Nomis.Domain;

namespace Nomis.DataAccess.PostgreSql.Referral.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add a data store related to referrals.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddReferralPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddDatabaseContext<ReferralDbContext>(configuration)
                .AddTransient<IReferralDbContext>(provider => provider.GetRequiredService<ReferralDbContext>())
                .AddTransient<IReferralReadDbContext, ReferralReadDbContext>();
            services.AddTransient<IDatabaseSeeder, ReferralDbSeeder>();

            return services;
        }
    }
}