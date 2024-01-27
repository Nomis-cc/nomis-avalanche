// ------------------------------------------------------------------------------------------------------
// <copyright file="PolygonIdAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.PolygonId.Settings
{
    /// <summary>
    /// PolygonID API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class PolygonIdAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => PolygonIdController.PolygonIdTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(PolygonIdController);
    }
}