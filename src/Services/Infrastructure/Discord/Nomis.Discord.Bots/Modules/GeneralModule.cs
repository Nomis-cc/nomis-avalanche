// ------------------------------------------------------------------------------------------------------
// <copyright file="GeneralModule.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord.Commands;
using Nomis.Discord.Bots.Interfaces;

namespace Nomis.Discord.Bots.Modules
{
    /// <summary>
    /// General module.
    /// </summary>
    public class GeneralModule :
        ModuleBase<SocketCommandContext>
    {
        private readonly IScorerDiscordBotService _scorerDiscordBotService;

        /// <summary>
        /// Initialize <see cref="GeneralModule"/>.
        /// </summary>
        /// <param name="scorerDiscordBotService"><see cref="IScorerDiscordBotService"/>.</param>
        public GeneralModule(
            IScorerDiscordBotService scorerDiscordBotService)
        {
            _scorerDiscordBotService = scorerDiscordBotService;
        }

        [Command("nb-echo")]
        private async Task Echo(string msg)
        {
            await Context.Channel.SendMessageAsync(msg).ConfigureAwait(false);
        }
    }
}