// ------------------------------------------------------------------------------------------------------
// <copyright file="IIPFSSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.IPFS.Interfaces.Settings
{
    /// <summary>
    /// IPFS settings.
    /// </summary>
    public interface IIPFSSettings
    {
        /// <summary>
        /// IPFS gateway URL template.
        /// </summary>
        public string? IpfsGatewayUrlTemplate { get; init; }
    }
}