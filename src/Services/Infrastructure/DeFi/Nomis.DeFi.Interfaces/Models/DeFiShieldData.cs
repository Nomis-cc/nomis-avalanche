// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiShieldData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DeFi.Interfaces.Models
{
    /// <summary>
    /// De.Fi shield data.
    /// </summary>
    public class DeFiShieldData
    {
        /// <summary>
        /// Internal project id.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Contract name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Contract logo url.
        /// </summary>
        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        /// <summary>
        /// Contract is scanning or updating results. if it's first scan, no more data will be available.
        /// </summary>
        [JsonPropertyName("inProgress")]
        public bool InProgress { get; set; }

        /// <summary>
        /// Smart contract is whitelisted by De.Fi.
        /// </summary>
        [JsonPropertyName("whitelisted")]
        public bool Whitelisted { get; set; }

        /// <summary>
        /// Shield contract tags.
        /// </summary>
        /// <remarks>
        /// Blockscan tags of contract.
        /// </remarks>
        [JsonPropertyName("tags")]
        public IDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Contract vulnerabilities list.
        /// </summary>
        [JsonPropertyName("issues")]
        public IList<DeFiIssueData> Issues { get; set; } = new List<DeFiIssueData>();
    }
}