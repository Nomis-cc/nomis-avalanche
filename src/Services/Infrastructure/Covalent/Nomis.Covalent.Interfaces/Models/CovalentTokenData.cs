// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent token data.
    /// </summary>
    public class CovalentTokenData
    {
        /// <summary>
        /// Contract decimals.
        /// </summary>
        [JsonPropertyName("contract_decimals")]
        public int? ContractDecimals { get; set; }

        /// <summary>
        /// Contract name.
        /// </summary>
        [JsonPropertyName("contract_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ContractName { get; set; }

        /// <summary>
        /// The ticker symbol for this contract.
        /// </summary>
        /// <remarks>
        /// This field is set by a developer and non-unique across a network.
        /// </remarks>
        [JsonPropertyName("contract_ticker_symbol")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ContractTickerSymbol { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string ContractAddress { get; set; } = null!;

        /// <summary>
        /// A list of supported standard ERC interfaces, eg: ERC20 and ERC721.
        /// </summary>
        [JsonPropertyName("supports_erc")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<string>? SupportsErc { get; set; }

        /// <summary>
        /// The contract logo URL.
        /// </summary>
        [JsonPropertyName("logo_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LogoUrl { get; set; }

        /// <summary>
        /// The timestamp when the token was transferred.
        /// </summary>
        [JsonPropertyName("last_transferred_at")]
        public DateTime? LastTransferredAt { get; set; }

        /// <summary>
        /// Indicates if a token is the chain's native gas token, eg: ETH on Ethereum.
        /// </summary>
        [JsonPropertyName("native_token")]
        public bool NativeToken { get; set; }

        /// <summary>
        /// Token type.
        /// </summary>
        /// <remarks>
        /// One of `cryptocurrency`, `stablecoin`, `nft` or `dust`.
        /// </remarks>
        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }

        /// <summary>
        /// Denotes whether the token is suspected spam.
        /// </summary>
        [JsonPropertyName("is_spam")]
        public bool IsSpam { get; set; }

        /// <summary>
        /// The asset balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public string Balance { get; set; } = null!;

        /// <summary>
        /// The exchange rate for the requested quote currency.
        /// </summary>
        [JsonPropertyName("quote_rate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? QuoteRate { get; set; }

        /// <summary>
        /// The current balance converted to fiat in quote currency.
        /// </summary>
        [JsonPropertyName("quote")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Quote { get; set; }

        /// <summary>
        /// NFT-specific data.
        /// </summary>
        [JsonPropertyName("nft_data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<CovalentNftTokenData>? NftData { get; set; }
    }
}