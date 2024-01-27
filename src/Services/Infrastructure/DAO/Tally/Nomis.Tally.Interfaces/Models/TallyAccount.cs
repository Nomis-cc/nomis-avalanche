// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyAccount.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

// ReSharper disable InconsistentNaming
namespace Nomis.Tally.Interfaces.Models
{
    /// <summary>
    /// Tally account data.
    /// </summary>
    public class TallyAccount
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// EVM Address for this Account.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Ethereum Name Service Name.
        /// </summary>
        [JsonPropertyName("ens")]
        public string? ENS { get; set; }

        /// <summary>
        /// Twitter handle.
        /// </summary>
        [JsonPropertyName("twitter")]
        public string? Twitter { get; set; }

        /// <summary>
        /// Account name set on Tally.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Account bio set on Tally.
        /// </summary>
        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        /// <summary>
        /// Governances where an Account has a token balance or delegations along with Account Participation: votes, proposals, stats, delegations, etc.
        /// </summary>
        [JsonPropertyName("participations")]
        public IList<TallyParticipation> Participations { get; set; } = new List<TallyParticipation>();

        /// <summary>
        /// Picture URL.
        /// </summary>
        [JsonPropertyName("picture")]
        public string? Picture { get; set; }
    }
}