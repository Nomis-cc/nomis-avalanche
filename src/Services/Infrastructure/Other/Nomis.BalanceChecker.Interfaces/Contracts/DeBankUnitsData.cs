// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankUnitsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.BalanceChecker.Interfaces.Contracts
{
    /// <summary>
    /// DeBank units data.
    /// </summary>
    public sealed class DeBankUnitsData
    {
        /// <summary>
        /// Remaining units.
        /// </summary>
        [JsonPropertyName("balance")]
        public long Balance { get; set; }
    }
}