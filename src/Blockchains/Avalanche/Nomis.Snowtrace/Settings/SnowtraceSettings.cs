// ------------------------------------------------------------------------------------------------------
// <copyright file="SnowtraceSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Utils.Enums;

namespace Nomis.Snowtrace.Settings
{
    /// <summary>
    /// Snowtrace settings.
    /// </summary>
    internal class SnowtraceSettings :
        IBlockchainSettings,
        IRateLimitSettings,
        IGetFromCacheStatsSettings,
        IHttpClientLoggingSettings,
        IUseHistoricalMedianBalanceUSDSettings,
        IFilterCounterpartiesByCalculationModelSettings
    {
        /// <inheritdoc />
        public int? ItemsFetchLimitPerRequest { get; init; }

        /// <inheritdoc />
        public int? TransactionsLimit { get; init; }

        /// <summary>
        /// API keys for Snowtrace.
        /// </summary>
        public IList<string> ApiKeys { get; init; } = new List<string>();

        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://docs.snowtrace.io/getting-started/endpoint-urls"/>
        /// </remarks>
        public string? ApiBaseUrl { get; init; }

        /// <summary>
        /// Appended path for API base URL.
        /// </summary>
        public string? AppendedPath { get; init; }

        /// <inheritdoc/>
        public uint MaxApiCallsPerSecond { get; init; }

        /// <inheritdoc />
        public IDictionary<BlockchainKind, BlockchainDescriptor> BlockchainDescriptors { get; init; } = new Dictionary<BlockchainKind, BlockchainDescriptor>();

        /// <inheritdoc/>
        public bool GetFromCacheStatsIsEnabled { get; init; }

        /// <inheritdoc/>
        public TimeSpan GetFromCacheStatsTimeLimit { get; init; }

        /// <summary>
        /// The time after which the stats are recalculated for Galxe.
        /// </summary>
        public TimeSpan GetFromCacheStatsGalxeTimeLimit { get; init; }

        /// <inheritdoc/>
        public bool UseHttpClientLogging { get; init; }

        /// <inheritdoc/>
        public IDictionary<ScoringCalculationModel, bool> UseHistoricalMedianBalanceUSD { get; init; } = new Dictionary<ScoringCalculationModel, bool>();

        /// <inheritdoc/>
        public decimal MedianBalancePrecision { get; init; }

        /// <inheritdoc/>
        public TimeSpan? MedianBalanceStartFrom { get; init; }

        /// <inheritdoc/>
        public int? MedianBalanceLastCount { get; init; }

        /// <inheritdoc/>
        public IDictionary<ScoringCalculationModel, List<CounterpartyData>> CounterpartiesFilterData { get; init; } =
            new Dictionary<ScoringCalculationModel, List<CounterpartyData>>();
    }
}