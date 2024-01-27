// ------------------------------------------------------------------------------------------------------
// <copyright file="OffchainOraclesData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Offchain oracles data.
    /// </summary>
    public class OffchainOraclesData
    {
        /// <summary>
        /// Offchain oracle smart-contract address.
        /// </summary>
        public string? OffchainOracleAddress { get; set; }

        /// <summary>
        /// MultiWrapper smart-contract address.
        /// </summary>
        public string? MultiWrapperAddress { get; set; }
    }
}