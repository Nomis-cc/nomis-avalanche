// ------------------------------------------------------------------------------------------------------
// <copyright file="IDexDescriptor.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;

namespace Nomis.Dex.Abstractions
{
    /// <summary>
    /// DEX descriptor.
    /// </summary>
    public interface IDexDescriptor
    {
        /// <summary>
        /// Is enabled.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Use caching for getting swap pairs.
        /// </summary>
        public bool UseCaching { get; }

        /// <summary>
        /// DEX provider.
        /// </summary>
        public DexProvider Provider { get; }

        /// <summary>
        /// DEX type.
        /// </summary>
        public DexType Type { get; }

        /// <summary>
        /// DEX type name.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// DEX name.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// DEX slug.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// DEX application URI.
        /// </summary>
        public string? AppUri { get; }

        /// <summary>
        /// URI to documentation.
        /// </summary>
        public string? DocsUri { get; }

        /// <summary>
        /// Exchange commission percentage.
        /// </summary>
        public decimal FeeInPercent { get; }

        /// <summary>
        /// URI to documentation mentioning commission percentage.
        /// </summary>
        public string? FeeDocsUri { get; }

        /// <summary>
        /// List of DEX endpoint with API URL and main contracts for specific blockchain.
        /// </summary>
        public IList<DexEndpoint?> Endpoints { get; }

        /// <summary>
        /// Relative API paths for getting swap pairs.
        /// </summary>
        public IDictionary<Chain, string> RelativeApiPaths { get; }

        /// <summary>
        /// Icon.
        /// </summary>
        public string? Icon { get; set; }
    }
}