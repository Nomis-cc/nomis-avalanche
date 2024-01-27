// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTallyProtocolRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Tally.Interfaces.Contracts
{
    /// <summary>
    /// Wallet Tally Protocol request.
    /// </summary>
    public interface IWalletTallyProtocolRequest :
        IHasAddress
    {
        /// <summary>
        /// Get wallet Tally protocol data.
        /// </summary>
        public bool GetTallyProtocolData { get; set; }
    }
}