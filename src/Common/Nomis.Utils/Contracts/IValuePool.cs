// ------------------------------------------------------------------------------------------------------
// <copyright file="IValuePool.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Contracts
{
    /// <summary>
    /// Value pool.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IValuePool<out TValue>
        where TValue : class
    {
        /// <summary>
        /// Get next value from pool.
        /// </summary>
        public TValue GetNextValue();
    }

    /// <inheritdoc cref="IValuePool{TValue}"/>
    /// <typeparam name="TService">The service type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IValuePool<TService, out TValue> :
        IValuePool<TValue>
        where TService : class
        where TValue : class
    {
        /// <summary>
        /// Get current value index.
        /// </summary>
        public int CurrentIndex { get; }
    }
}