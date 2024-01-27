// ------------------------------------------------------------------------------------------------------
// <copyright file="SwapPairData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions.Contracts.Data;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <inheritdoc cref="ISwapPairData"/>
    public class SwapPairData :
        ISwapPairData
    {
        /// <inheritdoc/>
        [JsonPropertyName("lastCheck")]
        public DateTime LastCheck { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("token0")]
        public TokenData? Token0 { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("token1")]
        public TokenData? Token1 { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("token0Price")]
        public string Token0Price { get; set; } = "0";

        /// <inheritdoc/>
        [JsonPropertyName("token1Price")]
        public string Token1Price { get; set; } = "0";

        /// <inheritdoc/>
        [JsonPropertyName("reserve0")]
        public string Reserve0 { get; set; } = "0";

        /// <inheritdoc/>
        [JsonPropertyName("reserve1")]
        public string Reserve1 { get; set; } = "0";

        /// <inheritdoc/>
        [JsonPropertyName("syncAtTimestamp")]
        public string? SyncAtTimestamp { get; set; }
    }
}