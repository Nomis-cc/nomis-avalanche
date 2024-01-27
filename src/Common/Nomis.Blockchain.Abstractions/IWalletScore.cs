// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletScore.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.PolygonId.Interfaces.Contracts;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;

// ReSharper disable InconsistentNaming
namespace Nomis.Blockchain.Abstractions
{
    /// <inheritdoc cref="IWalletScore"/>
    /// <typeparam name="TWalletStats"><see cref="IWalletCommonStats{TTransactionIntervalData}"/>.</typeparam>
    /// <typeparam name="TTransactionIntervalData"><see cref="ITransactionIntervalData"/>.</typeparam>
    public interface IWalletScore<TWalletStats, TTransactionIntervalData> :
        IWalletScore
        where TWalletStats : IWalletCommonStats<TTransactionIntervalData>
        where TTransactionIntervalData : class, ITransactionIntervalData
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string? Address { get; init; }

        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; init; }

        /// <inheritdoc cref="Utils.Enums.ScoreType"/>
        public ScoreType ScoreType { get; }

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
        public TWalletStats? Stats { get; init; }
    }

    /// <summary>
    /// Wallet score.
    /// </summary>
    public interface IWalletScore
    {
    }
}