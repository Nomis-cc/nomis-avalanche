// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Aave.Interfaces;
using Nomis.Api.Aave.Settings;
using Nomis.Api.Common.Extensions;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Aave.Extensions
{
    /// <summary>
    /// Aave extension methods.
    /// </summary>
    public static class AaveExtensions
    {
        /// <summary>
        /// Add Aave lending protocol.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithAaveLendingProtocol<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IAaveServiceRegistrar, new()
        {
            return optionsBuilder
                .With<AaveAPISettings, TServiceRegistrar>();
        }
    }
}