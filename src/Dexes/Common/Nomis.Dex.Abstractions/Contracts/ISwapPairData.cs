// ------------------------------------------------------------------------------------------------------
// <copyright file="ISwapPairData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Utils.Extensions;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// Swap pair data.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.sushi.com/docs/Developers/Subgraphs/Exchange/Entities#pair"/>.
    /// </remarks>
    public interface ISwapPairData
    {
        /// <summary>
        /// The date the data is received from the API.
        /// </summary>
        [JsonPropertyName("lastCheck")]
        public DateTime LastCheck { get; set; }

        /// <summary>
        /// Swap pair id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// The data of the first token.
        /// </summary>
        [JsonPropertyName("token0")]
        public TokenData? Token0 { get; set; }

        /// <summary>
        /// The data of the second token.
        /// </summary>
        [JsonPropertyName("token1")]
        public TokenData? Token1 { get; set; }

        /// <summary>
        /// The price of the first token.
        /// </summary>
        /// <remarks>
        /// In units of another token in the pair.
        /// </remarks>
        [JsonPropertyName("token0Price")]
        public string Token0Price { get; set; }

        /// <summary>
        /// The price of the second token.
        /// </summary>
        /// <remarks>
        /// In units of another token in the pair.
        /// </remarks>
        [JsonPropertyName("token1Price")]
        public string Token1Price { get; set; }

        /// <summary>
        /// The reserve of the first token.
        /// </summary>
        [JsonPropertyName("reserve0")]
        public string Reserve0 { get; set; }

        /// <summary>
        /// The reserve of the second token.
        /// </summary>
        [JsonPropertyName("reserve1")]
        public string Reserve1 { get; set; }

        /// <summary>
        /// The UTC timestamp of the last sync event occurred.
        /// </summary>
        [JsonPropertyName("syncAtTimestamp")]
        public string? SyncAtTimestamp { get; set; }

        /// <summary>
        /// The UTC date and time of the last sync event occurred.
        /// </summary>
        public DateTime? SyncAtDateTime => SyncAtTimestamp?.ToDateTime();

        /// <summary>
        /// Set value for <see cref="LastCheck"/>.
        /// </summary>
        /// <param name="lastCheck"><see cref="LastCheck"/>.</param>
        public void SetLastCheck(
            DateTime lastCheck)
        {
            LastCheck = lastCheck;
        }
    }
}