// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectEssenceCreator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.CyberConnect.Interfaces.Models
{
    /// <summary>
    /// CyberConnect essence creator data.
    /// </summary>
    public class CyberConnectEssenceCreator
    {
        /// <summary>
        /// Profile Id.
        /// </summary>
        [JsonPropertyName("profileID")]
        public int ProfileId { get; set; }

        /// <summary>
        /// Handle.
        /// </summary>
        [JsonPropertyName("handle")]
        public string? Handle { get; set; }
    }
}