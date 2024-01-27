// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentChainsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent chains data.
    /// </summary>
    public class CovalentChainsData
    {
        /// <summary>
        /// Updated at.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Blockchain data list.
        /// </summary>
        [JsonPropertyName("items")]
        public IList<CovalentChainData> Chains { get; set; } = new List<CovalentChainData>();
    }
}