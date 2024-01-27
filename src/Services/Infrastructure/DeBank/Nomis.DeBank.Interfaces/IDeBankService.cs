// ------------------------------------------------------------------------------------------------------
// <copyright file="IDeBankService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.DeBank.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.DeBank.Interfaces
{
    /// <summary>
    /// DeBank service.
    /// </summary>
    public interface IDeBankService :
        IInfrastructureService
    {
        /// <summary>
        /// Get DeBank units data.
        /// </summary>
        /// <param name="cancellationToken">&lt;see cref="CancellationToken"/&gt;.</param>
        /// <returns>Returns DeBank units data.</returns>
        public Task<DeBankUnitsData> UnitsDataAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hold tokens data by wallet address.
        /// </summary>
        /// <param name="owner">Blockchain id.</param>
        /// <param name="debankChainId">DeBank blockchain internal id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns hold tokens data by wallet address.</returns>
        public Task<Result<IList<DeBankTokenData>>> HoldTokensDataAsync(
            string owner,
            string debankChainId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get tokens data by ids.
        /// </summary>
        /// <param name="ids">Token ids.</param>
        /// <param name="debankChainId">DeBank blockchain internal id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns tokens data by ids.</returns>
        public Task<Result<IList<DeBankTokenData>>> TokensDataAsync(
            IList<string> ids,
            string debankChainId,
            CancellationToken cancellationToken = default);
    }
}