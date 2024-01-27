// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.CyberConnect.Settings;
using Nomis.CyberConnect.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.CyberConnect.Extensions
{
    /// <summary>
    /// CyberConnect extension methods.
    /// </summary>
    public static class CyberConnectExtensions
    {
        /// <summary>
        /// Add CyberConnect protocol.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithCyberConnectProtocol<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : ICyberConnectServiceRegistrar, new()
        {
            return optionsBuilder
                .With<CyberConnectAPISettings, TServiceRegistrar>();
        }
    }
}