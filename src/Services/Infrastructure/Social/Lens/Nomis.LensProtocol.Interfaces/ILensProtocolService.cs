// ------------------------------------------------------------------------------------------------------
// <copyright file="ILensProtocolService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.LensProtocol.Interfaces.Models;
using Nomis.LensProtocol.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.LensProtocol.Interfaces
{
    /// <summary>
    /// Service for interaction with LensProtocol API.
    /// </summary>
    public interface ILensProtocolService :
        IInfrastructureService
    {
        /// <summary>
        /// Get the Lens Protocol profile data.
        /// </summary>
        /// <param name="request">Lens Protocol profile request.</param>
        /// <returns>Returns Lens Protocol profile data.</returns>
        public Task<Result<LensProtocolProfileData?>> ProfileDataAsync(
            LensProtocolProfileRequest request);
    }
}