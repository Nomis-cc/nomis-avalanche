// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceCheckerTokenInfo.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nomis.BalanceChecker.Interfaces.Contracts
{
    /// <summary>
    /// Balance checker token info.
    /// </summary>
    public sealed class BalanceCheckerTokenInfo
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Parameter("address", "id", 1)]
        public string? Id { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        [Parameter("string", "symbol", 2)]
        public string? Symbol { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Parameter("string", "name", 3)]
        public string? Name { get; set; }

        /// <summary>
        /// Balance.
        /// </summary>
        [Parameter("uint256", "balance", 4)]
        [JsonIgnore]
        public BigInteger Balance { get; set; }

        /// <summary>
        /// Decimals.
        /// </summary>
        [Parameter("uint8", "decimals", 5)]
        public int Decimals { get; set; }

        /// <summary>
        /// Balance amount.
        /// </summary>
        public string Amount => Balance.ToString();

        /// <summary>
        /// URL of the token's logo image.
        /// </summary>
        public string? LogoUri { get; set; }

        /// <summary>
        /// USD price.
        /// </summary>
        /// <remarks>
        /// Price of 0 means no data.
        /// </remarks>
        public decimal Price { get; set; }

        /// <summary>
        /// Token data source.
        /// </summary>
        public string? Source { get; set; }
    }
}