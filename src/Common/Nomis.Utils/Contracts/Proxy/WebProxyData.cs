// ------------------------------------------------------------------------------------------------------
// <copyright file="WebProxyData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Contracts.Proxy
{
    /// <summary>
    /// Web-proxy data.
    /// </summary>
    public class WebProxyData
    {
        /// <summary>
        /// Is enabled.
        /// </summary>
        public bool IsEnabled { get; init; }

        /// <summary>
        /// Proxy id.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Proxy URI.
        /// </summary>
        /// <remarks>
        /// Example: http://localhost:8888.
        /// </remarks>
        public Uri? Uri { get; init; }

        /// <summary>
        /// Proxy group.
        /// </summary>
        public string? Group { get; init; }

        /// <summary>
        /// Use credentials.
        /// </summary>
        public bool UseCredentials { get; init; }

        /// <summary>
        /// Proxy username credential.
        /// </summary>
        public string? UserName { get; init; }

        /// <summary>
        /// Proxy password credential.
        /// </summary>
        public string? Password { get; init; }
    }
}