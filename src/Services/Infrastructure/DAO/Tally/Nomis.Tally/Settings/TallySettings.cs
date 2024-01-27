// ------------------------------------------------------------------------------------------------------
// <copyright file="TallySettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Tally.Interfaces.Settings;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Tally.Settings
{
    /// <summary>
    /// Tally settings.
    /// </summary>
    internal class TallySettings :
        ITallySettings,
        ISettings
    {
        /// <summary>
        /// Tally API key.
        /// </summary>
        public string? ApiKey { get; init; }

        /// <summary>
        /// Tally API base address.
        /// </summary>
        public string? ApiBaseUrl { get; init; }

        /// <inheritdoc />
        public IList<ulong> SupportedChainIds { get; init; } = new List<ulong>();
    }
}