// ------------------------------------------------------------------------------------------------------
// <copyright file="MailServicesAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.MailServices.Settings
{
    /// <summary>
    /// Mail services API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class MailServicesAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => MailServicesController.MailServicesTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(MailServicesController);
    }
}