// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenListTokenDataChainConverter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

using Nomis.Dex.Abstractions.Enums;

namespace Nomis.DexProviderService.Converters
{
    internal sealed class TokenListTokenDataChainConverter :
        JsonConverter<Chain?>
    {
        /// <inheritdoc/>
        public override Chain? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException($"Found token {reader.TokenType} but expected token {JsonTokenType.Number}");
            }

            using var doc = JsonDocument.ParseValue(ref reader);
            if (Enum.TryParse<Chain>(doc.RootElement.ToString(), true, out var chain))
            {
                return chain;
            }

            return null;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Chain? value, JsonSerializerOptions options) =>
            writer.WriteRawValue(value?.ToString() ?? "None", false);
    }
}