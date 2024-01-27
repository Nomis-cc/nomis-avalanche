// ------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

namespace Nomis.Utils.Extensions
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Get median value from array.
        /// </summary>
        /// <typeparam name="T">The type of array elements.</typeparam>
        /// <param name="items">Array of elements.</param>
        /// <returns>Returns median value from array.</returns>
        public static T? Median<T>(
            this IEnumerable<T> items)
            where T : IAdditionOperators<T, T, T>, IDivisionOperators<T, decimal, T>
        {
            var itemsList = items.ToList();
            if (itemsList.Count == 0)
            {
                return default;
            }

            int i = (int)Math.Ceiling((double)(itemsList.Count - 1) / 2);
            if (i <= 0)
            {
                return default;
            }

            itemsList.Sort();
            if (itemsList.Count % 2 == 0)
            {
                return (itemsList[i] + itemsList[i - 1]) / 2;
            }

            return itemsList[i];
        }
    }
}