// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Discord.Bots.BackgroundServices;
using Nomis.Discord.Bots.Handlers;
using Nomis.Discord.Bots.Interfaces;
using Nomis.Discord.Bots.Settings;
using Nomis.NomisHolders.Interfaces;
using Nomis.Utils.Extensions;
using Nomis.Zealy.Interfaces;

namespace Nomis.Discord.Bots.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Discord Bots services.
        /// </summary>
        /// <param name="hostBuilder"><see cref="IHostBuilder"/>.</param>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IHostBuilder AddDiscordBotsServices(
            this IHostBuilder hostBuilder,
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<DiscordBotsSettings>(configuration);
            var settings = configuration.GetSettings<DiscordBotsSettings>();

            hostBuilder.ConfigureDiscordHost((_, config) =>
                {
                    config.SocketConfig = settings.SocketConfig;
                    config.Token = settings.BotToken;
                }).UseCommandService((_, config) =>
                {
                    config.CaseSensitiveCommands = settings.CommandConfig.CaseSensitiveCommands;
                    config.LogLevel = settings.CommandConfig.LogLevel;
                    config.DefaultRunMode = settings.CommandConfig.DefaultRunMode;
                })
                .UseInteractionService((_, config) =>
                {
                    config.LogLevel = settings.InteractionConfig.LogLevel;
                    config.UseCompiledLambda = settings.InteractionConfig.UseCompiledLambda;
                    config.DefaultRunMode = settings.InteractionConfig.DefaultRunMode;
                    config.ThrowOnError = settings.InteractionConfig.ThrowOnError;
                })
                .ConfigureServices((_, srv) =>
                {
                    srv
                        .AddSingleton<IScorerDiscordBotService, ScorerDiscordBotService>(provider =>
                        {
                            var logger = provider.GetRequiredService<ILogger<ScorerDiscordBotService>>();
                            var cacheProviderService = provider.GetRequiredService<ICacheProviderService>();
                            var discordSocketClient = provider.GetRequiredService<DiscordSocketClient>();
                            var zealyService = provider.GetRequiredService<IZealyService>();
                            var nomisHoldersService = provider.GetRequiredService<INomisHoldersService>();
                            return new ScorerDiscordBotService(
                                settings,
                                discordSocketClient,
                                nomisHoldersService,
                                zealyService,
                                cacheProviderService,
                                logger);
                        })
                        .AddHostedService<DiscordBotsInteractionHandler>()
                        .AddHostedService<DiscordBotsEventsBackgroundService>()
                        .AddHostedService<DiscordBotsCommandHandler>();
                });

            return hostBuilder;
        }
    }
}