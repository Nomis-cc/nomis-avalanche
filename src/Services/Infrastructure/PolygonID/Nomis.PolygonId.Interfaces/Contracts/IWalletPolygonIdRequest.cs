// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletPolygonIdRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.PolygonId.Interfaces.Contracts
{
    /// <summary>
    /// Wallet polygon id request.
    /// </summary>
    public interface IWalletPolygonIdRequest
    {
        /// <summary>
        /// Use DID storage for score data.
        /// </summary>
        public bool UseDIDStorage { get; set; }

        /// <summary>
        /// User DID.
        /// </summary>
        public string? DID { get; set; }
    }
}