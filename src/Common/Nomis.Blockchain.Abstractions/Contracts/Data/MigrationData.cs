// ------------------------------------------------------------------------------------------------------
// <copyright file="MigrationData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Migration data.
    /// </summary>
    public sealed class MigrationData
    {
        /// <summary>
        /// Initialize <see cref="MigrationData"/>.
        /// </summary>
        /// <param name="blockNumber">Block number.</param>
        /// <param name="tokenId">Token id.</param>
        /// <param name="signature">Migrate signature.</param>
        /// <param name="deadline">Verifying deadline block timestamp.</param>
        /// <param name="address">Wallet address.</param>
        /// <param name="referralCode">Referral code.</param>
        /// <param name="referrerCode">Referrer code.</param>
        public MigrationData(
            string? blockNumber,
            string? tokenId,
            string? signature,
            ulong deadline,
            string address,
            string referralCode = "anon",
            string referrerCode = "nomis")
        {
            BlockNumber = blockNumber;
            TokenId = tokenId;
            Signature = signature;
            Deadline = deadline;
            Address = address;
            ReferralCode = referralCode;
            ReferrerCode = referrerCode;
        }

        /// <summary>
        /// Block number.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BlockNumber { get; init; }

        /// <summary>
        /// Token id.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TokenId { get; init; }

        /// <summary>
        /// Migrate signature.
        /// </summary>
        public string? Signature { get; init; }

        /// <summary>
        /// Verifying deadline block timestamp.
        /// </summary>
        public ulong Deadline { get; init; }

        /// <summary>
        /// Wallet address.
        /// </summary>
        public string? Address { get; init; }

        /// <summary>
        /// Referral code.
        /// </summary>
        public string? ReferralCode { get; init; }

        /// <summary>
        /// Referrer code.
        /// </summary>
        public string? ReferrerCode { get; init; }
    }
}