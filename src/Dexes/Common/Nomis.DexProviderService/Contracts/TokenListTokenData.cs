// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenListTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Dex.Abstractions.Enums;

namespace Nomis.DexProviderService.Contracts
{
    /// <summary>
    /// Token list token data.
    /// </summary>
    internal sealed class TokenListTokenData
    {
        /// <summary>
        /// Token address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Chain id.
        /// </summary>
        [JsonPropertyName("chainId")]
        public Chain? ChainId { get; set; }

        /// <summary>
        /// Token symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Token name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Token decimals.
        /// </summary>
        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }

        /// <summary>
        /// Logo URI.
        /// </summary>
        [JsonPropertyName("logoURI")]
        public string? LogoUri { get; set; }
    }
}