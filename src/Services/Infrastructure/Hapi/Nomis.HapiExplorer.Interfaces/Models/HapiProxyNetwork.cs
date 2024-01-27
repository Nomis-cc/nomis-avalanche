// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyNetwork.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.HapiExplorer.Interfaces.Models
{
    /// <summary>
    /// HAPI Proxy network data.
    /// </summary>
    public class HapiProxyNetwork
    {
        /// <summary>
        /// Network name.
        /// </summary>
        /// <example>solana</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}