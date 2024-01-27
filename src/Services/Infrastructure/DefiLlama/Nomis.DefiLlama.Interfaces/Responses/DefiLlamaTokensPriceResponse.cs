// ------------------------------------------------------------------------------------------------------
// <copyright file="DefiLlamaTokensPriceResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.DefiLlama.Interfaces.Models;

namespace Nomis.DefiLlama.Interfaces.Responses
{
    /// <summary>
    /// DefiLlama tokens price response.
    /// </summary>
    public class DefiLlamaTokensPriceResponse
    {
        /// <summary>
        /// Tokens prices.
        /// </summary>
        [JsonPropertyName("coins")]
        public IDictionary<string, TokenPriceData> TokensPrices { get; set; } = new Dictionary<string, TokenPriceData>();
    }
}