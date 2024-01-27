// ------------------------------------------------------------------------------------------------------
// <copyright file="ParametersDataForMigrationRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Requests
{
    /// <summary>
    /// Request for getting parameters data for migration.
    /// </summary>
    public class ParametersDataForMigrationRequest :
        IHasMintChain
    {
        /// <summary>
        /// Verifying deadline block timestamp.
        /// </summary>
        /// <example>1790647549</example>
        [FromQuery(Name = "deadline")]
        public ulong Deadline { get; set; }

        /// <summary>
        /// Scoring calculation model.
        /// </summary>
        /// <example>4</example>
        [FromQuery(Name = "calculationModel")]
        public virtual ScoringCalculationModel CalculationModel { get; set; } = ScoringCalculationModel.CommonV2;

        /// <inheritdoc />
        /// <example>137</example>
        [FromQuery(Name = "mintChain")]
        public virtual MintChain MintChain { get; set; } = MintChain.Polygon;

        /// <inheritdoc />
        /// <example>0</example>
        [FromQuery(Name = "mintBlockchainType")]
        public virtual MintChainType MintBlockchainType { get; set; } = MintChainType.Mainnet;

        /// <summary>
        /// Start block number.
        /// </summary>
        /// <example>0</example>
        [FromQuery(Name = "startBlockNumber")]
        public string StartBlockNumber { get; set; } = "0";

        /// <summary>
        /// Create referral and referrer codes if not exist.
        /// </summary>
        /// <example>false</example>
        [FromQuery(Name = "createCodesIfNotExist")]
        public bool CreateCodesIfNotExist { get; set; } = false;
    }
}