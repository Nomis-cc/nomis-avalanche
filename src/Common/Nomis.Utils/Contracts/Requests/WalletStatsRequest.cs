// ------------------------------------------------------------------------------------------------------
// <copyright file="WalletStatsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Properties;
using Nomis.Utils.Enums;

namespace Nomis.Utils.Contracts.Requests
{
    /// <summary>
    /// Request for getting the wallet stats.
    /// </summary>
    public class WalletStatsRequest :
        IHasAddress,
        IHasMintChain
    {
        private string _address = string.Empty;
        private string? _tokenAddress;

        /// <summary>
        /// Wallet address.
        /// </summary>
        [FromRoute(Name = "address")]
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value.Trim();
            }
        }

        /// <summary>
        /// Contract nonce.
        /// </summary>
        /// <remarks>
        /// Should be get from the contract.
        /// </remarks>
        /// <example>0</example>
        [FromQuery(Name = "nonce")]
        public virtual ulong Nonce { get; set; }

        /// <summary>
        /// Verifying deadline block timestamp.
        /// </summary>
        /// <example>1790647549</example>
        [FromQuery(Name = "deadline")]
        public virtual ulong Deadline { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        /// <example>11</example>
        [FromQuery(Name = "calculationModel")]
        public virtual ScoringCalculationModel CalculationModel { get; set; } = ScoringCalculationModel.CommonV3;

        /// <summary>
        /// Score type.
        /// </summary>
        /// <example>0</example>
        [FromQuery(Name = "scoreType")]
        public virtual ScoreType ScoreType { get; set; } = ScoreType.Finance;

        /// <summary>
        /// Token contract address.
        /// </summary>
        /// <remarks>
        /// If `ScoreType = 1` and set, scoring calculate for this token.
        /// </remarks>
        /// <example>null</example>
        [FromQuery(Name = "tokenAddress")]
        public virtual string? TokenAddress
        {
            get
            {
                return _tokenAddress;
            }

            set
            {
                _tokenAddress = value?.Trim();
            }
        }

        /// <summary>
        /// Prepare data to mint.
        /// </summary>
        /// <example>false</example>
        [FromQuery(Name = "prepareToMint")]
        public virtual bool PrepareToMint { get; set; } = false;

        /// <summary>
        /// Referrer code from another wallet.
        /// </summary>
        /// <remarks>
        /// If set, the reward will be distributed for the wallet that owns the referrer code.
        /// </remarks>
        public virtual string? ReferrerCode { get; set; }

        /// <inheritdoc />
        /// <example>0</example>
        [FromQuery]
        public virtual MintChain MintChain { get; set; } = MintChain.Native;

        /// <inheritdoc />
        /// <example>0</example>
        [FromQuery(Name = "mintBlockchainType")]
        public virtual MintChainType MintBlockchainType { get; set; } = MintChainType.Mainnet;

        /// <summary>
        /// Use Defillama for unknown tokens.
        /// </summary>
        /// <example>true</example>
        [BindNever]
        public virtual bool UseDefillamaForUnknownTokens { get; set; } = true;

        /// <summary>
        /// Disable cache.
        /// </summary>
        /// <example>false</example>
        [FromHeader(Name = "disableCache")]
        public virtual bool DisableCache { get; set; } = false;

        /// <summary>
        /// Disable proxy.
        /// </summary>
        /// <example>false</example>
        [FromHeader(Name = "disableProxy")]
        public virtual bool DisableProxy { get; set; } = false;
    }
}