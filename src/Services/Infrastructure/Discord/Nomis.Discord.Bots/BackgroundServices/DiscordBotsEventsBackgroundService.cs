// ------------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotsEventsBackgroundService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.Discord.Bots.Settings;

namespace Nomis.Discord.Bots.BackgroundServices
{
    /// <summary>
    /// Discord bots events background service.
    /// </summary>
    internal class DiscordBotsEventsBackgroundService :
        BackgroundService
    {
        private readonly DiscordSocketClient _client;
        private readonly ILogger<DiscordBotsEventsBackgroundService> _logger;
        private readonly DiscordBotsSettings _settings;

        public DiscordBotsEventsBackgroundService(
            IOptions<DiscordBotsSettings> settings,
            DiscordSocketClient client,
            ILogger<DiscordBotsEventsBackgroundService> logger)
        {
            _settings = settings.Value;
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (_settings is { IsEnabled: true, EventsIdEnabled: true })
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Discord bots events background service is stopping");
                    break;
                }

                await Task.Delay((int)_settings.EventsDelay.TotalMilliseconds, stoppingToken).ConfigureAwait(false);

                // _logger.LogInformation("Start next events iteration");

                // TODO - use for posting leader boards and other info
                /*var whalesBuys = _magicEdenService.GetWhalesBuys();
                await foreach (var (messages, channelIds) in whalesBuys.WithCancellation(stoppingToken))
                {
                    if (messages?.Any() == true && channelIds?.Any() == true)
                    {
                        foreach (var channelId in channelIds)
                        {
                            if (_client.GetChannel(channelId) is ITextChannel channel)
                            {
                                foreach (var whaleBuy in messages)
                                {
                                    await channel.SendMessageAsync(embed: whaleBuy.Build());
                                }
                            }
                        }
                    }
                }*/
            }
        }
    }
}