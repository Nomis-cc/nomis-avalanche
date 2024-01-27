// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.CyberConnect.Settings
{
    /// <summary>
    /// CyberConnect settings.
    /// </summary>
    internal class CyberConnectSettings :
        ISettings
    {
        /// <summary>
        /// CyberConnect API base address.
        /// </summary>
        public string? ApiBaseUrl { get; init; }
    }
}