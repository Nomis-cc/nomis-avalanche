// ------------------------------------------------------------------------------------------------------
// <copyright file="SoulboundTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.NFT;

namespace Nomis.SoulboundTokenService.Models
{
    /// <summary>
    /// Soulbound token data.
    /// </summary>
    public sealed class SoulboundTokenData :
        NFTCommonData
    {
        /// <summary>
        /// The contract signer wallet private key.
        /// </summary>
        public string? SignerWalletPrivateKey { get; set; }
    }
}