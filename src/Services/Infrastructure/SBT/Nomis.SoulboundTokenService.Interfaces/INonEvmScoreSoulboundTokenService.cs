// ------------------------------------------------------------------------------------------------------
// <copyright file="INonEvmScoreSoulboundTokenService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.SoulboundTokenService.Interfaces.Contracts;
using Nomis.SoulboundTokenService.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Responses;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.SoulboundTokenService.Interfaces
{
    /// <summary>
    /// Non EVM score soulbound token service.
    /// </summary>
    public interface INonEvmScoreSoulboundTokenService :
        IScoreSoulboundTokenService,
        IInfrastructureService
    {
        /// <summary>
        /// Get the score soulbound token signature for non-EVM blockchain.
        /// </summary>
        /// <param name="request">The score soulbound token request.</param>
        /// <returns>Returns the score soulbound token signature for non-EVM blockchain.</returns>
        public Task<Result<SignatureResponse?>> GetNonEvmSoulboundTokenSignatureAsync(
            ScoreSoulboundTokenRequest request);
    }
}