// ------------------------------------------------------------------------------------------------------
// <copyright file="SnowtraceService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.Util;
using Nomis.Aave.Interfaces;
using Nomis.Aave.Interfaces.Contracts;
using Nomis.Aave.Interfaces.Enums;
using Nomis.Aave.Interfaces.Responses;
using Nomis.BalanceChecker.Interfaces;
using Nomis.BalanceChecker.Interfaces.Requests;
using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Contracts.Models;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Blockchain.Abstractions.Extensions;
using Nomis.Blockchain.Abstractions.Requests;
using Nomis.Blockchain.Abstractions.Settings;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Chainanalysis.Interfaces;
using Nomis.Chainanalysis.Interfaces.Contracts;
using Nomis.Chainanalysis.Interfaces.Extensions;
using Nomis.Covalent.Interfaces;
using Nomis.CyberConnect.Interfaces;
using Nomis.CyberConnect.Interfaces.Contracts;
using Nomis.CyberConnect.Interfaces.Extensions;
using Nomis.DefiLlama.Interfaces;
using Nomis.DefiLlama.Interfaces.Models;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces;
using Nomis.DexProviderService.Interfaces.Extensions;
using Nomis.DexProviderService.Interfaces.Requests;
using Nomis.Domain.Scoring.Entities;
using Nomis.Greysafe.Interfaces;
using Nomis.Greysafe.Interfaces.Contracts;
using Nomis.Greysafe.Interfaces.Extensions;
using Nomis.IPFS.Interfaces;
using Nomis.PolygonId.Interfaces;
using Nomis.ReferralService.Interfaces;
using Nomis.ReferralService.Interfaces.Extensions;
using Nomis.ScoringService.Interfaces;
using Nomis.Snapshot.Interfaces;
using Nomis.Snapshot.Interfaces.Contracts;
using Nomis.Snapshot.Interfaces.Extensions;
using Nomis.Snowtrace.Calculators;
using Nomis.Snowtrace.Interfaces;
using Nomis.Snowtrace.Interfaces.Extensions;
using Nomis.Snowtrace.Interfaces.Models;
using Nomis.Snowtrace.Interfaces.Requests;
using Nomis.Snowtrace.Settings;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.SoulboundTokenService.Interfaces.Extensions;
using Nomis.Tally.Interfaces;
using Nomis.Tally.Interfaces.Contracts;
using Nomis.Tally.Interfaces.Extensions;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Enums;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Extensions;
using Nomis.Utils.Wrapper;

