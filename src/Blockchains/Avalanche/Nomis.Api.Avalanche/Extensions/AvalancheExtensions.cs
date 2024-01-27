// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Avalanche.Settings;
using Nomis.Api.Common.Extensions;
using Nomis.ScoringService.Interfaces.Builder;
using Nomis.Snowtrace.Interfaces;

namespace Nomis.Api.Avalanche.Extensions
{
    /// <summary>
    /// Avalanche extension methods.
    /// </summary>
    public static class AvalancheExtensions
    {
        /// <summary>
        /// Add Avalanche blockchain.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithAvalancheBlockchain<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IAvalancheServiceRegistrar, new()
        {
            return optionsBuilder
                .With<AvalancheAPISettings, TServiceRegistrar>();
        }
    }
}