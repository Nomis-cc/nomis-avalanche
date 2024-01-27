// ------------------------------------------------------------------------------------------------------
// <copyright file="StableCoinData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Enums;

namespace Nomis.DexProviderService.Interfaces.Contracts
{
    /// <summary>
    /// Stablecoin data.
    /// </summary>
    public class StableCoinData
    {
        /// <summary>
        /// Stablecoin contracts.
        /// </summary>
        public IDictionary<Chain, string> Contracts { get; set; } = new Dictionary<Chain, string>();

        /// <summary>
        /// Stablecoin name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Stablecoin symbol.
        /// </summary>
        public string? Symbol { get; set; }
    }
}