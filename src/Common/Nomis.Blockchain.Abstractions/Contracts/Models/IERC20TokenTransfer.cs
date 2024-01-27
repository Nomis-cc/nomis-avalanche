// ------------------------------------------------------------------------------------------------------
// <copyright file="IERC20TokenTransfer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// ERC-20 token transfer.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IERC20TokenTransfer :
        IHasContractAddress,
        IHasHash,
        IHasTo,
        IHasFrom,
        IHasValue<string?>
    {
    }
}