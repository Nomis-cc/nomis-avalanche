// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheWalletStatsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Nomis.Aave.Interfaces.Contracts;
using Nomis.Blockchain.Abstractions.Requests;
using Nomis.Dex.Abstractions.Contracts;

namespace Nomis.Snowtrace.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the wallet stats for Avalanche.
    /// </summary>
    public sealed class AvalancheWalletStatsRequest :
        BaseEvmWalletStatsRequest,
        IWalletTokensSwapPairsRequest,
        IWalletAaveProtocolRequest
    {
        /// <inheritdoc />
        /// <example>false</example>
        [FromQuery]
        public bool GetTokensSwapPairs { get; set; } = false;

        /// <inheritdoc />
        /// <example>100</example>
        [FromQuery]
        [Range(typeof(int), "1", "1000")]
        public int FirstSwapPairs { get; set; } = 100;

        /// <inheritdoc />
        /// <example>0</example>
        [FromQuery]
        [Range(typeof(int), "0", "2147483647")]
        public int Skip { get; set; } = 0;

        /// <inheritdoc />
        /// <example>false</example>
        [FromQuery]
        public bool GetAaveProtocolData { get; set; } = false;

        /// <inheritdoc />
        /// <example>false</example>
        [FromQuery]
        public bool FromCache { get; set; } = false;
    }
}