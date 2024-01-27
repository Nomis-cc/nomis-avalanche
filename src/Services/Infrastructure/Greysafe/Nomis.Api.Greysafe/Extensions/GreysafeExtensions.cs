// ------------------------------------------------------------------------------------------------------
// <copyright file="GreysafeExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Greysafe.Settings;
using Nomis.Greysafe.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Greysafe.Extensions
{
    /// <summary>
    /// Greysafe extension methods.
    /// </summary>
    public static class GreysafeExtensions
    {
        /// <summary>
        /// Add Greysafe scam reporting service.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithGreysafeService<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IGreysafeServiceRegistrar, new()
        {
            return optionsBuilder
                .With<GreysafeAPISettings, TServiceRegistrar>();
        }
    }
}