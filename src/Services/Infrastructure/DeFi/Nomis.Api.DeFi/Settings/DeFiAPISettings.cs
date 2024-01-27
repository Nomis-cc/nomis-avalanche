// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.DeFi.Settings
{
    /// <summary>
    /// De.Fi API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class DeFiAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => DeFiController.DeFiTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(DeFiController);
    }
}