// ------------------------------------------------------------------------------------------------------
// <copyright file="IBaseEvmClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts.Models;

namespace Nomis.Blockchain.Abstractions.Clients
{
    /// <summary>
    /// Base EVM client.
    /// </summary>
    public interface IBaseEvmClient
    {
        /// <summary>
        /// Get the account balance in Wei.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns <see cref="BaseEvmAccount"/>.</returns>
        Task<BaseEvmAccount> GetBalanceAsync(string address);

        /// <summary>
        /// Get the account token balance in Wei.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <param name="contractAddress">Token contract address.</param>
        /// <returns>Returns <see cref="BaseEvmAccount"/>.</returns>
        Task<BaseEvmAccount> GetTokenBalanceAsync(string address, string contractAddress);

        /// <summary>
        /// Get list of specific transactions/transfers of the given account.
        /// </summary>
        /// <typeparam name="TResult">The type of returned response.</typeparam>
        /// <typeparam name="TResultItem">The type of returned response data items.</typeparam>
        /// <param name="address">Account address.</param>
        /// <param name="startBlockParameterName">Start block parameter name.</param>
        /// <param name="endBLockParameterName">End block parameter name.</param>
        /// <returns>Returns list of specific transactions/transfers of the given account.</returns>
        Task<IEnumerable<TResultItem>> GetTransactionsAsync<TResult, TResultItem>(
            string address,
            string? startBlockParameterName = "startblock",
            string? endBLockParameterName = "endblock")
            where TResult : IBaseEvmTransferList<TResultItem>
            where TResultItem : IBaseEvmTransfer;
    }
}