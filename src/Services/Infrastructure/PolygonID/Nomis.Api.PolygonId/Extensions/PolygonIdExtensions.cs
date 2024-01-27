// ------------------------------------------------------------------------------------------------------
// <copyright file="PolygonIdExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.PolygonId.Settings;
using Nomis.PolygonId.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.PolygonId.Extensions
{
    /// <summary>
    /// PolygonID extension methods.
    /// </summary>
    public static class PolygonIdExtensions
    {
        /// <summary>
        /// Add PolygonID API.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithPolygonIdAPI<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IPolygonIdServiceRegistrar, new()
        {
            return optionsBuilder
                .With<PolygonIdAPISettings, TServiceRegistrar>();
        }
    }
}