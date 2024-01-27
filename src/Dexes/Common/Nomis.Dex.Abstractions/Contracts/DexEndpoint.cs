// ------------------------------------------------------------------------------------------------------
// <copyright file="DexEndpoint.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Enums;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// DEX endpoint.
    /// </summary>
    public class DexEndpoint
    {
        /// <summary>
        /// Blockchain.
        /// </summary>
        public Chain Blockchain { get; set; }

        /// <summary>
        /// DEX provider API base URI.
        /// </summary>
        public string? ApiUri { get; set; }

        /// <summary>
        /// Factory smart-contract address.
        /// </summary>
        public string? FactoryAddress { get; set; }

        /// <summary>
        /// Factory smart-contract init code hash.
        /// </summary>
        public string? FactoryInitCodePairHash { get; set; }

        /// <summary>
        /// Router smart-contract address.
        /// </summary>
        public string? RouterAddress { get; set; }

        /// <summary>
        /// Oracle smart-contract address.
        /// </summary>
        public string? OracleAddress { get; set; }

        /// <summary>
        /// Can be used in calculations.
        /// </summary>
        public bool CanBeUsed => !string.IsNullOrWhiteSpace(RouterAddress) && !string.IsNullOrWhiteSpace(FactoryAddress);

        /// <summary>
        /// Can be used for getting prices.
        /// </summary>
        public bool CanGetPrices => !string.IsNullOrWhiteSpace(OracleAddress);
    }
}