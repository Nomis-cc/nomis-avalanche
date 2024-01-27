// ------------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotsInteractionHandler.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Discord;
using Discord.Addons.Hosting;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Nomis.Discord.Bots.Converters;

namespace Nomis.Discord.Bots.Handlers
{
    /// <summary>
    /// Discord bots interaction handler.
    /// </summary>
    internal class DiscordBotsInteractionHandler :
        DiscordClientService
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _interaction;
        private readonly IServiceProvider _provider;

        /// <summary>
        /// Initialize <see cref="DiscordBotsInteractionHandler"/>.
        /// </summary>
        /// <param name="client"><see cref="DiscordSocketClient"/>.</param>
        /// <param name="interaction"><see cref="InteractionService"/>.</param>
        /// <param name="provider"><see cref="IServiceProvider"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DiscordBotsInteractionHandler(
            DiscordSocketClient client,
            InteractionService interaction,
            IServiceProvider provider,
            ILogger<DiscordClientService> logger)
            : base(client, logger)
        {
            _client = client;
            _interaction = interaction;
            _interaction.AddTypeConverter<string[]>(new StringArrayConverter());
            _provider = provider;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            await _interaction.AddModulesAsync(Assembly.GetExecutingAssembly(), _provider).ConfigureAwait(false);
            Client.Ready += RegisterCommandsAsync;
            Client.InteractionCreated += HandleInteractionAsync;
        }

        private async Task HandleInteractionAsync(SocketInteraction socketInteraction)
        {
            try
            {
                var context = new SocketInteractionContext(Client, socketInteraction);
                await _interaction.ExecuteCommandAsync(context, _provider).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Exception while attempting to handle interaction.");

                if (socketInteraction.Type == InteractionType.ApplicationCommand)
                {
                    var message = await socketInteraction.GetOriginalResponseAsync().ConfigureAwait(false);

                    await message.DeleteAsync().ConfigureAwait(false);
                }
            }
        }

        private async Task RegisterCommandsAsync()
        {
            await _interaction.RegisterCommandsGloballyAsync().ConfigureAwait(false);
        }
    }
}