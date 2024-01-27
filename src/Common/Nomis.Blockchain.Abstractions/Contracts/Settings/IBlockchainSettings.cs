// ------------------------------------------------------------------------------------------------------
// <copyright file="IBlockchainSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Blockchain.Abstractions.Contracts.Settings
{
    /// <summary>
    /// Blockchain settings.
    /// </summary>
    public interface IBlockchainSettings :
        ISettings
    {
        /// <summary>
        /// Items fetch limit per request.
        /// </summary>
        public int? ItemsFetchLimitPerRequest { get; init; }

        /// <summary>
        /// Counted transaction limit.
        /// </summary>
        public int? TransactionsLimit { get; init; }

        /// <summary>
        /// Blockchain descriptors.
        /// </summary>
        public IDictionary<BlockchainKind, BlockchainDescriptor> BlockchainDescriptors { get; init; }
    }
}