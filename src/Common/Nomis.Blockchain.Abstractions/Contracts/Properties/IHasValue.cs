// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasValue.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Properties
{
    /// <inheritdoc cref="IHasHash{TValue}"/>
    public interface IHasValue :
        IHasValue<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="Value"/>.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface IHasValue<TValue> :
        IHasProperty
    {
        /// <summary>
        /// Value.
        /// </summary>
        TValue Value { get; set; }
    }
}