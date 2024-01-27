// ------------------------------------------------------------------------------------------------------
// <copyright file="ScorerDiscordBotService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Discord.Bots.Interfaces;
using Nomis.Discord.Bots.Interfaces.Enums;
using Nomis.Discord.Bots.Settings;
using Nomis.NomisHolders.Interfaces;
using Nomis.NomisHolders.Interfaces.Enums;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;
using Nomis.Utils.Extensions;
using Nomis.Utils.Wrapper;
using Nomis.Zealy.Interfaces;

namespace Nomis.Discord.Bots
{
    /// <inheritdoc cref="IScorerDiscordBotService"/>
    internal sealed class ScorerDiscordBotService :
        IScorerDiscordBotService,
        ISingletonService
    {
        private readonly DiscordBotsSettings _settings;
        private readonly DiscordSocketClient _client;
        private readonly INomisHoldersService _nomisHoldersService;
        private readonly IZealyService _zealyService;
        private readonly ICacheProviderService _cacheProviderService;
        private readonly ILogger<ScorerDiscordBotService> _logger;

        /// <summary>
        /// Initialize <see cref="ScorerDiscordBotService"/>.
        /// </summary>
        /// <param name="settings"><see cref="DiscordBotsSettings"/>.</param>
        /// <param name="client"><see cref="DiscordSocketClient"/>.</param>
        /// <param name="nomisHoldersService"><see cref="INomisHoldersService"/>.</param>
        /// <param name="zealyService"><see cref="IZealyService"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ScorerDiscordBotService(
            DiscordBotsSettings settings,
            DiscordSocketClient client,
            INomisHoldersService nomisHoldersService,
            IZealyService zealyService,
            ICacheProviderService cacheProviderService,
            ILogger<ScorerDiscordBotService> logger)
        {
            _settings = settings;
            _client = client;
            _nomisHoldersService = nomisHoldersService;
            _zealyService = zealyService;
            _cacheProviderService = cacheProviderService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<string?> GetWalletAddressByDiscordAsync(
            string discordUserId,
            CancellationToken cancellationToken = default)
        {
            var zealyLeaderboard = await _zealyService.LeaderBoardAsync("0xscore", cancellationToken).ConfigureAwait(false);
            if (zealyLeaderboard != null)
            {
                var zealyUser = zealyLeaderboard.LeaderBoard.Find(x => x.DiscordId?.Equals(discordUserId) == true);
                if (zealyUser != null && !string.IsNullOrWhiteSpace(zealyUser.Address))
                {
                    return zealyUser.Address;
                }

                if (zealyUser != null && !string.IsNullOrWhiteSpace(zealyUser.ConnectedWallet))
                {
                    return zealyUser.ConnectedWallet;
                }
            }

            // TODO - add from Galxe API
            return null;
        }

        /// <inheritdoc />
        public async Task SendCalculatedScoreToChannelAsync<TWalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(
            TWalletStatsRequest request,
            TWalletScore scoreData,
            ScorerType scorerType)
            where TWalletStatsRequest : WalletStatsRequest
            where TWalletScore : IWalletScore<TWalletStats, TTransactionIntervalData>, new()
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData
        {
            if (!_settings.IsEnabled)
            {
                return;
            }

            if (_settings.ExcludedScorers.Contains(scorerType))
            {
                return;
            }

            await Task.Delay(_settings.SendChannelDelay).ConfigureAwait(false);
            var result = new EmbedBuilder()
                .WithAuthor("Nomis Protocol")
                .WithDescription($"**Wallet**: [{scoreData.Address}]({_settings.ExplorersBaseUrl[scorerType]}/{scoreData.Address})");

            switch (scorerType)
            {
                case ScorerType.Multichain:
                    result
                        .WithColor(Color.DarkBlue)
                        .WithTitle("Scored Multichain Non-sybil Score");
                    break;
                case ScorerType.ZkSync:
                    result
                        .WithColor(Color.Blue)
                        .WithTitle("Scored zkSync Reputation Score");
                    break;
                case ScorerType.LayerZero:
                    result
                        .WithColor(Color.DarkGreen)
                        .WithTitle("Scored LayerZero Reputation Score");
                    break;
                case ScorerType.DeFi:
                    result
                        .WithColor(Color.Red)
                        .WithTitle("Scored Cross-chain DeFi Reputation Score");
                    break;
                case ScorerType.ZkEVM:
                    result
                        .WithColor(Color.DarkMagenta)
                        .WithTitle("Scored Polygon zkEVM Reputation Score");
                    break;
                case ScorerType.Linea:
                    result
                        .WithColor(Color.DarkGrey)
                        .WithTitle("Scored Linea Reputation Score");
                    break;
                case ScorerType.Strateg:
                    result
                        .WithColor(Color.DarkOrange)
                        .WithTitle("Scored Strateg Reputation Score");
                    break;
                case ScorerType.Mantle:
                    result
                        .WithColor(Color.DarkTeal)
                        .WithTitle("Scored Mantle Reputation Score");
                    break;
                case ScorerType.OpBnb:
                    result
                        .WithColor(Color.Gold)
                        .WithTitle("Scored opBNB Reputation Score");
                    break;
            }

            string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, string.Empty)));
            string imageUrl = $"{_settings.ScoreImageBaseUrl}/api/score?address={scoreData.Address}&type={request.CalculationModel.ToString().ToLower()}&score={(byte)(scoreData.Score * 100)}&size=512&chainId={scoreData.MintData?.ChainId}&time={DateTime.UtcNow.ConvertToTimestamp() * 1000}";

            if (!string.IsNullOrWhiteSpace(scoreData.MintData?.MintedChain.ChainName))
            {
                result
                    .AddField("Blockchain:", $"**{scoreData.MintData.MintedChain.ChainName}**");
            }

            result
                .WithImageUrl(imageUrl)
                .AddField("Score:", $"**{scoreData.Score * 100}**")
                .AddField("Calculation model:", $"**{request.CalculationModel.ToString()}**")
                .AddField("Marketplaces:", $"**{marketplaces}**")
                .AddField("Mint link:", _settings.UpdateLinks[scorerType]);

            if (_client.GetChannel(_settings.ScoresChannelId) is ITextChannel channel)
            {
                await channel.SendMessageAsync(text: $"Successfully calculated score for {request.Address}", embed: result.Build()).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<Result<EmbedBuilder>> MintedScoreByScorerTypeAsync(
            string address,
            ScorerType scorerType)
        {
            if (!_settings.IsEnabled)
            {
                return await Result<EmbedBuilder>.FailAsync("Discord bot is not enabled.").ConfigureAwait(false);
            }

            var result = new EmbedBuilder()
                .WithAuthor("Nomis Protocol")
                .WithDescription($"**Wallet**: [{address}]({_settings.ExplorersBaseUrl[scorerType]}/{address})");

            switch (scorerType)
            {
                case ScorerType.Multichain:
                    result
                        .WithColor(Color.DarkBlue)
                        .WithTitle("Multichain Non-sybil Score");

                    var multichainResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.Multichain, address).ConfigureAwait(false);
                    if (multichainResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, multichainResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)multichainResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(multichainResponse.Score ?? 0)}&size=512&chainId={multichainResponse.ChainId}&time={multichainResponse.Updated * 1000}")
                            .AddField("Score:", $"**{multichainResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)multichainResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{multichainResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(multichainResponse.Message ?? "There is an error while getting Multichain Non-sybil Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.ZkSync:
                    result
                        .WithColor(Color.Blue)
                        .WithTitle("zkSync Reputation Score");

                    var zkSyncResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.ZkSync, address).ConfigureAwait(false);
                    if (zkSyncResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, zkSyncResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)zkSyncResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(zkSyncResponse.Score ?? 0)}&size=512&chainId={zkSyncResponse.ChainId}&time={zkSyncResponse.Updated * 1000}")
                            .AddField("Score:", $"**{zkSyncResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)zkSyncResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{zkSyncResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(zkSyncResponse.Message ?? "There is an error while getting zkSync Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.LayerZero:
                    result
                        .WithColor(Color.DarkGreen)
                        .WithTitle("LayerZero Reputation Score");

                    var layerZeroResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.LayerZero, address).ConfigureAwait(false);
                    if (layerZeroResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, layerZeroResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)layerZeroResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(layerZeroResponse.Score ?? 0)}&size=512&time={layerZeroResponse.Updated * 1000}")
                            .AddField("Score:", $"**{layerZeroResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)layerZeroResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{layerZeroResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(layerZeroResponse.Message ?? "There is an error while getting LayerZero Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.DeFi:
                    result
                        .WithColor(Color.Red)
                        .WithTitle("Cross-chain DeFi Reputation Score");

                    var rubicResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.Rubic, address).ConfigureAwait(false);
                    if (rubicResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, rubicResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)rubicResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(rubicResponse.Score ?? 0)}&size=512&chainId={rubicResponse.ChainId}&time={rubicResponse.Updated * 1000}")
                            .AddField("Score:", $"**{rubicResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)rubicResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{rubicResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(rubicResponse.Message ?? "There is an error while getting Cross-chain DeFi Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.ZkEVM:
                    result
                        .WithColor(Color.DarkMagenta)
                        .WithTitle("Polygon zkEVM Reputation Score");

                    var zkEvmResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.ZkEvm, address).ConfigureAwait(false);
                    if (zkEvmResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, zkEvmResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)zkEvmResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(zkEvmResponse.Score ?? 0)}&size=512&chainId={zkEvmResponse.ChainId}&time={zkEvmResponse.Updated * 1000}")
                            .AddField("Score:", $"**{zkEvmResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)zkEvmResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{zkEvmResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(zkEvmResponse.Message ?? "There is an error while getting Polygon zkEVM Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.Linea:
                    result
                        .WithColor(Color.DarkGrey)
                        .WithTitle("Linea Reputation Score");

                    var lineaResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.Linea, address).ConfigureAwait(false);
                    if (lineaResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, lineaResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)lineaResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(lineaResponse.Score ?? 0)}&size=512&chainId={lineaResponse.ChainId}&time={lineaResponse.Updated * 1000}")
                            .AddField("Score:", $"**{lineaResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)lineaResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{lineaResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(lineaResponse.Message ?? "There is an error while getting Linea Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.Strateg:
                    result
                        .WithColor(Color.DarkOrange)
                        .WithTitle("Strateg Reputation Score");

                    var strategResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.Strateg, address).ConfigureAwait(false);
                    if (strategResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, strategResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)strategResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(strategResponse.Score ?? 0)}&size=512&chainId={strategResponse.ChainId}&time={strategResponse.Updated * 1000}")
                            .AddField("Score:", $"**{strategResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)strategResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{strategResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(strategResponse.Message ?? "There is an error while getting Strateg Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.Mantle:
                    result
                        .WithColor(Color.DarkTeal)
                        .WithTitle("Mantle Reputation Score");

                    var mantleResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.Mantle, address).ConfigureAwait(false);
                    if (mantleResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, mantleResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)mantleResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(mantleResponse.Score ?? 0)}&size=512&chainId={mantleResponse.ChainId}&time={mantleResponse.Updated * 1000}")
                            .AddField("Score:", $"**{mantleResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)mantleResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{mantleResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(mantleResponse.Message ?? "There is an error while getting Mantle Reputation Score.").ConfigureAwait(false);
                    }

                    break;
                case ScorerType.OpBnb:
                    result
                        .WithColor(Color.Gold)
                        .WithTitle("opBNB Reputation Score");

                    var opBnbResponse = await _nomisHoldersService.HolderAsync(NomisHoldersScore.OpBnb, address).ConfigureAwait(false);
                    if (opBnbResponse.IsHolder == true)
                    {
                        string marketplaces = string.Join(" ", _settings.MarketplacesBaseUrl[scorerType].Select(x => string.Format(x, opBnbResponse.TokenId)));
                        result
                            .WithImageUrl($"{_settings.ScoreImageBaseUrl}/api/score?address={address}&type={((ScoringCalculationModel?)opBnbResponse.CalculationModel).ToString()?.ToLower()}&score={(byte)(opBnbResponse.Score ?? 0)}&size=512&chainId={opBnbResponse.ChainId}&time={opBnbResponse.Updated * 1000}")
                            .AddField("Score:", $"**{opBnbResponse.Score}**")
                            .AddField("Calculation model:", $"**{((ScoringCalculationModel?)opBnbResponse.CalculationModel).ToString()}**")
                            .AddField("Token id:", $"**{opBnbResponse.TokenId}**")
                            .AddField("Marketplaces:", $"**{marketplaces}**");
                    }
                    else
                    {
                        return await Result<EmbedBuilder>.FailAsync(opBnbResponse.Message ?? "There is an error while getting opBNB Reputation Score.").ConfigureAwait(false);
                    }

                    break;
            }

            result
                .AddField("Update link:", _settings.UpdateLinks[scorerType]);

            return await Result<EmbedBuilder>.SuccessAsync(result, "Successfully got minted score.").ConfigureAwait(false);
        }
    }
}