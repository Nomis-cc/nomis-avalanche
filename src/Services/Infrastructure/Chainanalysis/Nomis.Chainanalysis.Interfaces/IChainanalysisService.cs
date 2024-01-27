// ------------------------------------------------------------------------------------------------------
// <copyright file="IChainanalysisService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Chainanalysis.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Chainanalysis.Interfaces
{
    /// <summary>
    /// Chainanalysis sanctions reporting service.
    /// </summary>
    public interface IChainanalysisService :
        IInfrastructureService
    {
        /// <summary>
        /// Get wallet sanctions reports.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <returns>Returns <see cref="ChainanalysisReportsResponse"/> result.</returns>
        public Task<Result<ChainanalysisReportsResponse>> GetWalletReportsAsync(string address);
    }
}