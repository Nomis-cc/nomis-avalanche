// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasTo.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Properties
{
    /// <inheritdoc cref="IHasHash{TTo}"/>
    public interface IHasTo :
        IHasTo<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="To"/>.
    /// </summary>
    /// <typeparam name="TTo">The to address type.</typeparam>
    public interface IHasTo<TTo> :
        IHasProperty
    {
        /// <summary>
        /// To address.
        /// </summary>
        TTo To { get; set; }
    }
}