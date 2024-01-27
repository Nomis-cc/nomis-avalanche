// ------------------------------------------------------------------------------------------------------
// <copyright file="LogMaskedAttribute.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Attributes.Logging
{
    /// <summary>
    /// An attribute indicating that the property should be logged in masked form.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LogMaskedAttribute :
        Attribute
    {
        /// <summary>
        /// The character to mask by default.
        /// </summary>
        public const char DefaultMaskChar = '*';

        /// <summary>
        /// Initialize <see cref="LogMaskedAttribute"/>.
        /// </summary>
        /// <param name="maskChar">The symbol to mask.</param>
        /// <param name="showFirst">Show first n characters.</param>
        /// <param name="showLast">Show last n characters.</param>
        /// <param name="preserveLength">Keep the length of the original value.</param>
        public LogMaskedAttribute(
            char maskChar = DefaultMaskChar,
            int showFirst = 0,
            int showLast = 0,
            bool preserveLength = false)
        {
            MaskChar = maskChar;
            ShowFirst = showFirst;
            ShowLast = showLast;
            PreserveLength = preserveLength;
        }

        /// <summary>
        /// The symbol to mask.
        /// </summary>
        public char MaskChar { get; }

        /// <summary>
        /// Show first n characters.
        /// </summary>
        public int ShowFirst { get; set; }

        /// <summary>
        /// Show last n characters.
        /// </summary>
        public int ShowLast { get; set; }

        /// <summary>
        /// Keep the length of the original value.
        /// </summary>
        public bool PreserveLength { get; set; }

        /// <summary>
        /// Mask the property value.
        /// </summary>
        /// <param name="propValue">Property value.</param>
        /// <returns>Returns the masked property value.</returns>
        public object? MaskValue(object? propValue)
        {
            string? val = propValue as string;

            if (string.IsNullOrWhiteSpace(val))
            {
                return val;
            }

            if (ShowFirst == 0 && ShowLast == 0)
            {
                if (PreserveLength)
                {
                    return new string(MaskChar, val.Length);
                }

                return new string(MaskChar, 3);
            }

            if (ShowFirst > 0 && ShowLast == 0)
            {
                string first = val.Substring(0, Math.Min(ShowFirst, val.Length));

                if (!PreserveLength || !IsDefaultMask())
                {
                    return first + new string(MaskChar, 3);
                }

                string mask = string.Empty;
                if (ShowFirst <= val.Length)
                {
                    mask = new(MaskChar, val.Length - ShowFirst);
                }

                return first + mask;
            }

            if (ShowFirst == 0 && ShowLast > 0)
            {
#pragma warning disable IDE0057 // Use range operator
                string last = ShowLast > val.Length ? val : val.Substring(val.Length - ShowLast);
#pragma warning restore IDE0057 // Use range operator

                if (!PreserveLength || !IsDefaultMask())
                {
                    return new string(MaskChar, 3) + last;
                }

                string mask = string.Empty;
                if (ShowLast <= val.Length)
                {
                    mask = new(MaskChar, val.Length - ShowLast);
                }

                return mask + last;
            }

            if (ShowFirst > 0 && ShowLast > 0)
            {
                if (ShowFirst + ShowLast >= val.Length)
                {
                    return val;
                }

                string first = val.Substring(0, ShowFirst);
#pragma warning disable IDE0057 // Use range operator
                string last = val.Substring(val.Length - ShowLast);
#pragma warning restore IDE0057 // Use range operator

                if (!PreserveLength || !IsDefaultMask())
                {
                    return first + new string(MaskChar, 3) + last;
                }

                return first + new string(MaskChar, val.Length - ShowLast - ShowFirst) + last;
            }

            return propValue;
        }

        /// <summary>
        /// Check if the default mask character is used.
        /// If true PreserveLength is ignored.
        /// </summary>
        /// <remarks>
        /// If true, then <see cref="PreserveLength"/> does not apply.
        /// </remarks>
        /// <returns>Returns true if the default mask character is used. Otherwise - false.</returns>
        internal bool IsDefaultMask()
        {
            return MaskChar == DefaultMaskChar;
        }
    }
}