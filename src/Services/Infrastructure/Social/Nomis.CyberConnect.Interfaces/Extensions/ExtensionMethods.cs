// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.CyberConnect.Interfaces.Contracts;
using Nomis.CyberConnect.Interfaces.Models;
using Nomis.CyberConnect.Interfaces.Requests;
using Nomis.CyberConnect.Interfaces.Responses;
using Nomis.Utils.Contracts.Properties;

namespace Nomis.CyberConnect.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get CyberConnect data.
        /// </summary>
        /// <typeparam name="TWalletRequest">The waller request type.</typeparam>
        /// <param name="service"><see cref="ICyberConnectService"/>.</param>
        /// <param name="request"><see cref="IWalletCyberConnectProtocolRequest"/>.</param>
        /// <param name="chainId">Blockchain id.</param>
        /// <returns>Returns the CyberConnect data.</returns>
        public static async Task<CyberConnectData> DataAsync<TWalletRequest>(
            this ICyberConnectService service,
            TWalletRequest? request,
            ulong chainId)
            where TWalletRequest : IWalletCyberConnectProtocolRequest, IHasAddress
        {
            CyberConnectProfileData? cyberConnectProfileData = null;
            IEnumerable<CyberConnectEssenceData>? cyberConnectEssenceData = null;
            IEnumerable<CyberConnectLikeData>? cyberConnectLikeData = null;
            IEnumerable<CyberConnectSubscribingProfileData>? cyberConnectSubscribings = null;
            if (request?.GetCyberConnectProtocolData == true)
            {
                var handle = await service.HandleAsync(new CyberConnectHandleRequest
                {
                    Address = request.Address
                }).ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(handle.Data))
                {
                    cyberConnectProfileData = (await service.ProfileDataAsync(new CyberConnectProfileRequest
                    {
                        Handle = handle.Data,
                        ChainId = chainId
                    }).ConfigureAwait(false)).Data;
                    cyberConnectEssenceData = (await service.EssencesAsync(new CyberConnectEssencesRequest
                    {
                        Handle = handle.Data
                    }).ConfigureAwait(false)).Data;
                }

                cyberConnectSubscribings = (await service.SubscribingsAsync(new CyberConnectSubscribingsRequest
                {
                    Address = request.Address
                }).ConfigureAwait(false)).Data;
                cyberConnectLikeData = (await service.LikesAsync(new CyberConnectLikesRequest
                {
                    Address = request.Address
                }).ConfigureAwait(false)).Data;
            }

            return new CyberConnectData
            {
                Profile = cyberConnectProfileData,
                Essences = cyberConnectEssenceData,
                Likes = cyberConnectLikeData,
                Subscribings = cyberConnectSubscribings
            };
        }
    }
}