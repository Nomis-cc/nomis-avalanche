// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.LensProtocol.Interfaces.Contracts;
using Nomis.LensProtocol.Interfaces.Models;
using Nomis.LensProtocol.Interfaces.Requests;
using Nomis.LensProtocol.Interfaces.Responses;
using Nomis.Utils.Contracts.Properties;

namespace Nomis.LensProtocol.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get Lens Protocol data.
        /// </summary>
        /// <typeparam name="TWalletRequest">The waller request type.</typeparam>
        /// <param name="service"><see cref="ILensProtocolService"/>.</param>
        /// <param name="request"><see cref="IWalletLensProtocolRequest"/>.</param>
        /// <returns>Returns the Lens Protocol data.</returns>
        public static async Task<LensProtocolData?> DataAsync<TWalletRequest>(
            this ILensProtocolService service,
            TWalletRequest? request)
            where TWalletRequest : IWalletLensProtocolRequest, IHasAddress
        {
            LensProtocolProfileData? lensProtocolProfileData = null;
            if (request?.GetLensProtocolData == true)
            {
                var profileResult = await service.ProfileDataAsync(new LensProtocolProfileRequest
                {
                    Owner = request.Address
                }).ConfigureAwait(false);
                lensProtocolProfileData = profileResult.Data;
            }

            return new LensProtocolData
            {
                Profile = lensProtocolProfileData
            };
        }
    }
}