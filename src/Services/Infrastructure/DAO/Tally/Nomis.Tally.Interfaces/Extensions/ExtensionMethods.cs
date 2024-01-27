// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Tally.Interfaces.Contracts;
using Nomis.Tally.Interfaces.Models;
using Nomis.Utils.Contracts.Properties;

namespace Nomis.Tally.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get Tally data.
        /// </summary>
        /// <typeparam name="TWalletRequest">The wallet request type.</typeparam>
        /// <param name="service"><see cref="ITallyService"/>.</param>
        /// <param name="request"><see cref="IWalletTallyProtocolRequest"/>.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <returns>Returns the Tally data.</returns>
        public static async Task<TallyAccount?> AccountDataAsync<TWalletRequest>(
            this ITallyService service,
            TWalletRequest? request,
            ulong chainId)
            where TWalletRequest : IWalletTallyProtocolRequest, IHasAddress
        {
            if (request?.GetTallyProtocolData == true && service.Settings.SupportedChainIds.Contains(chainId))
            {
                return (await service.GetTallyAccountDataAsync(new()
                {
                    Address = request.Address!,
                    ChainId = chainId
                }).ConfigureAwait(false)).Data;
            }

            return null;
        }
    }
}