// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenPriceData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Utils.Extensions;

namespace Nomis.DefiLlama.Interfaces.Models
{
    /// <summary>
    /// DefiLlama token price data.
    /// </summary>
    public class TokenPriceData
    {
        /// <summary>
        /// Initialize <see cref="TokenPriceData"/>.
        /// </summary>
        public TokenPriceData()
        {
        }

        /// <summary>
        /// Initialize <see cref="TokenPriceData"/>.
        /// </summary>
        /// <param name="defiLlamaTokenPriceData"><see cref="TokenPriceData"/>.</param>
        public TokenPriceData(
            TokenPriceData defiLlamaTokenPriceData)
        {
            Id = defiLlamaTokenPriceData.Id;
            Decimals = defiLlamaTokenPriceData.Decimals;
            Price = defiLlamaTokenPriceData.Price;
            Symbol = defiLlamaTokenPriceData.Symbol;
            LogoUri = defiLlamaTokenPriceData.LogoUri;
            Timestamp = defiLlamaTokenPriceData.Timestamp;
            Confidence = defiLlamaTokenPriceData.Confidence;
        }

        /// <summary>
        /// Token id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Decimals.
        /// </summary>
        [JsonPropertyName("decimals")]
        public int? Decimals { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Logo URI.
        /// </summary>
        [JsonPropertyName("logoUri")]
        public string? LogoUri { get; set; }

        /// <summary>
        /// Timestamp.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public ulong? Timestamp { get; set; }

        /// <summary>
        /// Last price date and time.
        /// </summary>
        public DateTime? LastPriceDateTime => Timestamp?.ToString().ToDateTime();

        /// <summary>
        /// Confidence.
        /// </summary>
        [JsonPropertyName("confidence")]
        public decimal Confidence { get; set; }
    }
}