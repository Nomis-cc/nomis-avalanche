// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Tally.Settings;
using Nomis.ScoringService.Interfaces.Builder;
using Nomis.Tally.Interfaces;

namespace Nomis.Api.Tally.Extensions
{
    /// <summary>
    /// Tally extension methods.
    /// </summary>
    public static class TallyExtensions
    {
        /// <summary>
        /// Add Tally protocol.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithTallyProtocol<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : ITallyServiceRegistrar, new()
        {
            return optionsBuilder
                .With<TallyAPISettings, TServiceRegistrar>();
        }
    }
}