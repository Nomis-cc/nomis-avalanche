// ------------------------------------------------------------------------------------------------------
// <copyright file="IDexBaseService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Dex.Abstractions
{
    /// <summary>
    /// Base service for getting DEX data.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedTypeParameter
    public interface IDexBaseService :
        IInfrastructureService,
        IGetSwapPairs,
        IHasSwapPairCacheKeys
    {
        /// <inheritdoc cref="IDexDescriptor"/>
        public IDexDescriptor? DexDescriptor { get; }

        /// <inheritdoc cref="DexEndpoint.Blockchain"/>
        public Chain Blockchain { get; }
    }
}