namespace Nomis.Snowtrace
{
    /// <inheritdoc cref="IAvalancheScoringService"/>
    internal sealed class SnowtraceService :
        BlockchainDescriptor,
        IAvalancheScoringService,
        ITransientService
    {
        private readonly SnowtraceSettings _settings;
        private readonly BlacklistSettings _blacklistSettings;
        private readonly ISnowtraceClient _client;
        private readonly IScoringService _scoringService;
        private readonly IReferralService _referralService;
        private readonly IEvmScoreSoulboundTokenService _soulboundTokenService;
        private readonly ISnapshotService _snapshotService;
        private readonly ITallyService _tallyService;
        private readonly IDexProviderService _dexProviderService;
        private readonly IDefiLlamaService _defiLlamaService;
        private readonly IAaveService _aaveService;
        private readonly IGreysafeService _greysafeService;
        private readonly IChainanalysisService _chainanalysisService;
        private readonly ICyberConnectService _cyberConnectService;
        private readonly IBalanceCheckerService _balanceCheckerService;
        private readonly IIPFSService _ipfsService;
        private readonly IPolygonIdService _polygonIdService;
        private readonly ICacheProviderService _cacheProviderService;
        private readonly ICovalentService _covalentService;

        /// <summary>
        /// Initialize <see cref="SnowtraceService"/>.
        /// </summary>
        /// <param name="blacklistSettings"><see cref="BlacklistSettings"/>.</param>
        /// <param name="settings"><see cref="SnowtraceSettings"/>.</param>
        /// <param name="client"><see cref="ISnowtraceClient"/>.</param>
        /// <param name="scoringService"><see cref="IScoringService"/>.</param>
        /// <param name="referralService"><see cref="IReferralService"/>.</param>
        /// <param name="soulboundTokenService"><see cref="IEvmScoreSoulboundTokenService"/>.</param>
        /// <param name="snapshotService"><see cref="ISnapshotService"/>.</param>
        /// <param name="tallyService"><see cref="ITallyService"/>.</param>
        /// <param name="dexProviderService"><see cref="IDexProviderService"/>.</param>
        /// <param name="defiLlamaService"><see cref="IDefiLlamaService"/>.</param>
        /// <param name="aaveService"><see cref="IAaveService"/>.</param>
        /// <param name="greysafeService"><see cref="IGreysafeService"/>.</param>
        /// <param name="chainanalysisService"><see cref="IChainanalysisService"/>.</param>
        /// <param name="cyberConnectService"><see cref="ICyberConnectService"/>.</param>
        /// <param name="balanceCheckerService"><see cref="IBalanceCheckerService"/>.</param>
        /// <param name="ipfsService"><see cref="IIPFSService"/>.</param>
        /// <param name="polygonIdService"><see cref="IPolygonIdService"/>.</param>
        /// <param name="cacheProviderService"><see cref="ICacheProviderService"/>.</param>
        /// <param name="covalentService"><see cref="ICovalentService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public SnowtraceService(
            IOptions<BlacklistSettings> blacklistSettings,
            IOptions<SnowtraceSettings> settings,
            ISnowtraceClient client,
            IScoringService scoringService,
            IReferralService referralService,
            IEvmScoreSoulboundTokenService soulboundTokenService,
            ISnapshotService snapshotService,
            ITallyService tallyService,
            IDexProviderService dexProviderService,
            IDefiLlamaService defiLlamaService,
            IAaveService aaveService,
            IGreysafeService greysafeService,
            IChainanalysisService chainanalysisService,
            ICyberConnectService cyberConnectService,
            IBalanceCheckerService balanceCheckerService,
            IIPFSService ipfsService,
            IPolygonIdService polygonIdService,
            ICacheProviderService cacheProviderService,
            ICovalentService covalentService,
            ILogger<SnowtraceService> logger)
#pragma warning disable S3358
            : base(settings.Value.BlockchainDescriptors.TryGetValue(BlockchainKind.Mainnet, out var value) ? value : settings.Value.BlockchainDescriptors.TryGetValue(BlockchainKind.Testnet, out var testnetValue) ? testnetValue : null)
#pragma warning restore S3358
        {
            _settings = settings.Value;
            _blacklistSettings = blacklistSettings.Value;
            _client = client;
            _scoringService = scoringService;
            _referralService = referralService;
            _soulboundTokenService = soulboundTokenService;
            _snapshotService = snapshotService;
            _tallyService = tallyService;
            _dexProviderService = dexProviderService;
            _defiLlamaService = defiLlamaService;
            _aaveService = aaveService;
            _greysafeService = greysafeService;
            _chainanalysisService = chainanalysisService;
            _cyberConnectService = cyberConnectService;
            _balanceCheckerService = balanceCheckerService;
            _ipfsService = ipfsService;
            _polygonIdService = polygonIdService;
            _cacheProviderService = cacheProviderService;
            _covalentService = covalentService;
            Logger = logger;
        }

        /// <inheritdoc/>
        public ILogger Logger { get; }

