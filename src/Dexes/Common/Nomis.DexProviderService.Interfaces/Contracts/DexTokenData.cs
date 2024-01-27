// ------------------------------------------------------------------------------------------------------
// <copyright file="DexTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces.Enums;

namespace Nomis.DexProviderService.Interfaces.Contracts
{
    /// <summary>
    /// DEX token data.
    /// </summary>
    public sealed class DexTokenData :
        TokenData
    {
        /// <summary>
        /// Token data provider id.
        /// </summary>
        public TokensProvider Provider { get; set; }

        /// <summary>
        /// Token data provider name.
        /// </summary>
        public string ProviderName => Provider.ToString();

        /// <summary>
        /// Blockchain id.
        /// </summary>
        public Chain? ChainId { get; set; }

        /// <summary>
        /// Last token data check.
        /// </summary>
        public DateTime LastCheck => DateTime.UtcNow;
    }
}