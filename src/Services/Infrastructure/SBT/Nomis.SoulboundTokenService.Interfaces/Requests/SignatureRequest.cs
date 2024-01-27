// ------------------------------------------------------------------------------------------------------
// <copyright file="SignatureRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.SoulboundTokenService.Interfaces.Models;

namespace Nomis.SoulboundTokenService.Interfaces.Requests
{
    /// <summary>
    /// Signature request.
    /// </summary>
    public class SignatureRequest
    {
        /// <summary>
        /// Signature data.
        /// </summary>
        [JsonPropertyName("data")]
        public SignatureData Data { get; set; } = null!;
    }
}