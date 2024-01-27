// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletHapiProtocolRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.HapiExplorer.Interfaces.Contracts
{
    /// <summary>
    /// Wallet HAPI Protocol request.
    /// </summary>
    public interface IWalletHapiProtocolRequest
    {
        /// <summary>
        /// Get wallet HAPI Protocol risk score data (with adjusting score value).
        /// </summary>
        /// <remarks>
        /// You can not mint token without HAPI Protocol adjusting score.
        /// </remarks>
        public bool GetHapiProtocolData { get; set; }
    }
}