// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenBalancesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.DefiLlama.Interfaces.Models;

namespace Nomis.BalanceChecker.Interfaces.Requests
{
    /// <summary>
    /// Token balances request.
    /// </summary>
    public class TokenBalancesRequest
    {
        /// <summary>
        /// Owner wallet address.
        /// </summary>
        public string? Owner { get; set; }

        /// <summary>
        /// Blockchain descriptor.
        /// </summary>
        public IBlockchainDescriptor BlockchainDescriptor { get; set; } = null!;

        /// <summary>
        /// Token addresses.
        /// </summary>
        public IList<string> TokenAddresses { get; set; } = new List<string>();

        /// <summary>
        /// Stablecoins prices.
        /// </summary>
        public IList<TokenPriceData> StablecoinsPrices { get; set; } = new List<TokenPriceData>();

        /// <summary>
        /// Use Covalent API for getting token holding.
        /// </summary>
        public bool UseCovalentApi { get; init; }

        /// <summary>
        /// Use De.Fi API for getting token holding.
        /// </summary>
        public bool UseDeFiApi { get; init; }

        /// <summary>
        /// Use RPC API (smart-contract) for getting token holding.
        /// </summary>
        public bool UseRpcApi { get; init; }

        /// <summary>
        /// Is EVM address.
        /// </summary>
        public bool IsEvmAddress { get; init; } = true;
    }
}