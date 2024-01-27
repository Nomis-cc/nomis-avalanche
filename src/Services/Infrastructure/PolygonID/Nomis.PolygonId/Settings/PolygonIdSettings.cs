// ------------------------------------------------------------------------------------------------------
// <copyright file="PolygonIdSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.PolygonId.Settings
{
    /// <summary>
    /// PolygonId settings.
    /// </summary>
    internal class PolygonIdSettings :
        ISettings
    {
        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://0xpolygonid.github.io/tutorials/issuer-node/issuer-node-api/identity/apis/"/>
        /// </remarks>
        public string? ApiBaseUrl { get; init; }

        /// <summary>
        /// Issuer basic auth username.
        /// </summary>
        public string? IssuerBasicAuthUsername { get; init; }

        /// <summary>
        /// Issuer basic auth password.
        /// </summary>
        public string? IssuerBasicAuthPassword { get; init; }

        /// <summary>
        /// Issuer DID.
        /// </summary>
        public string? IssuerDid { get; init; }

        /// <summary>
        /// Nomis credential schema.
        /// </summary>
        public string? NomisCredentialSchema { get; init; }

        /// <summary>
        /// Nomis credential type.
        /// </summary>
        public string? NomisCredentialType { get; init; }
    }
}