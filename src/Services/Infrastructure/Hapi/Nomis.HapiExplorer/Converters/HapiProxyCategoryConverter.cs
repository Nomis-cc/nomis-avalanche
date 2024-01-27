// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyCategoryConverter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

using Nomis.HapiExplorer.Interfaces.Enums;

namespace Nomis.HapiExplorer.Converters
{
    internal sealed class HapiProxyCategoryConverter :
        JsonConverter<HapiProxyCategory>
    {
        /// <inheritdoc/>
        public override HapiProxyCategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Found token {reader.TokenType} but expected token {JsonTokenType.Number}");
            }

            using var doc = JsonDocument.ParseValue(ref reader);
            return Enum.Parse<HapiProxyCategory>(doc.RootElement.ToString());
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, HapiProxyCategory value, JsonSerializerOptions options) =>
            writer.WriteRawValue(value.ToString(), false);
    }
}