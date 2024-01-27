// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.CyberConnect.Settings
{
    /// <summary>
    /// CyberConnect API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class CyberConnectAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => CyberConnectController.CyberConnectTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(CyberConnectController);
    }
}