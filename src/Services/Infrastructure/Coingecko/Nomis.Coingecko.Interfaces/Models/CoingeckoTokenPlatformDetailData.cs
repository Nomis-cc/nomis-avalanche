// ------------------------------------------------------------------------------------------------------
// <copyright file="CoingeckoTokenPlatformDetailData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Coingecko.Interfaces.Models
{
    /// <summary>
    /// Coingecko token platform detail data.
    /// </summary>
    public class CoingeckoTokenPlatformDetailData
    {
        /// <summary>
        /// Decimal place.
        /// </summary>
        [JsonPropertyName("decimal_place")]
        public int DecimalPlace { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }
    }
}