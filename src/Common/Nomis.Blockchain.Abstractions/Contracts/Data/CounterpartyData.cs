// ------------------------------------------------------------------------------------------------------
// <copyright file="CounterpartyData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Counterparty data.
    /// </summary>
    public class CounterpartyData
    {
        /// <summary>
        /// Initialize <see cref="CounterpartyData"/>.
        /// </summary>
        public CounterpartyData()
        {
        }

        /// <summary>
        /// Use counterparty.
        /// </summary>
        public virtual bool UseCounterparty { get; init; }

        /// <summary>
        /// Is smart-contract ONFT.
        /// </summary>
        public virtual bool IsONFT { get; init; }

        /// <summary>
        /// Is socket smart-contract.
        /// </summary>
        public virtual bool IsSocket { get; init; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// Smart-contract name.
        /// </summary>
        public string? ContractName { get; init; }

        /// <summary>
        /// Description.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Smart-contract address.
        /// </summary>
        public string ContractAddress { get; init; } = null!;

        /// <summary>
        /// Smart-contract methods.
        /// </summary>
        public virtual IList<string> Methods { get; init; } = new List<string>();
    }
}