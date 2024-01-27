// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.LensProtocol.Settings
{
    /// <summary>
    /// Lens Protocol settings.
    /// </summary>
    internal class LensProtocolSettings :
        ISettings
    {
        /// <summary>
        /// Lens Protocol API base address.
        /// </summary>
        /// <remarks>
        /// <see href="https://docs.lens.xyz/docs/developer-quickstart"/>
        /// </remarks>
        public string? ApiBaseUrl { get; init; }
    }
}