// ------------------------------------------------------------------------------------------------------
// <copyright file="DefiLlamaExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.DefiLlama.Settings;
using Nomis.DefiLlama.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.DefiLlama.Extensions
{
    /// <summary>
    /// DefiLlama extension methods.
    /// </summary>
    public static class DefiLlamaExtensions
    {
        /// <summary>
        /// Add DefiLlama API.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithDefiLlamaAPI<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IDefiLlamaServiceRegistrar, new()
        {
            return optionsBuilder
                .With<DefiLlamaAPISettings, TServiceRegistrar>();
        }
    }
}