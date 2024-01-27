// ------------------------------------------------------------------------------------------------------
// <copyright file="DIDData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;

using Nomis.PolygonId.Interfaces.PolygonIdIssuerNode;

// ReSharper disable InconsistentNaming
namespace Nomis.PolygonId.Interfaces.Contracts
{
    /// <summary>
    /// DID data.
    /// </summary>
    public sealed class DIDData
    {
        /// <summary>
        /// Initialize <see cref="DIDData"/>.
        /// </summary>
        /// <param name="request"><see cref="IWalletPolygonIdRequest"/>.</param>
        /// <param name="createdClaimId">Created claim id.</param>
        /// <param name="claimQrResponse"><see cref="GetClaimQrCodeResponse"/>.</param>
        public DIDData(
            IWalletPolygonIdRequest? request,
            string? createdClaimId,
            GetClaimQrCodeResponse? claimQrResponse)
        {
            DID = request?.DID;
            CreatedClaimId = createdClaimId;
            ClaimQR = claimQrResponse != null ? JsonSerializer.Serialize(claimQrResponse, new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) : null;
        }

        /// <summary>
        /// User DID.
        /// </summary>
        public string? DID { get; init; }

        /// <summary>
        /// Created claim id.
        /// </summary>
        public string? CreatedClaimId { get; init; }

        /// <summary>
        /// Claim QR-code string.
        /// </summary>
        public string? ClaimQR { get; init; }
    }
}