        /// <inheritdoc/>
        public async Task<Result<TWalletScore>> GetWalletStatsAsync<TWalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(
            TWalletStatsRequest request,
            CancellationToken cancellationToken = default)
            where TWalletStatsRequest : WalletStatsRequest
            where TWalletScore : IWalletScore<TWalletStats, TTransactionIntervalData>, new()
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData
        {
            if (!new AddressUtil().IsValidAddressLength(request.Address) || !new AddressUtil().IsValidEthereumAddressHexFormat(request.Address))
            {
                throw new InvalidAddressException(request.Address);
            }

            #region Blacklist

            if (_blacklistSettings.UseBlacklist)
            {
                var blacklist = new List<string>();
                foreach (var blacklistItem in _blacklistSettings.Blacklist)
                {
                    blacklist.AddRange(blacklistItem.Value);
                }

                if (blacklist.Contains(request.Address.ToLower()))
                {
                    throw new CustomException("The specified wallet address cannot be scored.", statusCode: HttpStatusCode.BadRequest);
                }
            }

            #endregion Blacklist

            var messages = new List<string>();

            #region Referral

            string? ownReferralCode = null;
            if (request.CalculationModel != ScoringCalculationModel.Galxe)
            {
                var ownReferralCodeResult = await _referralService.GetOwnReferralCodeAsync(request, _cacheProviderService, Logger, (request as BaseEvmWalletStatsRequest)?.ShouldGetReferrerCode ?? true, cancellationToken).ConfigureAwait(false);
                messages.AddRange(ownReferralCodeResult.Messages);
                ownReferralCode = ownReferralCodeResult.Data;
            }

            #endregion Referral

            var mintBlockchain = _dexProviderService.MintChain(request, ChainId);

            TWalletStats? walletStats = null;
            bool calculateNewScore = false;
            if (_settings.GetFromCacheStatsIsEnabled && !request.DisableCache)
            {
                walletStats = await _cacheProviderService.GetFromCacheAsync<AvalancheWalletStats>($"{request.Address}_{ChainId}_{(int)request.CalculationModel}_{(int)request.ScoreType}_{request.Nonce}").ConfigureAwait(false) as TWalletStats;
            }

            if (walletStats == null)
            {
                calculateNewScore = true;
                string? balanceWei = (await _client.GetBalanceAsync(request.Address).ConfigureAwait(false)).Balance;
                TokenPriceData? priceData = null;
                (await _defiLlamaService.TokensPriceAsync(new List<string> { $"coingecko:{PlatformIds?[BlockchainPlatform.Coingecko]}" }).ConfigureAwait(false))?.TokensPrices.TryGetValue($"coingecko:{PlatformIds?[BlockchainPlatform.Coingecko]}", out priceData);
                decimal usdBalance = (priceData?.Price ?? 0M) * balanceWei?.ToAvax() ?? 0;
                var transactions = (await _client.GetTransactionsAsync<BaseEvmNormalTransactions, BaseEvmNormalTransaction>(request.Address).ConfigureAwait(false)).ToList();
                if (!transactions.Any())
                {
                    return await Result<TWalletScore>.FailAsync(
                        new()
                        {
                            Address = request.Address,
                            Stats = new TWalletStats
                            {
                                NoData = true
                            },
                            Score = 0
                        }, new List<string> { "There is no transactions for this wallet." }).ConfigureAwait(false);
                }

                var erc20Tokens = (await _client.GetTransactionsAsync<BaseEvmERC20TokenTransfers, BaseEvmERC20TokenTransfer>(request.Address).ConfigureAwait(false)).ToList();
                var tokenTransfers = new List<NFTTokenTransfer>();

                #region Tokens data

                var tokenDataBalances = new List<TokenDataBalance>();
                if ((request as IWalletTokensSwapPairsRequest)?.GetTokensSwapPairs == true
                    || (request as IWalletTokensBalancesRequest)?.GetHoldTokensBalances == true)
                {
                    bool checkBalanceResult = false;
                    var balanceCheckerResult = await _balanceCheckerService.TokenBalancesAsync(
                        new TokenBalancesRequest
                        {
                            Owner = request.Address,
                            BlockchainDescriptor = this,
                            TokenAddresses = erc20Tokens.Select(x => x.ContractAddress).Distinct().Where(x => x != null).Cast<string>().ToList(),
                            StablecoinsPrices = _dexProviderService.StablecoinsPriceData(ChainId),
                            UseDeFiApi = (request as BaseEvmWalletStatsRequest)?.UseDeFiApi ?? false,
                            UseCovalentApi = (request as BaseEvmWalletStatsRequest)?.UseCovalentApi ?? true,
                            UseRpcApi = (request as BaseEvmWalletStatsRequest)?.DisableRpcBalanceChecker != true
                        }, async (owner, tokenAddress) => await _client.GetTokenDataBalanceAsync(owner, tokenAddress, ChainId).ConfigureAwait(false)).ConfigureAwait(false);

                    if (balanceCheckerResult.Succeeded)
                    {
                        checkBalanceResult = true;
                        tokenDataBalances.AddRange(balanceCheckerResult.Data.Where(b => b.Balance > 0).Select(b => new TokenDataBalance
                        {
                            ChainId = ChainId,
                            Id = b.Id,
                            Balance = b.Balance,
                            Decimals = b.Decimals.ToString(),
                            Name = b.Name,
                            Symbol = b.Symbol,
                            LogoUri = b.LogoUri,
                            Price = b.Price,
                            LastPriceDateTime = DateTime.UtcNow,
                            Confidence = 0.9M,
                            Source = b.Source
                        }));
                    }

                    if (!checkBalanceResult)
                    {
                        foreach (string? erc20TokenContractId in erc20Tokens.Select(x => x.ContractAddress).Distinct())
                        {
                            var tokenDataBalance = await _client.GetTokenDataBalanceAsync(request.Address, erc20TokenContractId!, ChainId).ConfigureAwait(false);
                            if (tokenDataBalance != null)
                            {
                                tokenDataBalances.Add(tokenDataBalance);
                            }
                        }
                    }
                }

                #endregion Tokens data

                #region Tokens balances

                var stablecoinsPrices = _dexProviderService.StablecoinsPriceData(ChainId);
                var pricesData = tokenDataBalances.Select(x => x.ToTokenPriceData()).Union(stablecoinsPrices).ToList();
                if (request is IWalletTokensBalancesRequest balancesRequest)
                {
                    tokenDataBalances = await tokenDataBalances
                        .EnrichWithDefiLlamaAsync(_defiLlamaService, balancesRequest, PlatformIds![BlockchainPlatform.DefiLLama], pricesData).ConfigureAwait(false);
                    tokenDataBalances = await tokenDataBalances
                        .EnrichWithTokensListsAsync(_dexProviderService, balancesRequest, ChainId).ConfigureAwait(false);
                    tokenDataBalances = await tokenDataBalances
                        .EnrichWithLlamaFolioAsync(_defiLlamaService, request.Address, request, PlatformIds![BlockchainPlatform.Llamafolio]).ConfigureAwait(false);

                    tokenDataBalances = tokenDataBalances
                        .Where(b => b.TotalAmountPrice > 0)
                        .OrderByDescending(b => b.TotalAmountPrice)
                        .ThenByDescending(b => b.Balance)
                        .ThenBy(b => b.Id)
                        .ThenBy(b => b.Symbol)
                        .DistinctBy(b => $"{b.Symbol}_{b.Price}_{b.Amount}")
                        .ToList();
                }

                #endregion Tokens balances

                #region Galxe

                if (request.CalculationModel == ScoringCalculationModel.Galxe)
                {
                    walletStats = new TWalletStats
                    {
                        WalletAge = transactions.Any()
                            ? IWalletStatsCalculator.GetWalletAge(transactions.Select(x => x.Timestamp!.ToDateTime()))
                            : 0
                    };

                    if (walletStats is BaseEvmWalletStats<AvalancheTransactionIntervalData> baseWalletStats)
                    {
                        baseWalletStats.TokensHolding = erc20Tokens.Select(x => x.TokenSymbol).Distinct().Count();
                    }

                    if (walletStats is IWalletTransactionStats transactionStats)
                    {
                        transactionStats.TotalTransactions = transactions.Count;
                    }

                    if (walletStats is IWalletNftStats nftStats)
                    {
                        try
                        {
                            var covalentNfts = (await _covalentService.WalletNftsAsync(PlatformIds![BlockchainPlatform.Covalent], request.Address, true, cancellationToken).ConfigureAwait(false)).Data?.Nfts.ToList();
                            if (covalentNfts?.Count > 0)
                            {
                                nftStats.NFTHolding = covalentNfts.Count;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.LogWarning(e, "Error on getting nfts from Covalent API.");
                            var erc721Tokens = (await _client.GetTransactionsAsync<BaseEvmERC721TokenTransfers, BaseEvmERC721TokenTransfer>(request.Address).ConfigureAwait(false)).ToList();

                            // var erc1155Tokens = (await _client.GetTransactionsAsync<BaseEvmERC1155TokenTransfers, BaseEvmERC1155TokenTransfer>(request.Address).ConfigureAwait(false)).ToList();
                            // tokenTransfers.AddRange(erc1155Tokens);
                            tokenTransfers.AddRange(erc721Tokens);
                            var soldTokens = tokenTransfers.Where(x => x.From?.Equals(request.Address, StringComparison.InvariantCultureIgnoreCase) == true).ToList();
                            nftStats.NFTHolding = tokenTransfers.Count - soldTokens.Count;
                        }
                    }

                    if (walletStats is IWalletBalanceStats balanceStats)
                    {
                        balanceStats.TokenBalances = tokenDataBalances.Any() ? tokenDataBalances : null;
                    }

                    if (!request.DisableCache)
                    {
                        await _cacheProviderService.SetCacheAsync($"{request.Address}_{ChainId}_{(int)request.CalculationModel}_{(int)request.ScoreType}_{request.Nonce}", walletStats, new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = _settings.GetFromCacheStatsTimeLimit
                        }).ConfigureAwait(false);
                    }

                    return await Result<TWalletScore>.SuccessAsync(
                        new()
                        {
                            Address = request.Address,
                            Stats = walletStats,
                            Score = 0
                        }, messages).ConfigureAwait(false);
                }
                else
                {
                    var tokens = (await _client.GetTransactionsAsync<BaseEvmERC721TokenTransfers, BaseEvmERC721TokenTransfer>(request.Address).ConfigureAwait(false)).ToList();
                    tokenTransfers.AddRange(tokens);
                }

                #endregion Galxe

                var internalTransactions = (await _client.GetTransactionsAsync<BaseEvmInternalTransactions, BaseEvmInternalTransaction>(request.Address).ConfigureAwait(false)).ToList();

                #region Greysafe scam reports

                var greysafeReportsResponse = await _greysafeService.ReportsAsync(request as IWalletGreysafeRequest).ConfigureAwait(false);

                #endregion Greysafe scam reports

                #region Chainanalysis sanctions reports

                var chainanalysisReportsResponse = await _chainanalysisService.ReportsAsync(request as IWalletChainanalysisRequest).ConfigureAwait(false);

                #endregion Chainanalysis sanctions reports

                #region Snapshot protocol

                var snapshotData = await _snapshotService.DataAsync(request as IWalletSnapshotProtocolRequest, ChainId).ConfigureAwait(false);

                #endregion Snapshot protocol

                #region Tally protocol

                var tallyAccountData = await _tallyService.AccountDataAsync(request as IWalletTallyProtocolRequest, ChainId).ConfigureAwait(false);

                #endregion Tally protocol

                #region CyberConnect protocol

                var cyberConnectData = await _cyberConnectService.DataAsync(request as IWalletCyberConnectProtocolRequest, ChainId).ConfigureAwait(false);

                #endregion CyberConnect protocol

                #region Swap pairs from DEXes

                var dexTokenSwapPairs = new List<DexTokenSwapPairsData>();
                if ((request as IWalletTokensSwapPairsRequest)?.GetTokensSwapPairs == true && tokenDataBalances.Any())
                {
                    var swapPairsResult = await _dexProviderService.BlockchainSwapPairsAsync(new()
                    {
                        Blockchain = (Chain)ChainId,
                        First = (request as IWalletTokensSwapPairsRequest)?.FirstSwapPairs ?? 100,
                        Skip = (request as IWalletTokensSwapPairsRequest)?.Skip ?? 0,
                        FromCache = (request as IWalletTokensSwapPairsRequest)?.FromCache ?? false
                    }).ConfigureAwait(false);
                    if (swapPairsResult.Succeeded)
                    {
                        var dexTokensDataResult = await _dexProviderService.TokensDataAsync(new TokensDataRequest
                        {
                            Blockchain = (Chain)ChainId,
                            IncludeUniversalTokenLists = (request as IWalletTokensBalancesRequest)?.IncludeUniversalTokenLists ?? false,
                            FromCache = true
                        }).ConfigureAwait(false);
                        dexTokenSwapPairs.AddRange(tokenDataBalances.Select(t =>
                            DexTokenSwapPairsData.ForSwapPairs(t.Id!, t.Balance, swapPairsResult.Data, dexTokensDataResult.Data)));
                        dexTokenSwapPairs.RemoveAll(p => !p.TokenSwapPairs.Any());
                    }
                }

                #endregion Swap pairs from DEXes

                #region Aave protocol

                AaveUserAccountDataResponse? aaveAccountDataResponse = null;
                if ((request as IWalletAaveProtocolRequest)?.GetAaveProtocolData == true)
                {
                    aaveAccountDataResponse = (await _aaveService.GetAaveUserAccountDataAsync(AaveChain.Avalanche, request.Address).ConfigureAwait(false)).Data;
                }

                #endregion Aave protocol

                #region Median USD balance

                decimal medianUsdBalance = await _scoringService.MedianBalanceUsdAsync<AvalancheWalletStats>(request.Address, ChainId, request.CalculationModel, _settings, usdBalance + (tokenDataBalances.Any() ? tokenDataBalances : null)?.Sum(b => b.TotalAmountPrice) ?? 0, cancellationToken).ConfigureAwait(false);

                #endregion Median USD balance

                #region Tokens transfers balances

                var transferTokenDataBalances = new List<TransferTokenDataBalance>();
                if (request is IWalletTokensBalancesRequest { GetTokensTransfersBalances: true } tokensTransfersRequest)
                {
                    transferTokenDataBalances = erc20Tokens.ToTransferTokenDataBalances(transactions, request, ChainId);
                    pricesData = transferTokenDataBalances.Select(x => x.ToTokenPriceData()).Union(stablecoinsPrices).ToList();
                    transferTokenDataBalances = await transferTokenDataBalances
                        .EnrichWithDefiLlamaAsync(_defiLlamaService, tokensTransfersRequest, PlatformIds![BlockchainPlatform.DefiLLama], pricesData).ConfigureAwait(false);
                    transferTokenDataBalances = transferTokenDataBalances
                        .EnrichWithTokensBalances(tokenDataBalances);
                    transferTokenDataBalances = await transferTokenDataBalances
                        .EnrichWithTokensListsAsync(_dexProviderService, tokensTransfersRequest, ChainId).ConfigureAwait(false);
                    transferTokenDataBalances = await transferTokenDataBalances
                        .EnrichWithLlamaFolioAsync(_defiLlamaService, request.Address, request, PlatformIds![BlockchainPlatform.Llamafolio]).ConfigureAwait(false);

                    transferTokenDataBalances = transferTokenDataBalances
                        .Where(b => tokensTransfersRequest.ShowTokensTransfersWithZeroPrice || b.TotalAmountPrice > 0)
                        .OrderByDescending(b => b.TotalAmountPrice)
                        .ThenByDescending(b => b.Balance)
                        .ThenBy(b => b.Id)
                        .ThenBy(b => b.Symbol)
                        .DistinctBy(b => b.TransactionHash)
                        .ToList();
                }

                #endregion Tokens transfers balances

                walletStats = new AvalancheStatCalculator(
                        request.Address,
                        decimal.TryParse(balanceWei, out decimal wei) ? wei : 0,
                        usdBalance,
                        medianUsdBalance,
                        transactions,
                        internalTransactions,
                        tokenTransfers,
                        erc20Tokens,
                        snapshotData,
                        tallyAccountData,
                        tokenDataBalances,
                        dexTokenSwapPairs,
                        aaveAccountDataResponse,
                        greysafeReportsResponse?.Reports,
                        chainanalysisReportsResponse?.Identifications,
                        cyberConnectData,
                        transferTokenDataBalances)
                .Stats() as TWalletStats;

                if (!request.DisableCache)
                {
                    await _cacheProviderService.SetCacheAsync($"{request.Address}_{ChainId}_{(int)request.CalculationModel}_{(int)request.ScoreType}_{request.Nonce}", walletStats!, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _settings.GetFromCacheStatsTimeLimit
                    }).ConfigureAwait(false);
                }
            }

