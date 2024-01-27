// ------------------------------------------------------------------------------------------------------
// <copyright file="IScoreSoulboundTokenService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.SoulboundTokenService.Interfaces.Models;
using Nomis.SoulboundTokenService.Interfaces.Requests;
using Nomis.Utils.Contracts.NFT;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Wrapper;

// ReSharper disable InconsistentNaming
namespace Nomis.SoulboundTokenService.Interfaces.Contracts
{
    /// <summary>
    /// Score soulbound token service.
    /// </summary>
    public interface IScoreSoulboundTokenService
    {
        /// <summary>
        /// Get the migration signature.
        /// </summary>
        /// <param name="request">The score migration request.</param>
        /// <returns>Return the migration signature.</returns>
        public Result<NFTSignature> GetMigrationSignature(
            ScoreMigrationRequest request);

        /// <summary>
        /// Get the score soulbound token signature.
        /// </summary>
        /// <param name="request">The score soulbound token request.</param>
        /// <returns>Return the score soulbound token signature.</returns>
        public Result<NFTSignature> GetSoulboundTokenSignature(
            ScoreSoulboundTokenRequest request);

        /// <summary>
        /// Get the score soulbound token image.
        /// </summary>
        /// <param name="request">The score soulbound token image request.</param>
        /// <returns>Return the score soulbound token image.</returns>
        public Task<Result<NFTImage>> GetSoulboundTokenImageAsync(
            ScoreSoulboundTokenImageRequest request);

        /// <summary>
        /// Get the score soulbound token metadata.
        /// </summary>
        /// <param name="request">The score soulbound token metadata request.</param>
        /// <returns>Return the score soulbound token metadata.</returns>
        public Task<Result<NFTMetadata>> GetSoulboundTokenMetadataAsync(
            NFTMetadataRequest request);

        /// <summary>
        /// Get the ONFT soulbound token metadata.
        /// </summary>
        /// <param name="request">The ONFT soulbound token metadata request.</param>
        /// <returns>Return the ONFT soulbound token metadata.</returns>
        public Task<Result<NFTMetadata>> GetONFTSoulboundTokenMetadataAsync(
            NFTMetadataRequest request);

        /// <summary>
        /// Get scoring calculation models.
        /// </summary>
        /// <returns>Returns scoring calculation models.</returns>
        public Task<Result<IList<ScoringCalculationModelData>>> GetScoringCalculationModelsAsync();
    }
}