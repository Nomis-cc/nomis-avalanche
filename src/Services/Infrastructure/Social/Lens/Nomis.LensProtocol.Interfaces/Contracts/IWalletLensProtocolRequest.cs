// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletLensProtocolRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.LensProtocol.Interfaces.Contracts
{
    /// <summary>
    /// Wallet Lens Protocol request.
    /// </summary>
    public interface IWalletLensProtocolRequest :
        IHasAddress
    {
        /// <summary>
        /// Get Lens Protocol data.
        /// </summary>
        public bool GetLensProtocolData { get; set; }
    }
}