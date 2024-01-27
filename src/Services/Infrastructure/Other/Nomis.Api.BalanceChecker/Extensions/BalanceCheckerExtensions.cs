// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceCheckerExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.BalanceChecker.Settings;
using Nomis.Api.Common.Extensions;
using Nomis.BalanceChecker.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.BalanceChecker.Extensions
{
    /// <summary>
    /// BalanceChecker extension methods.
    /// </summary>
    public static class BalanceCheckerExtensions
    {
        /// <summary>
        /// Add Balance checker service.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithBalanceCheckerService<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IBalanceCheckerServiceRegistrar, new()
        {
            return optionsBuilder
                .With<BalanceCheckerAPISettings, TServiceRegistrar>();
        }
    }
}