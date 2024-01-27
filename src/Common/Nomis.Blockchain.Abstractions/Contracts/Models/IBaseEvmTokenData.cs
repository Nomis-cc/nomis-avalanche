// ------------------------------------------------------------------------------------------------------
// <copyright file="IBaseEvmTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// Token data.
    /// </summary>
    public interface IBaseEvmTokenData
    {
        /// <summary>
        /// Token name.
        /// </summary>
        [JsonPropertyName("tokenName")]
        public string? TokenName { get; set; }

        /// <summary>
        /// Token symbol.
        /// </summary>
        [JsonPropertyName("tokenSymbol")]
        public string? TokenSymbol { get; set; }

        /// <summary>
        /// Token decimal.
        /// </summary>
        [JsonPropertyName("tokenDecimal")]
        public string? TokenDecimal { get; set; }
    }
}