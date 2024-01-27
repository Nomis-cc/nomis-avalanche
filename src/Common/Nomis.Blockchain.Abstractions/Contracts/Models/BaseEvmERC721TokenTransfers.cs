// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmERC721TokenTransfers.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// ERC-721 token transfers.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BaseEvmERC721TokenTransfers :
        IBaseEvmTransferList<BaseEvmERC721TokenTransfer>
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
        public virtual IList<BaseEvmERC721TokenTransfer>? Data { get; set; } = new List<BaseEvmERC721TokenTransfer>();
    }
}