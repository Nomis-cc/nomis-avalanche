// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.DeFi.Settings;
using Nomis.DeFi.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

// ReSharper disable InconsistentNaming
namespace Nomis.Api.DeFi.Extensions
{
    /// <summary>
    /// De.Fi extension methods.
    /// </summary>
    public static class DeFiExtensions
    {
        /// <summary>
        /// Add De.Fi API.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithDeFiAPI<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IDeFiServiceRegistrar, new()
        {
            return optionsBuilder
                .With<DeFiAPISettings, TServiceRegistrar>();
        }
    }
}