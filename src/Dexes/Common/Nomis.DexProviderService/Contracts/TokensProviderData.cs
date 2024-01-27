// ------------------------------------------------------------------------------------------------------
// <copyright file="TokensProviderData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces.Enums;

namespace Nomis.DexProviderService.Contracts
{
    /// <summary>
    /// Tokens provider data.
    /// </summary>
    public sealed class TokensProviderData
    {
        /// <summary>
        /// Is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Is the response a list of tokens data.
        /// </summary>
        public bool IsResponseList { get; set; }

        /// <summary>
        /// Urls.
        /// </summary>
        public IDictionary<Chain, string> Urls { get; set; } = new Dictionary<Chain, string>();

        /// <summary>
        /// Tokens provider.
        /// </summary>
        public TokensProvider Provider { get; set; }

        /// <summary>
        /// Default blockchain id.
        /// </summary>
        public Chain? DefaultChainId { get; set; }

        /// <summary>
        /// Blockchain id substitutions.
        /// </summary>
        public IDictionary<Chain, Chain>? ChainIdSubstitutions { get; set; }
    }
}