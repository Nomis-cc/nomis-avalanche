// ------------------------------------------------------------------------------------------------------
// <copyright file="SBTAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

// ReSharper disable IdentifierTypo
namespace Nomis.Api.SoulboundToken.Settings
{
    /// <summary>
    /// SBT API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class SBTAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => SBTController.SBTTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(SBTController);
    }
}