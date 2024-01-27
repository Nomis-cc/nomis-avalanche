// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletChainanalysisRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Chainanalysis.Interfaces.Contracts
{
    /// <summary>
    /// Wallet Chainanalysis sanctions reporting service request.
    /// </summary>
    public interface IWalletChainanalysisRequest :
        IHasAddress
    {
        /// <summary>
        /// Get wallet Chainanalysis sanctions reporting service data (with adjusting score value).
        /// </summary>
        /// <remarks>
        /// You can not mint token without Chainanalysis adjusting score.
        /// </remarks>
        public bool GetChainanalysisData { get; set; }
    }
}