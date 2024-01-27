// ------------------------------------------------------------------------------------------------------
// <copyright file="CorsSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

// ReSharper disable InconsistentNaming
namespace Nomis.Api.Common.Settings
{
    /// <summary>
    /// CORS settings.
    /// </summary>
    public class CorsSettings :
        ISettings
    {
        /// <summary>
        /// Use CORS.
        /// </summary>
        public bool UseCORS { get; init; }

        /// <summary>
        /// Policy origins.
        /// </summary>
        public IDictionary<string, List<string>> PolicyOrigins { get; init; } = new Dictionary<string, List<string>>();
    }
}