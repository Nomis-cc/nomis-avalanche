// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmAccount.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// Account data.
    /// </summary>
    public class BaseEvmAccount
    {
        /// <summary>
        /// Status.
        /// </summary>
        [JsonPropertyName("status")]
        public virtual int Status { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        [JsonPropertyName("message")]
        public virtual string? Message { get; set; }

        /// <summary>
        /// Balance.
        /// </summary>
        [JsonPropertyName("result")]
        public virtual string? Balance { get; set; }
    }
}