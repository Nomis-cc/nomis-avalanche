// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedCounterpartiesDataLists.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Models;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Extended counterparties data lists.
    /// </summary>
    public sealed class ExtendedCounterpartiesDataLists<TNormalTransaction, TInternalTransaction, TERC20TokenTransfer>
        where TNormalTransaction : INormalTransaction
        where TInternalTransaction : IInternalTransaction
        where TERC20TokenTransfer : IERC20TokenTransfer
    {
        /// <summary>
        /// Extended counterparties data list.
        /// </summary>
        public IList<ExtendedCounterpartyData>? ExtendedCounterpartiesDataList { get; set; }

        /// <summary>
        /// Counterparties transactions.
        /// </summary>
        public IList<TNormalTransaction> Transactions { get; set; } = new List<TNormalTransaction>();

        /// <summary>
        /// Counterparties internal transactions.
        /// </summary>
        public IList<TInternalTransaction> InternalTransactions { get; set; } = new List<TInternalTransaction>();

        /// <summary>
        /// Counterparties ERC20 token transfers.
        /// </summary>
        public IList<TERC20TokenTransfer> Erc20Tokens { get; set; } = new List<TERC20TokenTransfer>();

        /// <summary>
        /// Counterparties NFT token transfers.
        /// </summary>
        public IList<NFTTokenTransfer> TokenTransfers { get; set; } = new List<NFTTokenTransfer>();
    }
}