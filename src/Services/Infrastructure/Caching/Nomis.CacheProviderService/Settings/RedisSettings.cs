// ------------------------------------------------------------------------------------------------------
// <copyright file="RedisSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.CacheProviderService.Settings
{
    /// <summary>
    /// Redis settings.
    /// </summary>
    public class RedisSettings :
        ISettings
    {
        /// <summary>
        /// Use Redis.
        /// </summary>
        public bool UseRedis { get; init; }

        /// <summary>
        /// Connection string.
        /// </summary>
        public string? ConnectionString { get; init; }

        /// <summary>
        /// Password.
        /// </summary>
        public string? Password { get; init; }

        /// <summary>
        /// Redis instance name.
        /// </summary>
        public string? InstanceName { get; init; }
    }
}