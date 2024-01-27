// ------------------------------------------------------------------------------------------------------
// <copyright file="LlamafolioTokenBalancesProtocolGroupBalanceBaseData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.DefiLlama.Interfaces.Models
{
    /// <summary>
    /// Llamafolio token balances protocol group balance base data.
    /// </summary>
    public class LlamafolioTokenBalancesProtocolGroupBalanceBaseData
    {
        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Address { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Symbol { get; set; }

        /// <summary>
        /// Decimals.
        /// </summary>
        [JsonPropertyName("decimals")]
        public object? Decimals { get; set; }

        /// <summary>
        /// Stable.
        /// </summary>
        [JsonPropertyName("stable")]
        public bool? Stable { get; set; }

        /// <summary>
        /// Price in USD.
        /// </summary>
        [JsonPropertyName("price")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Amount.
        /// </summary>
        [JsonPropertyName("amount")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Amount { get; set; }

        /// <summary>
        /// Balance in USD.
        /// </summary>
        [JsonPropertyName("balanceUSD")]
        public decimal BalanceUSD { get; set; }
    }
}