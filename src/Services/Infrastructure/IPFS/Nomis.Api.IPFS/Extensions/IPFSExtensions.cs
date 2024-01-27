// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFSExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.IPFS.Settings;
using Nomis.IPFS.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

// ReSharper disable InconsistentNaming
namespace Nomis.Api.IPFS.Extensions
{
    /// <summary>
    /// IPFS extension methods.
    /// </summary>
    public static class IPFSExtensions
    {
        /// <summary>
        /// Add IPFS service.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithIPFSService<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IIPFSServiceRegistrar, new()
        {
            return optionsBuilder
                .With<IPFSAPISettings, TServiceRegistrar>();
        }
    }
}