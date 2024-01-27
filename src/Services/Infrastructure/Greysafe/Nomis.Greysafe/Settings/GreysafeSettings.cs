// ------------------------------------------------------------------------------------------------------
// <copyright file="GreysafeSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Greysafe.Settings
{
    /// <summary>
    /// Greysafe scam reporting service settings.
    /// </summary>
    internal class GreysafeSettings :
        ISettings
    {
        /// <summary>
        /// Greysafe API URL.
        /// </summary>
        public string? ApiBaseUrl { get; init; }
    }
}