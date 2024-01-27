// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Token data.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.sushi.com/docs/Developers/Subgraphs/Exchange/Entities#token"/>
    /// </remarks>
    public class TokenData
    {
        /// <summary>
        /// Initialize <see cref="TokenData"/>.
        /// </summary>
        public TokenData()
        {
        }

        /// <summary>
        /// Initialize <see cref="TokenData"/>.
        /// </summary>
        /// <param name="tokenData"><see cref="TokenData"/>.</param>
        public TokenData(
            TokenData tokenData)
        {
            Id = tokenData.Id;
            Symbol = tokenData.Symbol;
            Name = tokenData.Name;
            Decimals = tokenData.Decimals;
            LogoUri = tokenData.LogoUri;
        }

        /// <summary>
        /// Token id in the blockchain.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        /// <summary>
        /// Token symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Symbol { get; set; }

        /// <summary>
        /// Token name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Token decimals.
        /// </summary>
        [JsonPropertyName("decimals")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Decimals { get; set; }

        /// <summary>
        /// URL of the token's logo image.
        /// </summary>
        [JsonPropertyName("logoUri")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LogoUri { get; set; }
    }
}