// ------------------------------------------------------------------------------------------------------
// <copyright file="StableCoinSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.DexProviderService.Interfaces.Contracts;
using Nomis.Utils.Contracts.Common;

namespace Nomis.DexProviderService.Settings
{
    /// <summary>
    /// Stablecoin settings.
    /// </summary>
    public class StableCoinSettings :
        ISettings
    {
        /// <summary>
        /// List of stablecoins data.
        /// </summary>
        public IList<StableCoinData> Stablecoins { get; init; } = new List<StableCoinData>();
    }
}