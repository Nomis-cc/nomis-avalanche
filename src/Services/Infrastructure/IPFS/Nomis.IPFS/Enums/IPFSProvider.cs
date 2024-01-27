// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFSProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.IPFS.Enums
{
    /// <summary>
    /// IPFS provider.
    /// </summary>
    public enum IPFSProvider
    {
        /// <summary>
        /// Local node.
        /// </summary>
        LocalNode = 0,

        /// <summary>
        /// Web3 storage.
        /// </summary>
        Web3Storage,

        /// <summary>
        /// Pinata.
        /// </summary>
        Pinata
    }
}