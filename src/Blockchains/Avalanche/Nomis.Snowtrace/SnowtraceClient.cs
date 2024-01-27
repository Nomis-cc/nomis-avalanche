// ------------------------------------------------------------------------------------------------------
// <copyright file="SnowtraceClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions.Clients;
using Nomis.Snowtrace.Interfaces;
using Nomis.Snowtrace.Settings;
using Nomis.Utils.Contracts;

namespace Nomis.Snowtrace
{
    /// <inheritdoc cref="ISnowtraceClient"/>
    internal sealed class SnowtraceClient :
        BaseEvmClient<SnowtraceSettings>,
        ISnowtraceClient
    {
        /// <summary>
        /// Initialize <see cref="SnowtraceClient"/>.
        /// </summary>
        /// <param name="settings"><see cref="SnowtraceSettings"/>.</param>
        /// <param name="apiKeysPool"><see cref="IValuePool{TService,TValue}"/>.</param>
        /// <param name="client"><see cref="HttpClient"/>.</param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/>.</param>
        public SnowtraceClient(
            SnowtraceSettings settings,
            IValuePool<SnowtraceService, string> apiKeysPool,
            HttpClient client,
            ILogger<SnowtraceClient> logger)
            : base(settings, client, logger, apiKeysPool.GetNextValue(), settings.AppendedPath)
        {
        }
    }
}