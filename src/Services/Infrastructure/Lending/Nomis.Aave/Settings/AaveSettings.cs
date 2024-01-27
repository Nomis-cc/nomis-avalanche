// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Aave.Contracts;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Aave.Settings
{
    /// <summary>
    /// Aave Protocol settings.
    /// </summary>
    internal class AaveSettings :
        ISettings
    {
        /// <summary>
        /// List of Aave data feed.
        /// </summary>
        public List<AaveDataFeed>? DataFeeds { get; init; }
    }
}