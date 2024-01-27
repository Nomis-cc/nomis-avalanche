// ------------------------------------------------------------------------------------------------------
// <copyright file="MailServicesExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.MailServices.Settings;
using Nomis.MailingService.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.MailServices.Extensions
{
    /// <summary>
    /// Mail services extension methods.
    /// </summary>
    public static class MailServicesExtensions
    {
        /// <summary>
        /// Add Mail service.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithMailService<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IMailServiceRegistrar, new()
        {
            return optionsBuilder.With<MailServicesAPISettings, TServiceRegistrar>();
        }
    }
}