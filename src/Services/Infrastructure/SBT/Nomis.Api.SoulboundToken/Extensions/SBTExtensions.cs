// ------------------------------------------------------------------------------------------------------
// <copyright file="SBTExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.SoulboundToken.Settings;
using Nomis.ScoringService.Interfaces.Builder;
using Nomis.SoulboundTokenService.Interfaces;

namespace Nomis.Api.SoulboundToken.Extensions
{
    /// <summary>
    /// SBT extension methods.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SBTExtensions
    {
        /// <summary>
        /// Add EVM soulbound token service.
        /// </summary>
        /// <typeparam name="TEvmSoulboundTokenServiceRegistrar">The soulbound token service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithEvmSoulboundTokenService<TEvmSoulboundTokenServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TEvmSoulboundTokenServiceRegistrar : IEvmScoreSoulboundTokenServiceRegistrar, new()
        {
            return optionsBuilder
                .With<SBTAPISettings, TEvmSoulboundTokenServiceRegistrar>();
        }

        /// <summary>
        /// Add non EVM soulbound token service.
        /// </summary>
        /// <typeparam name="TNonEvmSoulboundTokenServiceRegistrar">The soulbound token service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithNonEvmSoulboundTokenService<TNonEvmSoulboundTokenServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TNonEvmSoulboundTokenServiceRegistrar : INonEvmScoreSoulboundTokenServiceRegistrar, new()
        {
            return optionsBuilder
                .With<SBTAPISettings, TNonEvmSoulboundTokenServiceRegistrar>();
        }
    }
}