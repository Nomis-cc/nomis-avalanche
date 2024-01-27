// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheWalletScore.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.PolygonId.Interfaces.Contracts;
using Nomis.Utils.Enums;

namespace Nomis.Snowtrace.Interfaces.Models
{
    /// <summary>
    /// Avalanche wallet score.
    /// </summary>
    public class AvalancheWalletScore :
        IWalletScore<AvalancheWalletStats, AvalancheTransactionIntervalData>
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string? Address { get; init; }

        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; init; }

        /// <summary>
        /// Score type.
        /// </summary>
        public ScoreType ScoreType => ScoreType.Finance;

        /// <summary>
        /// Migrate data.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MigrationData? MigrationData { get; init; }

        /// <summary>
        /// Mint data.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MintData? MintData { get; init; }

        /// <summary>
        /// DID data.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DIDData? DIDData { get; init; }

        /// <summary>
        /// Referral code.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReferralCode { get; init; }

        /// <summary>
        /// Referrer code.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReferrerCode { get; init; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public AvalancheWalletStats? Stats { get; init; }
    }
}