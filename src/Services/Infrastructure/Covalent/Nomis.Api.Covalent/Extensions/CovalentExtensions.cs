// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Covalent.Settings;
using Nomis.Covalent.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Covalent.Extensions
{
    /// <summary>
    /// Covalent extension methods.
    /// </summary>
    public static class CovalentExtensions
    {
        /// <summary>
        /// Add Covalent API.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithCovalentAPI<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : ICovalentServiceRegistrar, new()
        {
            return optionsBuilder
                .With<CovalentAPISettings, TServiceRegistrar>();
        }
    }
}