            double score = walletStats!.CalculateScore<TWalletStats, TTransactionIntervalData>(ChainId, request.CalculationModel);
            if (calculateNewScore && request is BaseEvmWalletStatsRequest { StoreScoreResults: true })
            {
                var scoringData = new ScoringData(request.Address, request.Address, request.CalculationModel, JsonSerializer.Serialize(request), ChainId, score, JsonSerializer.Serialize(walletStats));
                await _scoringService.SaveScoringDataToDatabaseAsync(scoringData, cancellationToken).ConfigureAwait(false);
            }

            var metadataResult = await _soulboundTokenService.TokenMetadataAsync(_ipfsService, _cacheProviderService, request, ChainId, ChainName, score).ConfigureAwait(false);

            // getting signature
            ushort mintedScore = (ushort)(score * 10000);
            var signatureResult = await _soulboundTokenService
                .SignatureAsync(request, mintedScore, mintBlockchain?.ChainId ?? request.GetChainId(_settings), mintBlockchain?.SBTData ?? request.GetSBTData(_settings), metadataResult.Data, ChainId, ownReferralCode ?? "anon", request.ReferrerCode ?? "nomis", (request as IWalletGreysafeRequest)?.GetGreysafeData, (request as IWalletChainanalysisRequest)?.GetChainanalysisData).ConfigureAwait(false);

