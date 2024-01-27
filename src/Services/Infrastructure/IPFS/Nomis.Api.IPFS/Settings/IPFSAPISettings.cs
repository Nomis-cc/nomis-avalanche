// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFSAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

// ReSharper disable IdentifierTypo
namespace Nomis.Api.IPFS.Settings
{
    /// <summary>
    /// IPFS API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class IPFSAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => IPFSController.IPFSTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(IPFSController);
    }
}