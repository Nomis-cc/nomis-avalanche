// ------------------------------------------------------------------------------------------------------
// <copyright file="IUseHistoricalMedianBalanceUSDSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Contracts.Settings
{
    /// <summary>
    /// Have historical median balance USD settings.
    /// </summary>
    public interface IUseHistoricalMedianBalanceUSDSettings
    {
        /// <summary>
        /// Use historical median balance USD by scoring calculation model.
        /// </summary>
        public IDictionary<ScoringCalculationModel, bool> UseHistoricalMedianBalanceUSD { get; init; }

        /// <summary>
        /// Median balance precision.
        /// </summary>
        public decimal MedianBalancePrecision { get; init; }

        /// <summary>
        /// How long ago to start calculating the median time.
        /// </summary>
        public TimeSpan? MedianBalanceStartFrom { get; init; }

        /// <summary>
        /// Take last count of median balance.
        /// </summary>
        public int? MedianBalanceLastCount { get; init; }
    }
}