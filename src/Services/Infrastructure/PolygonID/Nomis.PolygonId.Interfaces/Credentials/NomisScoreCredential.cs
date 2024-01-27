// ------------------------------------------------------------------------------------------------------
// <copyright file="NomisScoreCredential.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace Nomis.PolygonId.Interfaces.Credentials
{
    /// <summary>
    /// Nomis score credential.
    /// </summary>
    public class NomisScoreCredential
    {
        /// <summary>
        /// User DID.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Wallet Nomis Score.
        /// </summary>
        [JsonPropertyName("score")]
        [JsonProperty("score")]
        public ushort Score { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("calculationModel")]
        [JsonProperty("calculationModel")]
        public int CalculationModel { get; set; } = 0;

        /// <summary>
        /// Blockchain in which the score will be minted.
        /// </summary>
        /// <example>137</example>
        [JsonPropertyName("mintChain")]
        [JsonProperty("mintChain")]
        public ulong MintChain { get; set; } = 137;

        /// <summary>
        /// Blockchains in which the score will be minted for multichain score.
        /// </summary>
        /// <example>1,56,137</example>
        [JsonPropertyName("multichainBlockchains")]
        [JsonProperty("multichainBlockchains")]
        public string? MultichainBlockchains { get; set; }

        /// <summary>
        /// Score type.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("scoreType")]
        [JsonProperty("scoreType")]
        public int ScoreType { get; set; } = 0;

        /// <summary>
        /// Blockchain in which the score will be calculated.
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("scoredChain")]
        [JsonProperty("scoredChain")]
        public ulong ScoredChain { get; set; }
    }
}