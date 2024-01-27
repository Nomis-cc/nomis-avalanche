// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentChainsResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Covalent.Interfaces.Models;

namespace Nomis.Covalent.Interfaces.Responses
{
    /// <summary>
    /// Covalent chains response.
    /// </summary>
    public class CovalentChainsResponse
    {
        /// <summary>
        /// Chains data.
        /// </summary>
        [JsonPropertyName("data")]
        public CovalentChainsData? Data { get; set; }

        /// <summary>
        /// Is error.
        /// </summary>
        [JsonPropertyName("error")]
        public bool Error { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        [JsonPropertyName("error_message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Error code.
        /// </summary>
        [JsonPropertyName("error_code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ErrorCode { get; set; }
    }
}