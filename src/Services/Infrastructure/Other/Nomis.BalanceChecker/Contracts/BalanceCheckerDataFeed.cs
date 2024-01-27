// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceCheckerDataFeed.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.BalanceChecker.Interfaces.Enums;

namespace Nomis.BalanceChecker.Contracts
{
    /// <summary>
    /// Balance checker data feed.
    /// </summary>
    internal sealed class BalanceCheckerDataFeed
    {
        /// <summary>
        /// Blockchain.
        /// </summary>
        public BalanceCheckerChain Blockchain { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        public string? ContractAddress { get; set; }

        /// <summary>
        /// Contract ABI.
        /// </summary>
        public string? ContractAbi { get; set; }

        /// <summary>
        /// Method name.
        /// </summary>
        public string? MethodName { get; set; }

        /// <summary>
        /// Blockchain RPC URL.
        /// </summary>
        public string? RpcUrl { get; set; }

        /// <summary>
        /// Addresses batch limit.
        /// </summary>
        public int BatchLimit { get; set; } = 10000;
    }
}