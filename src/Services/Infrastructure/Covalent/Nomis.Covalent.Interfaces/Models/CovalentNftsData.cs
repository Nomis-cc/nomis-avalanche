// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentNftsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Covalent.Interfaces.Models
{
    /// <summary>
    /// Covalent NFTs data.
    /// </summary>
    public class CovalentNftsData
    {
        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// Updated at.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Nft data list.
        /// </summary>
        [JsonPropertyName("items")]
        public IList<CovalentNftData> Nfts { get; set; } = new List<CovalentNftData>();
    }
}