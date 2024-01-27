// ------------------------------------------------------------------------------------------------------
// <copyright file="INFTTokenTransfer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// NFT token transfer.
    /// </summary>
    public interface INFTTokenTransfer
    {
        /// <summary>
        /// Hash.
        /// </summary>
        public string? Hash { get; set; }

        /// <summary>
        /// From address.
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        public string? ContractAddress { get; set; }

        /// <summary>
        /// To address.
        /// </summary>
        public string? To { get; set; }

        /// <summary>
        /// Token identifier.
        /// </summary>
        public string? TokenId { get; set; }
    }
}