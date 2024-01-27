// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasHash.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Properties
{
    /// <inheritdoc cref="IHasHash{THash}"/>
    public interface IHasHash :
        IHasHash<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="Hash"/>.
    /// </summary>
    /// <typeparam name="THash">The hash type.</typeparam>
    public interface IHasHash<THash> :
        IHasProperty
    {
        /// <summary>
        /// Hash.
        /// </summary>
        THash Hash { get; set; }
    }
}