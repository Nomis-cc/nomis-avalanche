// ------------------------------------------------------------------------------------------------------
// <copyright file="IFilterCounterpartiesByCalculationModelSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Contracts.Settings
{
    /// <summary>
    /// Filter counterparties by calculation model settings.
    /// </summary>
    public interface IFilterCounterpartiesByCalculationModelSettings
    {
        /// <summary>
        /// Filter data.
        /// </summary>
        public IDictionary<ScoringCalculationModel, List<CounterpartyData>> CounterpartiesFilterData { get; init; }
    }
}