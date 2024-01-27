// ------------------------------------------------------------------------------------------------------
// <copyright file="IHapiExplorerService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.HapiExplorer.Interfaces
{
    /// <summary>
    /// HAPI explorer service.
    /// </summary>
    public interface IHapiExplorerService :
        IInfrastructureService
    {
        /// <summary>
        /// Get wallet risk score.
        /// </summary>
        /// <param name="network">Network.</param>
        /// <param name="address">Wallet address.</param>
        /// <returns>Returns <see cref="HapiProxyRiskScoreResponse"/> result.</returns>
        public Task<Result<HapiProxyRiskScoreResponse>> GetWalletRiskScoreAsync(string network, string address);
    }
}