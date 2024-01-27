// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyBlock.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally block data.
    /// </summary>
    public class TallyBlock
    {
        /// <summary>
        /// Number.
        /// </summary>
        [JsonPropertyName("number")]
        public ulong Number { get; set; }

        /// <summary>
        /// Timestamp.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}