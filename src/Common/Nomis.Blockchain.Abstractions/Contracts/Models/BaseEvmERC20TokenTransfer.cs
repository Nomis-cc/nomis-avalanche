// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmERC20TokenTransfer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// ERC-20 token transfer.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BaseEvmERC20TokenTransfer :
        IERC20TokenTransfer,
        IBaseEvmTransfer,
        IBaseEvmTokenData
    {
        /// <inheritdoc />
        [JsonPropertyName("blockNumber")]
        public virtual string? BlockNumber { get; set; }

        /// <summary>
        /// Timestamp.
        /// </summary>
        [JsonPropertyName("timeStamp")]
        public virtual string? TimeStamp { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("hash")]
        public virtual string? Hash { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("from")]
        public virtual string? From { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("contractAddress")]
        public virtual string? ContractAddress { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("to")]
        public virtual string? To { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("value")]
        public virtual string? Value { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("tokenName")]
        public virtual string? TokenName { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("tokenSymbol")]
        public virtual string? TokenSymbol { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("tokenDecimal")]
        public virtual string? TokenDecimal { get; set; }
    }
}