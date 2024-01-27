// ------------------------------------------------------------------------------------------------------
// <copyright file="TransferTokenDataBalance.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Transfer token data balance.
    /// </summary>
    public class TransferTokenDataBalance :
        TokenDataBalance
    {
        /// <summary>
        /// Transfer token transaction hash.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TransactionHash { get; set; }

        /// <summary>
        /// Is outcome transfer.
        /// </summary>
        public bool IsOutcome { get; set; }

        /// <summary>
        /// Invocation type.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? InvocationType { get; set; }
    }
}