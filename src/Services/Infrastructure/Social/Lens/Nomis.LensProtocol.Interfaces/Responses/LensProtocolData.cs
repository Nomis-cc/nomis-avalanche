// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.LensProtocol.Interfaces.Models;

namespace Nomis.LensProtocol.Interfaces.Responses
{
    /// <summary>
    /// LensProtocol data.
    /// </summary>
    public class LensProtocolData
    {
        /// <inheritdoc cref="LensProtocolProfileData"/>
        public LensProtocolProfileData? Profile { get; set; }
    }
}