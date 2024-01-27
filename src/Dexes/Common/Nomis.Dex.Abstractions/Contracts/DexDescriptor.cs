// ------------------------------------------------------------------------------------------------------
// <copyright file="DexDescriptor.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Enums;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <inheritdoc cref="IDexDescriptor"/>
    public class DexDescriptor :
        IDexDescriptor
    {
        /// <summary>
        /// Initialize <see cref="DexDescriptor"/>.
        /// </summary>
        public DexDescriptor()
        {
            // for serializers
        }

        /// <summary>
        /// Initialize <see cref="DexDescriptor"/>.
        /// </summary>
        /// <param name="dexDescriptor"><see cref="IDexDescriptor"/>.</param>
        public DexDescriptor(
            IDexDescriptor dexDescriptor)
        {
            IsEnabled = dexDescriptor.IsEnabled;
            UseCaching = dexDescriptor.UseCaching;
            Provider = dexDescriptor.Provider;
            Type = dexDescriptor.Type;
            Name = dexDescriptor.Name;
            Slug = dexDescriptor.Slug;
            AppUri = dexDescriptor.AppUri;
            DocsUri = dexDescriptor.DocsUri;
            FeeInPercent = dexDescriptor.FeeInPercent;
            FeeDocsUri = dexDescriptor.FeeDocsUri;
            Endpoints = dexDescriptor.Endpoints;
            Icon = dexDescriptor.Icon;
        }

        /// <inheritdoc />
        public bool IsEnabled { get; set; }

        /// <inheritdoc />
        public bool UseCaching { get; set; }

        /// <inheritdoc />
        public DexProvider Provider { get; set; }

        /// <inheritdoc />
        public DexType Type { get; set; }

        /// <inheritdoc />
        public string TypeName => Type.ToString();

        /// <inheritdoc />
        public string? Name { get; set; }

        /// <inheritdoc />
        public string? Slug { get; set; }

        /// <inheritdoc />
        public string? AppUri { get; set; }

        /// <inheritdoc />
        public string? DocsUri { get; set; }

        /// <inheritdoc />
        public decimal FeeInPercent { get; set; }

        /// <inheritdoc />
        public string? FeeDocsUri { get; set; }

        /// <inheritdoc />
        public IList<DexEndpoint?> Endpoints { get; set; } = new List<DexEndpoint?>();

        /// <inheritdoc />
        public IDictionary<Chain, string> RelativeApiPaths =>
            Endpoints.ToDictionary(x => x!.Blockchain, y => $"/api/v1/dex/aggregator/swap-pairs?Blockchain={(ulong?)y!.Blockchain}&IncludedProviderIds={(ulong)Provider}");

        /// <inheritdoc />
        public string? Icon { get; set; }
    }
}