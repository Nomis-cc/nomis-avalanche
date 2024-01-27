// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmERC1155TokenTransfers.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// ERC-1155 token transfers.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BaseEvmERC1155TokenTransfers :
        IBaseEvmTransferList<BaseEvmERC1155TokenTransfer>
    {
        /// <summary>
        /// Status.
        /// </summary>
        [JsonPropertyName("status")]
        public virtual int Status { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        [JsonPropertyName("message")]
        public virtual string? Message { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("result")]
        [DataMember(EmitDefaultValue = true)]
        public virtual IList<BaseEvmERC1155TokenTransfer>? Data { get; set; } = new List<BaseEvmERC1155TokenTransfer>();
    }
}