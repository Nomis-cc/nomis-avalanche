// ------------------------------------------------------------------------------------------------------
// <copyright file="SignatureResponseData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.SoulboundTokenService.Interfaces.Models
{
    /// <summary>
    /// Signature data in response.
    /// </summary>
    public class SignatureResponseData
    {
        /// <summary>
        /// R, used for signature verification.
        /// </summary>
        [JsonPropertyName("r")]
        public string? R { get; set; }

        /// <summary>
        /// S, used for signature verification.
        /// </summary>
        [JsonPropertyName("s")]
        public string? S { get; set; }
    }
}