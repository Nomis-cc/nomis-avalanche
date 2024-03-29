﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.DeBankApi.Settings;
using Nomis.DeBank.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.DeBankApi.Extensions
{
    /// <summary>
    /// DeBank extension methods.
    /// </summary>
    public static class DeBankExtensions
    {
        /// <summary>
        /// Add DeBank API.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithDeBankAPI<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IDeBankServiceRegistrar, new()
        {
            return optionsBuilder
                .With<DeBankApiAPISettings, TServiceRegistrar>();
        }
    }
}