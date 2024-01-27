// ------------------------------------------------------------------------------------------------------
// <copyright file="SignatureResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.SoulboundTokenService.Interfaces.Models;

namespace Nomis.SoulboundTokenService.Interfaces.Responses
{
    /// <summary>
    /// Signature response.
    /// </summary>
    public class SignatureResponse
    {
        /// <summary>
        /// Signature data in response.
        /// </summary>
        [JsonPropertyName("signature")]
        public SignatureResponseData? Signature { get; set; }

        /// <summary>
        /// Wallet public key, used for signature verification.
        /// </summary>
        [JsonPropertyName("publicKey")]
        public string? PublicKey { get; set; }
    }
}