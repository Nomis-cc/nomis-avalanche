// ------------------------------------------------------------------------------------------------------
// <copyright file="ApiCommonSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Common.Settings
{
    /// <summary>
    /// API common settings.
    /// </summary>
    public class ApiCommonSettings :
        ISettings
    {
        /// <summary>
        /// Use Swagger.
        /// </summary>
        public bool UseSwagger { get; init; }

        /// <summary>
        /// Use Swagger caching.
        /// </summary>
        public bool UseSwaggerCaching { get; init; }

        /// <summary>
        /// Sink logs to console by Serilog.
        /// </summary>
        public bool SerilogSinkToConsole { get; init; }

        /// <summary>
        /// Serilog output template for console logging.
        /// </summary>
        public string SerilogConsoleOutputTemplate { get; init; } = null!;
    }
}