// ------------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotsCommandHandler.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.Discord.Bots.Settings;

namespace Nomis.Discord.Bots.Handlers
{
    /// <summary>
    /// Discord bots command handler.
    /// </summary>
    internal class DiscordBotsCommandHandler :
        DiscordClientService
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _provider;
        private readonly CommandService _service;
        private readonly DiscordBotsSettings _settings;

        /// <summary>
        /// Initialize <see cref="DiscordBotsCommandHandler"/>.
        /// </summary>
        /// <param name="client"><see cref="DiscordSocketClient"/>.</param>
        /// <param name="provider"><see cref="IServiceProvider"/>.</param>
        /// <param name="service"><see cref="CommandService"/>.</param>
        /// <param name="settings"><see cref="DiscordBotsSettings"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DiscordBotsCommandHandler(
            DiscordSocketClient client,
            IServiceProvider provider,
            CommandService service,
            IOptions<DiscordBotsSettings> settings,
            ILogger<DiscordClientService> logger)
            : base(client, logger)
        {
            _settings = settings.Value;
            _provider = provider;
            _service = service;
            _client = client;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider).ConfigureAwait(false);
        }

        private async Task OnCommandExecuted(
            Optional<CommandInfo> commandInfo,
            ICommandContext commandContext,
            IResult result)
        {
            Logger.LogInformation("User {user} attempted to use command {command}", commandContext.User, commandInfo.Value.Name);

            if (!commandInfo.IsSpecified || result.IsSuccess)
            {
                return;
            }

            await commandContext.Channel.SendMessageAsync(result.ErrorReason).ConfigureAwait(false);
        }

        private async Task OnMessageReceived(SocketMessage socketMsg)
        {
            if (socketMsg is not SocketUserMessage { Source: MessageSource.User } message)
            {
                return;
            }

            int argPos = 0;
            if (!message.HasStringPrefix(_settings.CommandsPrefix, ref argPos) &&
                !message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider).ConfigureAwait(false);
        }
    }
}