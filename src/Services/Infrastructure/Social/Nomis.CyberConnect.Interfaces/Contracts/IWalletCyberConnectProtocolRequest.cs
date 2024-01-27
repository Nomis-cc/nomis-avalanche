// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletCyberConnectProtocolRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.CyberConnect.Interfaces.Contracts
{
    /// <summary>
    /// Wallet CyberConnect Protocol request.
    /// </summary>
    public interface IWalletCyberConnectProtocolRequest :
        IHasAddress
    {
        /// <summary>
        /// Get CyberConnect protocol data.
        /// </summary>
        public bool GetCyberConnectProtocolData { get; set; }
    }
}