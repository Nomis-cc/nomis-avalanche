// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletAaveProtocolRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Aave.Interfaces.Contracts
{
    /// <summary>
    /// Wallet Aave Protocol request.
    /// </summary>
    public interface IWalletAaveProtocolRequest
    {
        /// <summary>
        /// Get wallet Aave protocol data.
        /// </summary>
        public bool GetAaveProtocolData { get; set; }
    }
}