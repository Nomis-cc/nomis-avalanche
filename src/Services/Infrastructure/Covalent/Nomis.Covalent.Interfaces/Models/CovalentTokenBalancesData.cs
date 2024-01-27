// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentTokenBalancesData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent token balances data.
    /// </summary>
    public class CovalentTokenBalancesData
    {
        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// Updated at.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Quote currency.
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string QuoteCurrency { get; set; } = null!;

        /// <summary>
        /// Blockchain id.
        /// </summary>
        [JsonPropertyName("chain_id")]
        public ulong ChainId { get; set; }

        /// <summary>
        /// Blockchain name.
        /// </summary>
        [JsonPropertyName("chain_name")]
        public string ChainName { get; set; } = null!;

        /// <summary>
        /// Token data list.
        /// </summary>
        [JsonPropertyName("items")]
        public IList<CovalentTokenData> Tokens { get; set; } = new List<CovalentTokenData>();

        /// <summary>
        /// Total tokens quote.
        /// </summary>
        [JsonPropertyName("total_quote")]
        public decimal TotalQuote => Tokens.Sum(t => t.Quote ?? 0);
    }
}