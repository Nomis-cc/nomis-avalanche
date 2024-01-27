// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.LensProtocol.Settings;
using Nomis.LensProtocol.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.LensProtocol.Extensions
{
    /// <summary>
    /// Lens Protocol extension methods.
    /// </summary>
    public static class LensProtocolExtensions
    {
        /// <summary>
        /// Add Lens Protocol protocol.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithLensProtocol<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : ILensProtocolServiceRegistrar, new()
        {
            return optionsBuilder
                .With<LensProtocolAPISettings, TServiceRegistrar>();
        }
    }
}