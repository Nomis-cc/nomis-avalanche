// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralsExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Referrals.Settings;
using Nomis.ReferralService.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Referrals.Extensions
{
    /// <summary>
    /// Referrals extension methods.
    /// </summary>
    public static class ReferralsExtensions
    {
        /// <summary>
        /// Add referrals service.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithReferralsService<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IReferralServiceRegistrar, new()
        {
            return optionsBuilder
                .With<ReferralsSettings, TServiceRegistrar>();
        }
    }
}