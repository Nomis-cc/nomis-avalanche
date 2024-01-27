// ------------------------------------------------------------------------------------------------------
// <copyright file="UpdateSwapPairsCacheBackgroundService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces;
using Nomis.DexProviderService.Settings;

namespace Nomis.DexProviderService.Background
{
    /// <summary>
    /// Background service for updating the DEX swap pairs cache.
    /// </summary>
    internal sealed class UpdateSwapPairsCacheBackgroundService :
        BackgroundService
    {
        private readonly DexProviderSettings _dexProviderSettings;
        private readonly IDexProviderService _dexProviderService;
        private readonly ILogger<UpdateSwapPairsCacheBackgroundService> _logger;

        /// <summary>
        /// Initialize <see cref="UpdateSwapPairsCacheBackgroundService"/>.
        /// </summary>
        /// <param name="dexProviderOptions"><see cref="DexProviderSettings"/>.</param>
        /// <param name="dexProviderService"><see cref="IDexProviderService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public UpdateSwapPairsCacheBackgroundService(
            IOptions<DexProviderSettings> dexProviderOptions,
            IDexProviderService dexProviderService,
            ILogger<UpdateSwapPairsCacheBackgroundService> logger)
        {
            _dexProviderSettings = dexProviderOptions.Value;
            _dexProviderService = dexProviderService ?? throw new ArgumentNullException(nameof(dexProviderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_dexProviderSettings.UseBackgroundCacheUpdater)
            {
                while (true)
                {
                    await Task.Delay((int)_dexProviderSettings.Delay.TotalMilliseconds, stoppingToken).ConfigureAwait(false);
                    _logger.LogInformation("Start next cache update (swap pairs): {Date}", DateTime.UtcNow.ToString("O"));

                    var providers = _dexProviderService.ProvidersData();
                    if (providers.Succeeded)
                    {
                        foreach (var blockchain in providers.Data.Select(x => x.BlockсhainDescriptor).Distinct())
                        {
                            _ = _dexProviderService.BlockchainSwapPairsAsync(new()
                            {
                                Blockchain = (Chain)blockchain!.ChainId,
                                FromCache = false,
                                IgnoredProviderIds = new List<DexProvider>(),
                                IncludedProviderIds = new List<DexProvider>()
                            });
                        }
                    }
                }
            }
        }
    }
}