            messages.AddRange(signatureResult.Messages);
            messages.Add($"Got {ChainName} wallet {request.ScoreType.ToString()} score.");

            #region DID

            var didDataResult = await _polygonIdService.CreateClaimAndGetQrAsync<AvalancheWalletStatsRequest, AvalancheWalletStats, AvalancheTransactionIntervalData>((request as AvalancheWalletStatsRequest) !, mintedScore, (walletStats as AvalancheWalletStats) !, DateTime.UtcNow.AddYears(5).ConvertToTimestamp(), ChainId, cancellationToken).ConfigureAwait(false);
            messages.Add(didDataResult.Messages.FirstOrDefault() ?? string.Empty);

            #endregion DID

            return await Result<TWalletScore>.SuccessAsync(
                new()
                {
                    Address = request.Address,
                    Stats = walletStats,
                    Score = score,
                    MintData = request.PrepareToMint ? new MintData(signatureResult.Data.Signature, mintedScore, request.CalculationModel, request.Deadline, metadataResult.Data, ChainId, mintBlockchain ?? this, ownReferralCode ?? "anon", request.ReferrerCode ?? "nomis") : null,
                    DIDData = didDataResult.Data,
                    ReferralCode = ownReferralCode,
                    ReferrerCode = request.ReferrerCode
                }, messages).ConfigureAwait(false);
        }
    }
}