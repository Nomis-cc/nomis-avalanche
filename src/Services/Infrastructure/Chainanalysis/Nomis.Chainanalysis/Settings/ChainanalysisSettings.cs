// ------------------------------------------------------------------------------------------------------
// <copyright file="ChainanalysisSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Chainanalysis.Settings
{
    /// <summary>
    /// Chainanalysis sanctions reporting service settings.
    /// </summary>
    internal class ChainanalysisSettings :
        ISettings
    {
        /// <summary>
        /// API key for Chainanalysis.
        /// </summary>
        public string? ApiKey { get; init; }

        /// <summary>
        /// Chainanalysis API URL.
        /// </summary>
        public string? ApiBaseUrl { get; init; }
    }
}