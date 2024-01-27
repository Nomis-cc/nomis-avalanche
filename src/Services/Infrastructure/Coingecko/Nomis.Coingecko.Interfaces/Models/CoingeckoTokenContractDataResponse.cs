// ------------------------------------------------------------------------------------------------------
// <copyright file="CoingeckoTokenContractDataResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Coingecko.Interfaces.Models
{
    /// <summary>
    /// Coingecko token contract data response.
    /// </summary>
    public class CoingeckoTokenContractDataResponse
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Asset platform token detail.
        /// </summary>
        [JsonPropertyName("detail_platforms")]
        public IDictionary<string, CoingeckoTokenPlatformDetailData> DetailPlatforms { get; set; } = new Dictionary<string, CoingeckoTokenPlatformDetailData>();

        /// <summary>
        /// Asset platform id.
        /// </summary>
        [JsonPropertyName("asset_platform_id")]
        public string? AssetPlatformId { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        [JsonPropertyName("image")]
        public IDictionary<string, string> Image { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Contract address.
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }
    }
}