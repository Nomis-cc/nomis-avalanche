// ------------------------------------------------------------------------------------------------------
// <copyright file="DexProviderService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Dex.Abstractions;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;
using Nomis.Dex.Abstractions.Responses;
using Nomis.DexProviderService.Contracts;
using Nomis.DexProviderService.Converters;
using Nomis.DexProviderService.Extensions;
using Nomis.DexProviderService.Interfaces;
using Nomis.DexProviderService.Interfaces.Contracts;
using Nomis.DexProviderService.Interfaces.Requests;
using Nomis.DexProviderService.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.DexProviderService
{
    /// <inheritdoc cref="IDexProviderService"/>
    internal sealed class DexProviderService :
        IDexProviderService,
        ISingletonService
    {
        private readonly TokensProvidersSettings _tokensProvidersSettings;
        private readonly List<IBlockchainDescriptor> _blockchainDescriptors;
        private readonly List<StableCoinData> _stableCoins;
        private readonly List<IDexBaseService> _providers;
        private readonly ICacheProviderService _cacheProvider;
        private readonly ILogger<DexProviderService> _logger;

        /// <summary>
        /// Initialize <see cref="DexProviderService"/>.
        /// </summary>
        /// <param name="tokensProvidersSettings"><see cref="TokensProvidersSettings"/>.</param>
        /// <param name="blockchainDescriptors">List of <see cref="IBlockchainDescriptor"/>.</param>
        /// <param name="stableCoins">List of <see cref="StableCoinData"/>.</param>
        /// <param name="providers">List of <see cref="IDexBaseService"/>.</param>
        /// <param name="cacheProvider"><see cref="ICacheProviderService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DexProviderService(
            TokensProvidersSettings tokensProvidersSettings,
            List<IBlockchainDescriptor> blockchainDescriptors,
            List<StableCoinData> stableCoins,
            List<IDexBaseService> providers,
            ICacheProviderService cacheProvider,
            ILogger<DexProviderService> logger)
        {
            _tokensProvidersSettings = tokensProvidersSettings;
            _blockchainDescriptors = blockchainDescriptors;
            _stableCoins = stableCoins;
            _providers = providers;
            _cacheProvider = cacheProvider;
            _logger = logger;
        }

        /// <inheritdoc />
        public Result<List<StableCoinData>> StablecoinsData(
            Chain blockchain = Chain.None)
        {
            var result = _stableCoins;
            if (blockchain != Chain.None)
            {
                result = result.Where(x => x.Contracts.ContainsKey(blockchain)).ToList();
            }

            return Result<List<StableCoinData>>.Success(result, "Got the list of stablecoins.");
        }

        /// <inheritdoc />
        public Result<List<DexProviderData>> ProvidersData(
            DexProvider provider = DexProvider.None,
            Chain blockchain = Chain.None,
            bool? isEnabled = null)
        {
            var foundedProviders = ProviderServiceExtensions.DexDescriptors.AsEnumerable();

            if (isEnabled != null)
            {
                foundedProviders = foundedProviders
                    .Where(p => p?.IsEnabled == isEnabled);
            }

            if (provider != DexProvider.None)
            {
                foundedProviders = foundedProviders
                    .Where(p => p?.Provider == provider);
            }

            if (blockchain != Chain.None)
            {
                foundedProviders = foundedProviders
                    .Where(p => p?.Endpoints.Any(x => x?.Blockchain == blockchain) == true);
            }

            var result = new List<DexProviderData>();
            var blockchainDescriptor = _blockchainDescriptors.Find(b => b.ChainId == (ulong)blockchain);
            foreach (var foundedProvider in foundedProviders)
            {
                var providerData = new DexProviderData(
                    new(foundedProvider!),
                    blockchainDescriptor != null
                        ? new(blockchainDescriptor)
                        : new BlockchainDescriptor());
                result.Add(providerData);
            }

            return Result<List<DexProviderData>>.Success(result, "Got the list of DEX-providers data.");
        }

        /// <inheritdoc />
        public async Task<Result<List<DexTokenData>>> TokensDataAsync(
            TokensDataRequest request)
        {
            var results = new List<DexTokenData>();
            var providers = _tokensProvidersSettings
                .TokensProviders
                .Where(p => p.IsEnabled && (p.Urls.ContainsKey(request.Blockchain) || (request.IncludeUniversalTokenLists && p.Urls.ContainsKey(Chain.None)) || request.Blockchain == Chain.None))
                .ToList();

            foreach (var provider in providers)
            {
                provider.Urls = provider.Urls
                    .Where(u => u.Key == request.Blockchain || (request.IncludeUniversalTokenLists && u.Key == Chain.None) || request.Blockchain == Chain.None)
                    .ToDictionary(x => x.Key, y => y.Value);
            }

            var validProviders = providers
                .Where(p => request.IncludedProviderIds.Count == 0 || request.IncludedProviderIds.Contains(p.Provider))
                .Where(p => !request.IgnoredProviderIds.Contains(p.Provider))
                .ToList();

            var tasks = new List<Task<List<DexTokenData>>>();
            foreach (var tokensProvider in validProviders)
            {
                var urls = tokensProvider.Urls.ToDictionary(u => u.Key, w => w.Value);
                foreach (var url in urls)
                {
                    using var httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(url.Value)
                    };

                    var task = httpClient.GetAsync(string.Empty);
                    try
                    {
                        _logger.LogInformation("Starting getting tokens data for {TokenProvider} ({Blockchain}) tokens provider...", tokensProvider.Provider.ToString(), url.Key.ToString());
                        tasks.Add(task.ContinueWith(o =>
                        {
                            try
                            {
                                if (o.IsFaulted && !string.IsNullOrWhiteSpace(o.Exception?.Message))
                                {
                                    return new List<DexTokenData>();
                                }

                                string cacheKey = $"{url.Key.ToString()}_{tokensProvider.Provider.ToString()}_tokens_data";
                                if (_tokensProvidersSettings.UseCaching && request.FromCache)
                                {
                                    var tokensData = _cacheProvider.GetFromCacheAsync<List<DexTokenData>>(cacheKey).ConfigureAwait(false).GetAwaiter().GetResult();

                                    if (tokensData?.Any() == true)
                                    {
                                        _logger.LogInformation("Successfully got tokens data from cache for {TokenProvider} ({Blockchain}) tokens provider.", tokensProvider.Provider.ToString(), url.Key.ToString());
                                        return tokensData;
                                    }
                                }
                                else
                                {
                                    _logger.LogInformation("Successfully got tokens data for {TokenProvider} ({Blockchain}) tokens provider.", tokensProvider.Provider.ToString(), url.Key.ToString());
                                }

                                var resultType = tokensProvider.IsResponseList
                                    ? typeof(List<TokenListTokenData>)
                                    : typeof(TokenListTokens);
                                IList<TokenListTokenData> tokens;
                                object? result = o.Result.Content.ReadFromJsonAsync(resultType, new JsonSerializerOptions
                                {
                                    Converters = { new TokenListTokenDataChainConverter() },
                                    PropertyNameCaseInsensitive = false
                                }).ConfigureAwait(false).GetAwaiter().GetResult();
                                if (tokensProvider.IsResponseList)
                                {
                                    tokens = result as List<TokenListTokenData> ?? new List<TokenListTokenData>();
                                }
                                else
                                {
                                    tokens = (result as TokenListTokens)?.Tokens ?? new List<TokenListTokenData>();
                                }

                                if (tokensProvider.DefaultChainId != null)
                                {
                                    foreach (var token in tokens)
                                    {
                                        if (token.ChainId is null or Chain.None)
                                        {
                                            token.ChainId = tokensProvider.DefaultChainId;
                                        }
                                    }
                                }

                                if (tokensProvider.ChainIdSubstitutions?.Any() == true)
                                {
                                    foreach (var token in tokens)
                                    {
                                        if (token.ChainId != null
                                            && tokensProvider.ChainIdSubstitutions.ContainsKey((Chain)token.ChainId))
                                        {
                                            token.ChainId = tokensProvider.ChainIdSubstitutions[(Chain)token.ChainId];
                                        }
                                    }
                                }

                                if (request.Blockchain != Chain.None)
                                {
                                    tokens = tokens
                                        .Where(t => t.ChainId?.Equals(request.Blockchain) == true
                                                    || _blockchainDescriptors.Find(b => (Chain)b.ChainId == request.Blockchain)?.IsEVMCompatible != true)
                                        .ToList();
                                }

                                var resultTokens = tokens.Select(t => new DexTokenData
                                {
                                    Provider = tokensProvider.Provider,
                                    ChainId = _blockchainDescriptors.Find(b => (Chain)b.ChainId == request.Blockchain)?.IsEVMCompatible != true
                                        ? t.ChainId ?? url.Key
                                        : url.Key,
                                    Id = t.Address,
                                    Name = t.Name,
                                    Symbol = t.Symbol,
                                    Decimals = t.Decimals.ToString(),
                                    LogoUri = t.LogoUri
                                });

#pragma warning disable S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
                                var response = resultTokens.Where(t => results.All(r => r.Id?.Equals(t.Id, StringComparison.OrdinalIgnoreCase) != true)).ToList();
#pragma warning restore S6603 // The collection-specific "TrueForAll" method should be used instead of the "All" extension
                                if (_tokensProvidersSettings.UseCaching && response.Any())
                                {
                                    _logger.LogInformation("Successfully saved tokens data to cache for {TokenProvider} ({Blockchain}) tokens provider.", tokensProvider.Provider.ToString(), url.Key.ToString());
                                    _cacheProvider.SetCacheAsync(
                                        cacheKey,
                                        response,
                                        new() { SlidingExpiration = _tokensProvidersSettings.CacheSlidingExpiration }).ConfigureAwait(false).GetAwaiter().GetResult();
                                }

                                return response;
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e, "The error occurred when trying to get tokens from {TokensProvider}.", tokensProvider.Provider.ToString());
                            }

                            return new List<DexTokenData>();
                        }));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "The error occurred when trying to get tokens from {TokensProvider}.", tokensProvider.Provider.ToString());
                    }

                    var taskResults = await Task.WhenAll(tasks).ConfigureAwait(false);
                    results.AddRange(taskResults.SelectMany(r => r));
                }
            }

            results = results
                .OrderByDescending(r => r.LogoUri)
                .DistinctBy(r => r.Id)
                .OrderBy(t => t.Symbol)
                .ToList();
            return await Result<List<DexTokenData>>.SuccessAsync(results, "Got tokens data.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Result<List<IBlockchainDescriptor>> Blockchains(
            BlockchainType type = BlockchainType.None,
            bool? isEnabled = null)
        {
            var result = _blockchainDescriptors
                .Where(x => !x.IsHided)
                .OrderBy(x => x.Order)
                .ThenBy(x => x.ChainId)
                .AsEnumerable();

            if (isEnabled != null)
            {
                result = result.Where(x => x.IsEnabled == isEnabled);
            }

            if (type != BlockchainType.None)
            {
                result = result.Where(x => x.Type == type);
            }

            return Result<List<IBlockchainDescriptor>>.Success(result.ToList(), "Got the list of all supported blockchains.");
        }

        /// <inheritdoc />
        public async Task<Result<SwapPairDataResponse>> BlockchainSwapPairsAsync(
            BlockchainSwapPairsRequest request)
        {
            if (request.Blockchain == Chain.None)
            {
                return await Result<SwapPairDataResponse>.FailAsync("Should choose the blockchain.").ConfigureAwait(false);
            }

            var blockchainDescriptor = _blockchainDescriptors.Find(b => b.ChainId == (ulong)request.Blockchain);
            if (blockchainDescriptor?.IsEVMCompatible != true)
            {
                return await Result<SwapPairDataResponse>.FailAsync("Used blockchain is not supported yet.").ConfigureAwait(false);
            }

            var validProviders = ProviderServiceExtensions.DexDescriptors
                .Where(p => request.IncludedProviderIds.Count == 0 || (request.IncludedProviderIds.Contains(p!.Provider) && p.Endpoints.Any(x => x?.Blockchain == request.Blockchain)))
                .Where(p => !request.IgnoredProviderIds.Contains(p!.Provider) && p.Endpoints.Any(x => x?.Blockchain == request.Blockchain));

            var errors = new List<string>();
            var tasks = new List<Task<Result<(DexProvider DexProvider, List<ISwapPairData> SwapPairs)>>>();
            foreach (var validProvider in validProviders)
            {
                var provider = _providers
                    .Where(p => p.DexDescriptor?.Provider == validProvider?.Provider && p.Blockchain == request.Blockchain)
                    .Cast<IGetSwapPairs>()
                    .FirstOrDefault();

                if (provider == null)
                {
                    continue;
                }

                var task = provider.GetAllSwapPairsAsync(request.Blockchain, request.First, request.Skip, request.FromCache);
                _logger.LogInformation("Starting getting swap pairs data for {Provider} ({Blockchain}) provider...", validProvider?.Provider.ToString(), request.Blockchain.ToString());
                tasks.Add(task.ContinueWith(o =>
                {
                    if (o.IsFaulted && !string.IsNullOrWhiteSpace(o.Exception?.Message))
                    {
                        return Result<(DexProvider, List<ISwapPairData>)>.Fail(o.Exception.Message);
                    }

                    _logger.LogInformation("Successfully got swap pairs data for {Provider} ({Blockchain}) provider...", validProvider?.Provider.ToString(), request.Blockchain.ToString());
                    return o.Result;
                }));
            }

            if (tasks.Count == 0)
            {
                return await Result<SwapPairDataResponse>.FailAsync($"There are no valid DEX providers for {request.Blockchain.ToString()} blockchain.").ConfigureAwait(false);
            }

            var resultPairsData = new Dictionary<DexDescriptor, List<ISwapPairData>>();
            var taskResults = await Task.WhenAll(tasks).ConfigureAwait(false);

            var dexTokensData = new List<DexTokenData>();
            if (request.UseTokenLists)
            {
                var dexTokensDataResult = await TokensDataAsync(new TokensDataRequest
                {
                    Blockchain = request.Blockchain,
                    IncludeUniversalTokenLists = request.IncludeUniversalTokenLists,
                    FromCache = true
                }).ConfigureAwait(false);

                if (dexTokensDataResult.Succeeded)
                {
                    dexTokensData = dexTokensDataResult.Data;
                }
            }

            foreach (var taskResult in taskResults.Where(r => r.Succeeded))
            {
                var dexDescriptor =
                    ProviderServiceExtensions.DexDescriptors.FirstOrDefault(p => p?.Provider == taskResult.Data.DexProvider);
                if (dexDescriptor != null)
                {
                    var endpointsToRemove = new List<DexEndpoint?>();
                    endpointsToRemove.AddRange(dexDescriptor.Endpoints.Where(e => e?.Blockchain != request.Blockchain));
                    foreach (var endpointToRemove in endpointsToRemove)
                    {
                        dexDescriptor.Endpoints.Remove(endpointToRemove);
                    }

                    var swapPairs = taskResult.Data.SwapPairs.OrderByDescending(p => p.SyncAtDateTime).ToList();
                    if (request.UseTokenLists)
                    {
                        foreach (var swapPair in swapPairs)
                        {
                            swapPair.Token0 = dexTokensData?
                                .Find(t => t.Id?.Equals(swapPair.Token0?.Id, StringComparison.OrdinalIgnoreCase) == true) ?? swapPair.Token0;
                            swapPair.Token1 = dexTokensData?
                                .Find(t => t.Id?.Equals(swapPair.Token1?.Id, StringComparison.OrdinalIgnoreCase) == true) ?? swapPair.Token1;
                        }
                    }

                    resultPairsData.Add(new DexDescriptor(dexDescriptor), swapPairs);
                }
            }

            errors.AddRange(taskResults
                .Where(r => !r.Succeeded && r.Data.DexProvider != DexProvider.None)
                .SelectMany(x => x.Messages.Select(m => $"{x.Data.DexProvider.ToString()}: {m}")));
            if (resultPairsData.Count == 0)
            {
                return await Result<SwapPairDataResponse>.FailAsync(errors).ConfigureAwait(false);
            }

            var response = new SwapPairDataResponse(
                resultPairsData.Select(d => new DexSwapPairsData(d.Key, d.Value)).ToList(),
                blockchainDescriptor);
            var messages = new List<string>
            {
                "Got the list of swap pairs from supported DEXes by blockchain."
            };
            messages.AddRange(errors);
            return await Result<SwapPairDataResponse>.SuccessAsync(response, messages).ConfigureAwait(false);
        }
    }
}