// ------------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotsSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Nomis.Discord.Bots.Interfaces.Enums;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Discord.Bots.Settings
{
    /// <summary>
    /// Discord bots settings.
    /// </summary>
    internal class DiscordBotsSettings :
        ISettings
    {
        /// <summary>
        /// Discord bot is enabled.
        /// </summary>
        public bool IsEnabled { get; init; }

        /// <summary>
        /// Discord bot token.
        /// </summary>
        public string BotToken { get; init; } = null!;

        /// <summary>
        /// Bot commands prefix.
        /// </summary>
        public string CommandsPrefix { get; init; } = null!;

        /// <summary>
        /// Score image base URL.
        /// </summary>
        public string ScoreImageBaseUrl { get; init; } = null!;

        /// <summary>
        /// Discord channel send delay.
        /// </summary>
        public TimeSpan SendChannelDelay { get; init; }

        /// <summary>
        /// Excluded scorers for sending to discord channel.
        /// </summary>
        public IList<ScorerType> ExcludedScorers { get; init; } = new List<ScorerType>();

        /// <summary>
        /// Channel id for posting calculated scores.
        /// </summary>
        public ulong ScoresChannelId { get; init; }

        /// <summary>
        /// Blockchain explorers base URL.
        /// </summary>
        public IDictionary<ScorerType, string> ExplorersBaseUrl { get; init; } = new Dictionary<ScorerType, string>();

        /// <summary>
        /// Update score links.
        /// </summary>
        public IDictionary<ScorerType, string> UpdateLinks { get; init; } = new Dictionary<ScorerType, string>();

        /// <summary>
        /// Marketplaces base URL.
        /// </summary>
        public IDictionary<ScorerType, List<string>> MarketplacesBaseUrl { get; init; } = new Dictionary<ScorerType, List<string>>();

        /// <summary>
        /// Events is enabled.
        /// </summary>
        public bool EventsIdEnabled { get; init; }

        /// <summary>
        /// Events delay.
        /// </summary>
        public TimeSpan EventsDelay { get; init; }

        /// <inheritdoc cref="DiscordSocketConfig"/>
        public DiscordSocketConfig SocketConfig { get; init; } = null!;

        /// <inheritdoc cref="CommandServiceConfig"/>
        public CommandServiceConfig CommandConfig { get; init; } = null!;

        /// <inheritdoc cref="InteractionServiceConfig"/>
        public InteractionServiceConfig InteractionConfig { get; init; } = null!;
    }
}