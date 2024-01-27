// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Chainanalysis.Interfaces.Contracts;
using Nomis.Chainanalysis.Interfaces.Responses;
using Nomis.Utils.Contracts.Properties;
using Nomis.Utils.Exceptions;

namespace Nomis.Chainanalysis.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get Chainanalysis reports.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="IChainanalysisService"/>.</param>
        /// <param name="request"><see cref="IWalletChainanalysisRequest"/>.</param>
        /// <returns>Returns Chainanalysis reports.</returns>
        public static async Task<ChainanalysisReportsResponse?> ReportsAsync<TWalletRequest>(
            this IChainanalysisService service,
            TWalletRequest? request)
            where TWalletRequest : IWalletChainanalysisRequest, IHasAddress
        {
            ChainanalysisReportsResponse? chainanalysisReportsResponse = null;
            if (request?.GetChainanalysisData == true && !string.IsNullOrWhiteSpace(request.Address))
            {
                try
                {
                    chainanalysisReportsResponse = (await service.GetWalletReportsAsync(request.Address).ConfigureAwait(false)).Data;
                }
                catch (NoDataException)
                {
                    // ignored
                }
            }

            return chainanalysisReportsResponse;
        }
    }
}