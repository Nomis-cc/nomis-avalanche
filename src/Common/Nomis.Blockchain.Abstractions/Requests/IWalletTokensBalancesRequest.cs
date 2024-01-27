// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTokensBalancesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Requests
{
    /// <summary>
    /// Wallet tokens balances request.
    /// </summary>
    public interface IWalletTokensBalancesRequest
    {
        /// <summary>
        /// Get wallet hold tokens balances.
        /// </summary>
        public bool GetHoldTokensBalances { get; set; }

        /// <summary>
        /// Time range in hours on either side to find price data for token balances.
        /// </summary>
        public int SearchWidthInHours { get; set; }

        /// <summary>
        /// Use token lists for getting tokens data.
        /// </summary>
        public bool UseTokenLists { get; set; }

        /// <summary>
        /// Include universal token lists.
        /// </summary>
        public bool IncludeUniversalTokenLists { get; set; }

        /// <summary>
        /// Get wallet tokens transfers balances.
        /// </summary>
        public bool GetTokensTransfersBalances { get; set; }

        /// <summary>
        /// Show wallet tokens transfers balances with zero price.
        /// </summary>
        public bool ShowTokensTransfersWithZeroPrice { get; set; }

        /// <summary>
        /// Time range in hours on either side to find price data for tokens transfers.
        /// </summary>
        public int TransfersSearchWidthInHours { get; set; }
    }
}