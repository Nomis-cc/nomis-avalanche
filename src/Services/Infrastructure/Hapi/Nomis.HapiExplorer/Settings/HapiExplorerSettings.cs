// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiExplorerSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.HapiExplorer.Settings
{
    /// <summary>
    /// HAPI explorer settings.
    /// </summary>
    internal class HapiExplorerSettings :
        ISettings
    {
        /// <summary>
        /// HAPI explorer API URL.
        /// </summary>
        public string? ApiBaseUrl { get; init; }

        /// <summary>
        /// Blockchain provider URL.
        /// </summary>
        public string? BlockchainProviderUrl { get; init; }
    }
}