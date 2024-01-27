// ------------------------------------------------------------------------------------------------------
// <copyright file="IInternalTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Properties;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// Internal transaction.
    /// </summary>
    public interface IInternalTransaction :
        IHasTimestamp,
        IHasContractAddress,
        IHasHash,
        IHasFrom,
        IHasTo,
        IHasValue
    {
    }
}