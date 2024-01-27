// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheHelpers.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

using Nomis.Blockchain.Abstractions.Contracts.Models;

namespace Nomis.Snowtrace.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods for Avalanche.
    /// </summary>
    public static class AvalancheHelpers
    {
        /// <summary>
        /// Convert Wei value to AVAX.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total AVAX.</returns>
        public static decimal ToNative(this in BigInteger valueInWei)
        {
            if (valueInWei > new BigInteger(decimal.MaxValue))
            {
                return (decimal)(valueInWei / new BigInteger(100_000_000_000_000_000));
            }

            return (decimal)valueInWei * 0.000_000_000_000_000_001M;
        }

        /// <summary>
        /// Convert native value to wei.
        /// </summary>
        /// <param name="value">Native value.</param>
        /// <returns>Returns total wei.</returns>
        public static BigInteger FromNative(this in decimal value)
        {
            if (value > decimal.MaxValue / 100_000_000_000_000_000)
            {
                return (BigInteger)(value * new decimal(100_000_000_000_000_000));
            }

            return (BigInteger)(value / 0.000_000_000_000_000_001M);
        }

        /// <summary>
        /// Convert Wei value to AVAX.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total AVAX.</returns>
        public static decimal ToAvax(this string valueInWei)
        {
            return BigInteger
                .TryParse(valueInWei, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out var value)
                ? value.ToNative()
                : 0;
        }

        /// <summary>
        /// Convert Wei value to AVAX.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total AVAX.</returns>
        public static decimal ToAvax(this decimal valueInWei)
        {
            return new BigInteger(valueInWei).ToNative();
        }

        /// <summary>
        /// Get token UID based on it ContractAddress and Id.
        /// </summary>
        /// <param name="token">Token info.</param>
        /// <returns>Returns token UID.</returns>
        public static string GetTokenUid(this INFTTokenTransfer token)
        {
            return $"{token.ContractAddress}_{token.TokenId}";
        }
    }
}