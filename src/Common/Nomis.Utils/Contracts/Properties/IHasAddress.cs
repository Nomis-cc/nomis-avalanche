// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasAddress.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasAddress{TAddress}"/>
    public interface IHasAddress :
        IHasAddress<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="Address"/>.
    /// </summary>
    /// <typeparam name="TAddress">The address type.</typeparam>
    public interface IHasAddress<out TAddress> :
        IHasProperty
    {
        /// <summary>
        /// Address.
        /// </summary>
        TAddress Address { get; }
    }
}