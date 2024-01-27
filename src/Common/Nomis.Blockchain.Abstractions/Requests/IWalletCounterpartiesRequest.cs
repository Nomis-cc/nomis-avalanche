// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletCounterpartiesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Requests
{
    /// <summary>
    /// Wallet counterparties request.
    /// </summary>
    public interface IWalletCounterpartiesRequest
    {
        /// <summary>
        /// Use only counterparties contracts for score calculation.
        /// </summary>
        public bool CalculateOnlyCounterparties { get; set; }
    }
}