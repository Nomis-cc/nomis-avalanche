// ------------------------------------------------------------------------------------------------------
// <copyright file="ICyberConnectService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.CyberConnect.Interfaces.Models;
using Nomis.CyberConnect.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.CyberConnect.Interfaces
{
    /// <summary>
    /// Service for interaction with CyberConnect API.
    /// </summary>
    public interface ICyberConnectService :
        IInfrastructureService
    {
        /// <summary>
        /// Get the CyberConnect handle.
        /// </summary>
        /// <param name="request">CyberConnect handle request.</param>
        /// <returns>Returns CyberConnect handle.</returns>
        public Task<Result<string?>> HandleAsync(CyberConnectHandleRequest request);

        /// <summary>
        /// Get the CyberConnect profile data.
        /// </summary>
        /// <param name="request">CyberConnect profile request.</param>
        /// <returns>Returns CyberConnect profile data.</returns>
        public Task<Result<CyberConnectProfileData?>> ProfileDataAsync(CyberConnectProfileRequest request);

        /// <summary>
        /// Get the CyberConnect subscribings.
        /// </summary>
        /// <param name="request">CyberConnect subscribings request.</param>
        /// <returns>Returns CyberConnect subscribings.</returns>
        public Task<Result<IEnumerable<CyberConnectSubscribingProfileData>?>> SubscribingsAsync(CyberConnectSubscribingsRequest request);

        /// <summary>
        /// Get the CyberConnect likes.
        /// </summary>
        /// <param name="request">CyberConnect likes request.</param>
        /// <returns>Returns CyberConnect likes.</returns>
        public Task<Result<IEnumerable<CyberConnectLikeData>?>> LikesAsync(CyberConnectLikesRequest request);

        /// <summary>
        /// Get the CyberConnect essences.
        /// </summary>
        /// <param name="request">CyberConnect essences request.</param>
        /// <returns>Returns CyberConnect essences.</returns>
        public Task<Result<IEnumerable<CyberConnectEssenceData>?>> EssencesAsync(CyberConnectEssencesRequest request);
    }
}