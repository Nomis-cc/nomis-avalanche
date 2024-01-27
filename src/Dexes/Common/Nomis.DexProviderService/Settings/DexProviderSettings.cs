// ------------------------------------------------------------------------------------------------------
// <copyright file="DexProviderSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.DexProviderService.Interfaces;

namespace Nomis.DexProviderService.Settings
{
    /// <summary>
    /// <see cref="IDexProviderService"/> settings.
    /// </summary>
    public class DexProviderSettings :
        IDexSettings
    {
        /// <summary>
        /// Use background service for updating the cache.
        /// </summary>
        public bool UseBackgroundCacheUpdater { get; init; }

        /// <summary>
        /// Time to the next update starting.
        /// </summary>
        public TimeSpan Delay { get; init; }

        /// <inheritdoc />
        public IList<DexDescriptor>? DexDescriptors { get; init; }
    }
}