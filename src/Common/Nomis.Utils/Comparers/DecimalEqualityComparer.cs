// ------------------------------------------------------------------------------------------------------
// <copyright file="DecimalEqualityComparer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Comparers
{
    /// <summary>
    /// Decimal equality comparer.
    /// </summary>
    public class DecimalEqualityComparer :
        IEqualityComparer<decimal>
    {
        private readonly decimal _precision;

        /// <summary>
        /// Initialize <see cref="DecimalEqualityComparer"/>.
        /// </summary>
        /// <param name="precision">Precision.</param>
        public DecimalEqualityComparer(
            decimal precision)
        {
            _precision = precision;
        }

        /// <inheritdoc />
        public bool Equals(decimal x, decimal y)
        {
            return Math.Abs(x - y) < _precision;
        }

        /// <inheritdoc />
        public int GetHashCode(decimal obj)
        {
            return obj.GetHashCode();
        }
    }
}