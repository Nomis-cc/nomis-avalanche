// ------------------------------------------------------------------------------------------------------
// <copyright file="ProviderServiceExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.CacheProviderService.Interfaces;
using Nomis.Dex.Abstractions;
using Nomis.Dex.Common;
using Nomis.DexProviderService.Background;
using Nomis.DexProviderService.Interfaces;
using Nomis.DexProviderService.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.DexProviderService.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ProviderServiceExtensions
    {
        /// <summary>
        /// List of supported DEX descriptors.
        /// </summary>
        public static IList<IDexDescriptor?> DexDescriptors { get; private set; } = new List<IDexDescriptor?>();

        /// <summary>
        /// Add DEX provider service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDexProviderService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            services
                .AddSettings<DexProviderSettings>(configuration)
                .AddSettings<StableCoinSettings>(configuration)
                .AddSettings<TokensProvidersSettings>(configuration);

            var settings = configuration.GetSettings<DexProviderSettings>();
            var stableCoinSettings = configuration.GetSettings<StableCoinSettings>();
            var tokensProvidersSettings = configuration.GetSettings<TokensProvidersSettings>();

            DexDescriptors = settings.DexDescriptors?.Cast<IDexDescriptor?>().ToList() ?? new List<IDexDescriptor?>();

            var blockchainSettingsTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes()
                    .Where(type => type.IsClass && typeof(IBlockchainSettings).IsAssignableFrom(type)))
                .ToList();
            var blockchainSettings = blockchainSettingsTypes
                .Select(t => configuration.GetSettings(t))
                .Where(s => s != null)
                .Cast<IBlockchainSettings>()
                .ToList();

            var mainnetBlockchainDescriptors = blockchainSettings
#pragma warning disable S3358
                .Select(s => s.BlockchainDescriptors.TryGetValue(BlockchainKind.Mainnet, out var mainnetBlockchainDescriptor) ? mainnetBlockchainDescriptor : null)
                .Where(d => d != null)
#pragma warning restore S3358
                .Cast<IBlockchainDescriptor>()
                .ToList();

            var testnetBlockchainDescriptors = blockchainSettings
#pragma warning disable S3358
                .Select(s => s.BlockchainDescriptors.TryGetValue(BlockchainKind.Testnet, out var testnetBlockchainDescriptor) ? testnetBlockchainDescriptor : null)
                .Where(d => d != null)
#pragma warning restore S3358
                .Cast<IBlockchainDescriptor>()
                .ToList();

            var blockchainDescriptors = mainnetBlockchainDescriptors.Union(testnetBlockchainDescriptors).ToList();

            var providers = new List<IDexBaseService>();
            foreach (var dexDescriptor in DexDescriptors)
            {
                if (dexDescriptor?.IsEnabled != true)
                {
                    continue;
                }

                var cacheProvider = serviceProvider.GetRequiredService<ICacheProviderService>();
                foreach (var dexDescriptorContractData in dexDescriptor.Endpoints)
                {
                    if (string.IsNullOrWhiteSpace(dexDescriptorContractData?.ApiUri))
                    {
                        continue;
                    }

                    var graphQlOptions = new GraphQLHttpClientOptions
                    {
                        EndPoint = new(dexDescriptorContractData.ApiUri)
                    };
                    var graphQlClient = new GraphQLHttpClient(graphQlOptions, new SystemTextJsonSerializer());
                    providers.Add(new DexBaseService(graphQlClient, cacheProvider, dexDescriptor, dexDescriptorContractData.Blockchain));
                }
            }

            services.AddSingleton<IDexProviderService>(_ =>
            {
                var logger = services.BuildServiceProvider().GetRequiredService<ILogger<DexProviderService>>();
                var cacheProvider = serviceProvider.GetRequiredService<ICacheProviderService>();
                return new DexProviderService(tokensProvidersSettings, blockchainDescriptors, stableCoinSettings.Stablecoins.ToList(), providers, cacheProvider, logger);
            });

            if (settings.UseBackgroundCacheUpdater)
            {
                services
                    .AddHostedService<UpdateSwapPairsCacheBackgroundService>();
            }

            return services;
        }
    }
}