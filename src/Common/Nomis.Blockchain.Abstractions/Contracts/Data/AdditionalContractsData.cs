// ------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalContractsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Additional contracts data.
    /// </summary>
    public class AdditionalContractsData
    {
        /// <summary>
        /// Contracts data.
        /// </summary>
        public IDictionary<string, AdditionalContractData> Contracts { get; set; } = new Dictionary<string, AdditionalContractData>();
    }
}