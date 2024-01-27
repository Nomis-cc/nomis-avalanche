// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmERC1155TokenTransfer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// ERC-1155 token transfer.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BaseEvmERC1155TokenTransfer :
        NFTTokenTransfer,
        IBaseEvmTransfer
    {
        /// <inheritdoc />
        [JsonPropertyName("blockNumber")]
        public virtual string? BlockNumber { get; set; }
    }
}