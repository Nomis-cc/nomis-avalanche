// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Greysafe.Interfaces.Contracts;
using Nomis.Greysafe.Interfaces.Responses;
using Nomis.Utils.Contracts.Properties;
using Nomis.Utils.Exceptions;

namespace Nomis.Greysafe.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get Greysafe reports.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="IGreysafeService"/>.</param>
        /// <param name="request"><see cref="IWalletGreysafeRequest"/>.</param>
        /// <returns>Returns Greysafe reports.</returns>
        public static async Task<GreysafeReportsResponse?> ReportsAsync<TWalletRequest>(
            this IGreysafeService service,
            TWalletRequest? request)
            where TWalletRequest : IWalletGreysafeRequest, IHasAddress
        {
            GreysafeReportsResponse? greysafeReportsResponse = null;
            if (request?.GetGreysafeData == true && !string.IsNullOrWhiteSpace(request.Address))
            {
                try
                {
                    greysafeReportsResponse =
                        (await service.GetWalletReportsAsync(request.Address).ConfigureAwait(false)).Data;
                }
                catch (NoDataException)
                {
                    // ignored
                }
                catch (HttpRequestException)
                {
                    // ignored
                }
            }

            return greysafeReportsResponse;
        }
    }
}