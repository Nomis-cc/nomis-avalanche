// ------------------------------------------------------------------------------------------------------
// <copyright file="DexAggregatorExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.DexAggregator.Settings;
using Nomis.DexProviderService.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.DexAggregator.Extensions
{
    /// <summary>
    /// DexAggregator extension methods.
    /// </summary>
    public static class DexAggregatorExtensions
    {
        /// <summary>
        /// Add DEX aggregator.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithDexAggregator<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IDexProviderServiceRegistrar, new()
        {
            return optionsBuilder
                .With<DexAggregatorAPISettings, TServiceRegistrar>();
        }
    }
}