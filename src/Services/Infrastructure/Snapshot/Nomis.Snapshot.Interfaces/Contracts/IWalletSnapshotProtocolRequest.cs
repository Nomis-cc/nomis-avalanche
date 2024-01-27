// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletSnapshotProtocolRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Snapshot.Interfaces.Contracts
{
    /// <summary>
    /// Wallet Snapshot Protocol request.
    /// </summary>
    public interface IWalletSnapshotProtocolRequest :
        IHasAddress
    {
        /// <summary>
        /// Get wallet Snapshot protocol data (votes and proposals).
        /// </summary>
        public bool GetSnapshotProtocolData { get; set; }
    }
}