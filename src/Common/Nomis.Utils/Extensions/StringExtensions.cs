// ------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;
using System.Text;

namespace Nomis.Utils.Extensions
{
    /// <summary>
    /// Extension methods for converting string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert string value to BigInteger value.
        /// </summary>
        /// <param name="stringValue">String value.</param>
        /// <returns>Returns total BigInteger value.</returns>
        public static BigInteger ToBigInteger(
            this string? stringValue)
        {
            return !BigInteger.TryParse(stringValue, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out var value) ? 0 : value;
        }

        /// <summary>
        /// Get a string in the specified encoding from the base64 string.
        /// </summary>
        /// <param name="str">Base64 string.</param>
        /// <param name="enc"><see cref="Encoding"/>.</param>
        /// <returns>Returns a string in the specified encoding from a base64 string.</returns>
        public static string FromBase64ToString(
            this string str,
            Encoding? enc = null)
        {
            return (enc ?? Encoding.Default).GetString(FromBase64(str));
        }

        /// <summary>
        /// Get an array of bytes from a base64 string.
        /// </summary>
        /// <param name="str">Base64 string.</param>
        /// <returns>Returns an array of bytes from a base64 string.</returns>
        public static byte[] FromBase64(
            this string str)
        {
            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// Get the base64 string from a string in the specified encoding.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <param name="enc"><see cref="Encoding"/>.</param>
        /// <returns>Returns a base64 string from a string in the specified encoding.</returns>
        public static string ToBase64(
            this string str,
            Encoding? enc = null)
        {
            return ToBase64((enc ?? Encoding.Default).GetBytes(str));
        }

        /// <summary>
        /// Get base64 string from byte array.
        /// </summary>
        /// <param name="data">An array of bytes.</param>
        /// <returns>Returns a base64 string from an array of bytes.</returns>
        public static string ToBase64(
            this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Get an array of bytes from a string in the specified encoding.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <param name="enc"><see cref="Encoding"/>.</param>
        /// <returns>Returns an array of bytes from a string in the specified encoding.</returns>
        public static byte[] ToByteArray(
            this string str,
            Encoding? enc = null)
        {
            return enc?.GetBytes(str) ?? Array.Empty<byte>();
        }
    }
}