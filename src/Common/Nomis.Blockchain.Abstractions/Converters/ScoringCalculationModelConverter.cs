// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoringCalculationModelConverter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

using Nomis.Utils.Enums;

namespace Nomis.Blockchain.Abstractions.Converters
{
    /// <summary>
    /// Scoring calculation model converter.
    /// </summary>
    public sealed class ScoringCalculationModelConverter :
        JsonConverter<ScoringCalculationModel>
    {
        /// <inheritdoc/>
        public override ScoringCalculationModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException($"Found token {reader.TokenType} but expected token {JsonTokenType.Number}");
            }

            using var doc = JsonDocument.ParseValue(ref reader);
            if (Enum.TryParse<ScoringCalculationModel>(doc.RootElement.ToString(), true, out var scoringCalculationModel))
            {
                return scoringCalculationModel;
            }

            return ScoringCalculationModel.CommonV2;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ScoringCalculationModel value, JsonSerializerOptions options) =>
            writer.WriteRawValue(value.ToString(), false);
    }
}