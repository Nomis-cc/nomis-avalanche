// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasContractAddress.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Properties
{
    /// <inheritdoc cref="IHasContractAddress{TContractAddress}"/>
    public interface IHasContractAddress :
        IHasContractAddress<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="ContractAddress"/>.
    /// </summary>
    /// <typeparam name="TContractAddress">The contract address type.</typeparam>
    public interface IHasContractAddress<
        TContractAddress> :
        IHasProperty
    {
        /// <summary>
        /// Contract address.
        /// </summary>
        TContractAddress ContractAddress { get; set; }
    }
}