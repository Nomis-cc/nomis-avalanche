// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasTimestamp.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Properties
{
    /// <inheritdoc cref="IHasTimestamp{TTimestamp}"/>
    public interface IHasTimestamp :
        IHasTimestamp<string?>
    {
    }

    /// <summary>
    /// Has property with name <see cref="Timestamp"/>.
    /// </summary>
    /// <typeparam name="TTimestamp">The contract timestamp.</typeparam>
    public interface IHasTimestamp<TTimestamp> :
        IHasProperty
    {
        /// <summary>
        /// Timestamp.
        /// </summary>
        TTimestamp Timestamp { get; set; }
    }
}