// ------------------------------------------------------------------------------------------------------
// <copyright file="IGreysafeService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Greysafe.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Greysafe.Interfaces
{
    /// <summary>
    /// Greysafe scam reporting service.
    /// </summary>
    public interface IGreysafeService :
        IInfrastructureService
    {
        /// <summary>
        /// Get wallet scam reports.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <returns>Returns <see cref="GreysafeReportsResponse"/> result.</returns>
        public Task<Result<GreysafeReportsResponse>> GetWalletReportsAsync(string address);
    }
}