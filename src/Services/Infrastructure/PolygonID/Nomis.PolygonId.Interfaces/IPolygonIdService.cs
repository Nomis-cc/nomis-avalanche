// ------------------------------------------------------------------------------------------------------
// <copyright file="IPolygonIdService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.PolygonId.Interfaces.Contracts;
using Nomis.PolygonId.Interfaces.PolygonIdIssuerNode;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Contracts.Stats;
using Nomis.Utils.Wrapper;

namespace Nomis.PolygonId.Interfaces
{
    /// <summary>
    /// PolygonID API service.
    /// </summary>
    public interface IPolygonIdService :
        IInfrastructureService
    {
        /// <summary>
        /// PolygonID issuer client.
        /// </summary>
        public IPolygonIdIssuerClient Client { get; }

        /// <summary>
        /// Create claim and get QR code.
        /// </summary>
        /// <param name="request">Wallet stats request.</param>
        /// <param name="mintedScore">Nomis minted score value.</param>
        /// <param name="walletStats">Wallet stats.</param>
        /// <param name="expiration">Expiration timestamp.</param>
        /// <param name="scoredChain">Blockchain in which the score will be calculated.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <param name="multichainBlockchains">Blockchains in which the score will be calculated for multichain score.</param>
        /// <returns>Returns created claim QR code.</returns>
        public Task<Result<DIDData?>> CreateClaimAndGetQrAsync<TWalletStatsRequest, TWalletStats, TTransactionIntervalData>(
            TWalletStatsRequest request, ushort mintedScore, TWalletStats walletStats, long expiration, ulong scoredChain, CancellationToken cancellationToken, string? multichainBlockchains = null)
            where TWalletStatsRequest : WalletStatsRequest, IWalletPolygonIdRequest
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData;
    }
}