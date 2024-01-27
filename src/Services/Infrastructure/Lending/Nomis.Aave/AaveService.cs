// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Microsoft.Extensions.Options;
using Nethereum.Util;
using Nethereum.Web3;
using Nomis.Aave.Contracts;
using Nomis.Aave.Interfaces;
using Nomis.Aave.Interfaces.Enums;
using Nomis.Aave.Interfaces.Responses;
using Nomis.Aave.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.Aave
{
    /// <inheritdoc cref="IAaveService"/>
    internal sealed class AaveService :
        IAaveService,
        ISingletonService
    {
        private readonly AaveSettings _settings;
        private readonly Dictionary<AaveChain, Web3> _nethereumClients;

        /// <summary>
        /// Initialize <see cref="AaveService"/>.
        /// </summary>
        /// <param name="settings"><see cref="AaveSettings"/>.</param>
        public AaveService(
            IOptions<AaveSettings> settings)
        {
            _settings = settings.Value;
            _nethereumClients = new Dictionary<AaveChain, Web3>();
            foreach (var chain in Enum.GetValues<AaveChain>())
            {
                _nethereumClients.Add(chain, new Web3(_settings.DataFeeds?.Find(a => a.Blockchain == chain)?.RpcUrl ?? "http://localhost:8545")
                {
                    TransactionManager =
                    {
                        DefaultGasPrice = new(0x4c4b40),
                        DefaultGas = new(0x4c4b40)
                    }
                });
            }
        }

        /// <inheritdoc />
        public async Task<Result<AaveUserAccountDataResponse>> GetAaveUserAccountDataAsync(
            AaveChain blockchain,
            string address)
        {
            if (!new AddressUtil().IsValidAddressLength(address) || !new AddressUtil().IsValidEthereumAddressHexFormat(address))
            {
                throw new InvalidAddressException(address);
            }

            var contractsData = _settings.DataFeeds?.Find(a => a.Blockchain == blockchain);
            if (!new AddressUtil().IsValidAddressLength(contractsData?.PoolContractAddress))
            {
                throw new CustomException("Invalid contract address", statusCode: HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(contractsData?.PoolContractAbi))
            {
                throw new CustomException("ABI must be set", statusCode: HttpStatusCode.BadRequest);
            }

            var nethereumClient = _nethereumClients[blockchain];
            var contract = nethereumClient.Eth.GetContract(contractsData.PoolContractAbi, contractsData.PoolContractAddress);
            var function = contract.GetFunction("getUserAccountData");

            var result = await function.CallDeserializingToObjectAsync<AaveUserAccountData>(address).ConfigureAwait(false);
            return await Result<AaveUserAccountDataResponse>.SuccessAsync(
                new AaveUserAccountDataResponse
                {
                    TotalCollateralBase = result.TotalCollateralBase.ToString(),
                    TotalDebtBase = result.TotalDebtBase.ToString(),
                    AvailableBorrowsBase = result.AvailableBorrowsBase.ToString(),
                    CurrentLiquidationThreshold = result.CurrentLiquidationThreshold.ToString(),
                    Ltv = result.Ltv.ToString(),
                    HealthFactor = result.HealthFactor.ToString()
                }, "Got Aave user account data.").ConfigureAwait(false);
        }
    }
}