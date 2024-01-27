// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Snapshot.Settings;
using Nomis.ScoringService.Interfaces.Builder;
using Nomis.Snapshot.Interfaces;

namespace Nomis.Api.Snapshot.Extensions
{
    /// <summary>
    /// Snapshot extension methods.
    /// </summary>
    public static class SnapshotExtensions
    {
        /// <summary>
        /// Add Snapshot protocol.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithSnapshotProtocol<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : ISnapshotServiceRegistrar, new()
        {
            return optionsBuilder
                .With<SnapshotAPISettings, TServiceRegistrar>();
        }
    }
}