// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Avalanche.Settings
{
    /// <summary>
    /// Avalanche API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class AvalancheAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => AvalancheController.AvalancheTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(AvalancheController);
    }
}