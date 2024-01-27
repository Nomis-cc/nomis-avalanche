// ------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalContractData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Additional contract data.
    /// </summary>
    public class AdditionalContractData
    {
        /// <summary>
        /// Contract address.
        /// </summary>
        public string Address { get; set; } = null!;

        /// <summary>
        /// Block created.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ulong? BlockCreated { get; set; }
    }
}