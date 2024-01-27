// ------------------------------------------------------------------------------------------------------
// <copyright file="StringArrayConverter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord;
using Discord.Interactions;

namespace Nomis.Discord.Bots.Converters
{
    /// <summary>
    /// String array converter.
    /// </summary>
    internal class StringArrayConverter :
        TypeConverter<string[]>
    {
        public override ApplicationCommandOptionType GetDiscordType()
        {
            return ApplicationCommandOptionType.String;
        }

        public override Task<TypeConverterResult> ReadAsync(
            IInteractionContext context,
            IApplicationCommandInteractionDataOption option,
            IServiceProvider services)
        {
            string? value = (string)option.Value;
            value = value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase);
            if (value.Contains(',', StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(TypeConverterResult.FromSuccess(value.Split(',').Select(x => x.Trim()).ToArray()));
            }

            if (value.Contains(';', StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(TypeConverterResult.FromSuccess(value.Split(';').Select(x => x.Trim()).ToArray()));
            }

            if (value.Contains('.', StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(TypeConverterResult.FromSuccess(value.Split('.').Select(x => x.Trim()).ToArray()));
            }

            return Task.FromResult(TypeConverterResult.FromSuccess(value.Split().Select(x => x.Trim()).ToArray()));
        }
    }
}