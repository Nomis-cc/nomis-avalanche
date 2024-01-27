// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveDataFeed.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Aave.Interfaces.Enums;

namespace Nomis.Aave.Contracts
{
    /// <summary>
    /// Aave data feed.
    /// </summary>
    internal sealed class AaveDataFeed
    {
        /// <summary>
        /// Blockchain.
        /// </summary>
        public AaveChain Blockchain { get; set; }

        /// <summary>
        /// Pool contract address.
        /// </summary>
        public string? PoolContractAddress { get; set; }

        /// <summary>
        /// Pool contract ABI.
        /// </summary>
        public string? PoolContractAbi { get; set; }

        /// <summary>
        /// Blockchain RPC URL.
        /// </summary>
        public string? RpcUrl { get; set; }
    }
}