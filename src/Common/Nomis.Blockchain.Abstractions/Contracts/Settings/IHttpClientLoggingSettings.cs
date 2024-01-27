// ------------------------------------------------------------------------------------------------------
// <copyright file="IHttpClientLoggingSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Contracts.Settings
{
    /// <summary>
    /// <see cref="HttpClient"/> logging settings.
    /// </summary>
    public interface IHttpClientLoggingSettings
    {
        /// <summary>
        /// Use <see cref="HttpClient"/> logging.
        /// </summary>
        public bool UseHttpClientLogging { get; init; }
    }
}