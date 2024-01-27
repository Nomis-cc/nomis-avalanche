// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenListTokens.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.DexProviderService.Contracts
{
    /// <summary>
    /// Token list tokens.
    /// </summary>
    internal sealed class TokenListTokens
    {
        /// <summary>
        /// Tokens.
        /// </summary>
        [JsonPropertyName("tokens")]
        public IList<TokenListTokenData> Tokens { get; set; } = new List<TokenListTokenData>();
    }
}