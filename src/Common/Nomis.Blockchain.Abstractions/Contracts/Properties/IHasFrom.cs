// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasFrom.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Properties
{
    /// <inheritdoc cref="IHasHash{TFrom}"/>
    public interface IHasFrom :
        IHasFrom<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="From"/>.
    /// </summary>
    /// <typeparam name="TFrom">The from address type.</typeparam>
    public interface IHasFrom<TFrom> :
        IHasProperty
    {
        /// <summary>
        /// From address.
        /// </summary>
        TFrom From { get; set; }
    }
}