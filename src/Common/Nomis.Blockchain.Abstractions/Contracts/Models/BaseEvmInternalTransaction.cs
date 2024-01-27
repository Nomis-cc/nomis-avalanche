// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmInternalTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// Internal transaction.
    /// </summary>
    public class BaseEvmInternalTransaction :
        IInternalTransaction,
        IBaseEvmTransfer
    {
        /// <inheritdoc />
        [JsonPropertyName("timeStamp")]
        public virtual string? Timestamp { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("contractAddress")]
        public virtual string? ContractAddress { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("hash")]
        public virtual string? Hash { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("from")]
        public virtual string? From { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("to")]
        public virtual string? To { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("value")]
        public virtual string? Value { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("blockNumber")]
        public virtual string? BlockNumber { get; set; }
    }
}