// ------------------------------------------------------------------------------------------------------
// <copyright file="CounterpartiesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Contracts.Models;
using Nomis.Blockchain.Abstractions.Contracts.Settings;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions.Requests
{
    /// <summary>
    /// Counterparties request.
    /// </summary>
    public class CounterpartiesRequest<TNormalTransaction, TInternalTransaction, TERC20TokenTransfer>
        where TNormalTransaction : INormalTransaction
        where TInternalTransaction : IInternalTransaction
        where TERC20TokenTransfer : IERC20TokenTransfer
    {
        /// <inheritdoc cref="IFilterCounterpartiesByCalculationModelSettings"/>
        public IFilterCounterpartiesByCalculationModelSettings Settings { get; set; } = null!;

#pragma warning disable MA0016
        /// <summary>
        /// Transaction list.
        /// </summary>
        public List<TNormalTransaction> Transactions { get; set; } = null!;

        /// <summary>
        /// Internal transaction list.
        /// </summary>
        public List<TInternalTransaction> InternalTransactions { get; set; } = null!;

        /// <summary>
        /// ERC-20 token transfer list.
        /// </summary>
        public List<TERC20TokenTransfer> Erc20Tokens { get; set; } = null!;

        /// <summary>
        /// NFT token transfer list.
        /// </summary>
        public List<NFTTokenTransfer> TokenTransfers { get; set; } = null!;

        /// <summary>
        /// Tokens data balances.
        /// </summary>
        public List<TokenDataBalance> TokenDataBalances { get; set; } = null!;

        /// <summary>
        /// Tokens prices data.
        /// </summary>
        public Dictionary<string, decimal> TokensPricesData { get; set; } = null!;
#pragma warning restore MA0016

        /// <summary>
        /// Blockchain id.
        /// </summary>
        public ulong ChainId { get; set; }

        /// <summary>
        /// Defillama platform id.
        /// </summary>
        public string? DefillamaPlatformId { get; set; }

        /// <summary>
        /// Native token data balance.
        /// </summary>
        public TokenDataBalance? NativeTokenDataBalance { get; set; }
    }
}