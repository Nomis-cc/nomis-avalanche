// ------------------------------------------------------------------------------------------------------
// <copyright file="SlashCommandModule.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord;
using Discord.Interactions;
using Nomis.Discord.Bots.Interfaces;
using Nomis.Discord.Bots.Interfaces.Enums;

namespace Nomis.Discord.Bots.Modules
{
    /// <summary>
    /// Slash command module.
    /// </summary>
    public class SlashCommandModule :
        InteractionModuleBase
    {
        private readonly IScorerDiscordBotService _scorerDiscordBotService;

        /// <summary>
        /// Initialize <see cref="SlashCommandModule"/>.
        /// </summary>
        /// <param name="scorerDiscordBotService"><see cref="IScorerDiscordBotService"/>.</param>
        public SlashCommandModule(
            IScorerDiscordBotService scorerDiscordBotService)
        {
            _scorerDiscordBotService = scorerDiscordBotService;
        }

        [DefaultMemberPermissions(GuildPermission.ViewChannel)]
        [SlashCommand("nomis-minted-score", "Returns wallet's Nomis scores.")]
        private async Task CurrentScoreByScorerTypeAsync(
            [Summary(description: "Scored wallet address")] string wallet,
            [Summary(description: "Scorer")] ScorerType scorerType = ScorerType.Multichain)
        {
            if (string.IsNullOrWhiteSpace(wallet))
            {
                await Context.Channel.SendMessageAsync("Wrong wallet", flags: MessageFlags.Ephemeral).ConfigureAwait(false);
            }
            else
            {
                var scoreResult = await _scorerDiscordBotService.MintedScoreByScorerTypeAsync(wallet, scorerType).ConfigureAwait(false);
                if (scoreResult.Succeeded)
                {
                    await RespondAsync(text: scoreResult.Messages.FirstOrDefault(), embed: scoreResult.Data.Build(), ephemeral: true).ConfigureAwait(false);
                }
                else
                {
                    foreach (string message in scoreResult.Messages)
                    {
                        await RespondAsync(message, ephemeral: true).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}