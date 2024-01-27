// ------------------------------------------------------------------------------------------------------
// <copyright file="IScorerDiscordBotService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Discord;
using Nomis.Blockchain.Abstractions;
using Nomis.Discord.Bots.Interfaces.Enums;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Wrapper;

namespace Nomis.Discord.Bots.Interfaces
{
    /// <summary>
    /// OneInch service.
    /// </summary>
    public interface IScorerDiscordBotService :
        IInfrastructureService
    {
        /// <summary>
        /// Send calculated score data to discord channel.
        /// </summary>
        /// <typeparam name="TWalletStatsRequest">The type of wallet request.</typeparam>
        /// <typeparam name="TWalletScore">The type of wallet score.</typeparam>
        /// <typeparam name="TWalletStats">The type of wallet stats.</typeparam>
        /// <typeparam name="TTransactionIntervalData">The type of transaction interval data.</typeparam>
        /// <param name="request">Wallet request.</param>
        /// <param name="scoreData">Wallet score.</param>
        /// <param name="scorerType">Score type.</param>
        public Task SendCalculatedScoreToChannelAsync<TWalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(
            TWalletStatsRequest request,
            TWalletScore scoreData,
            ScorerType scorerType)
            where TWalletStatsRequest : WalletStatsRequest
            where TWalletScore : IWalletScore<TWalletStats, TTransactionIntervalData>, new()
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData;

        /// <summary>
        /// Get wallet address by user discord id.
        /// </summary>
        /// <param name="discordUserId">User discord id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns wallet address by user discord id.</returns>
        public Task<string?> GetWalletAddressByDiscordAsync(
            string discordUserId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get wallet minted score.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <param name="scorerType">Score type.</param>
        /// <returns>Returns wallet current score.</returns>
        public Task<Result<EmbedBuilder>> MintedScoreByScorerTypeAsync(
            string address,
            ScorerType scorerType);
    }
}