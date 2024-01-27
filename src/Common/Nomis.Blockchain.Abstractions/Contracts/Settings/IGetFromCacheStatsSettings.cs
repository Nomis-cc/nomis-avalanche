// ------------------------------------------------------------------------------------------------------
// <copyright file="IGetFromCacheStatsSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

namespace Nomis.Blockchain.Abstractions.Contracts.Settings
{
    /// <summary>
    /// Get from cache stats settings.
    /// </summary>
    public interface IGetFromCacheStatsSettings
    {
        /// <summary>
        /// Get from cache stats is enabled.
        /// </summary>
        public bool GetFromCacheStatsIsEnabled { get; init; }

        /// <summary>
        /// The time after which the stats are recalculated.
        /// </summary>
        public TimeSpan GetFromCacheStatsTimeLimit { get; init; }
    }
}