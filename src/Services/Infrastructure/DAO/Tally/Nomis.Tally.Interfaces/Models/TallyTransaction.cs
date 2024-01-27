// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally transaction data.
    /// </summary>
    public class TallyTransaction
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Block vote was cast in.
        /// </summary>
        [JsonPropertyName("block")]
        public TallyBlock? Block { get; set; }
    }
}