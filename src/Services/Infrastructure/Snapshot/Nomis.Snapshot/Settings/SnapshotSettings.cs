// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Snapshot.Settings
{
    /// <summary>
    /// Snapshot settings.
    /// </summary>
    internal class SnapshotSettings :
        ISettings
    {
        /// <summary>
        /// Snapshot API base address.
        /// </summary>
        public string? ApiBaseUrl { get; init; }
    }
}