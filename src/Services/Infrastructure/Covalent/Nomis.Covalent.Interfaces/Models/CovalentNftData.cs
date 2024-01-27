// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentNftData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent NFT data.
    /// </summary>
    public class CovalentNftData
    {
        /// <summary>
        /// Contract name.
        /// </summary>
        [JsonPropertyName("contract_name")]
        public string? ContractName { get; set; }

        /// <summary>
        /// Contract ticker symbol.
        /// </summary>
        [JsonPropertyName("contract_ticker_symbol")]
        public string? ContractTickerSymbol { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }

        /// <summary>
        /// Is spam.
        /// </summary>
        [JsonPropertyName("is_spam")]
        public bool IsSpam { get; set; }

        /// <summary>
        /// Balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public string? Balance { get; set; }
    }
}