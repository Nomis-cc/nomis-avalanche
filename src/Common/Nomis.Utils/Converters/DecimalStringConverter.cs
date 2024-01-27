// ------------------------------------------------------------------------------------------------------
// <copyright file="DecimalStringConverter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nomis.Utils.Converters
{
    /// <summary>
    /// <see cref="decimal"/> converter.
    /// </summary>
    public sealed class DecimalStringConverter :
        JsonConverter<decimal>
    {
        /// <inheritdoc />
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && decimal.TryParse(reader.GetString(), NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." }, out decimal value))
            {
                return value;
            }

            try
            {
                return reader.GetDecimal();
            }
            catch
            {
                return 0M;
            }
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(NumberFormatInfo.InvariantInfo));
        }
    }
}