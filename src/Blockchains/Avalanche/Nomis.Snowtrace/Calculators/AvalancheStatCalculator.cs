// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheStatCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nomis.Aave.Interfaces.Calculators;
using Nomis.Aave.Interfaces.Responses;
using Nomis.Blockchain.Abstractions.Calculators;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Contracts.Models;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Chainanalysis.Interfaces.Models;
using Nomis.CyberConnect.Interfaces.Responses;
using Nomis.Dex.Abstractions.Calculators;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Greysafe.Interfaces.Models;
using Nomis.Snapshot.Interfaces.Responses;
using Nomis.Snowtrace.Interfaces.Extensions;
using Nomis.Snowtrace.Interfaces.Models;
using Nomis.Tally.Interfaces.Models;
using Nomis.Utils.Contracts.Calculators;

namespace Nomis.Snowtrace.Calculators
{
    /// <summary>
    /// Avalanche wallet stats calculator.
    /// </summary>
    internal sealed class AvalancheStatCalculator :
        BaseEvmStatCalculator<AvalancheWalletStats, AvalancheTransactionIntervalData>,
        IWalletNftStatsCalculator<AvalancheWalletStats, AvalancheTransactionIntervalData>,
        IWalletAaveStatsCalculator<AvalancheWalletStats, AvalancheTransactionIntervalData>,
        IWalletDexTokenSwapPairsStatsCalculator<AvalancheWalletStats, AvalancheTransactionIntervalData>
    {
        private readonly string _address;
        private readonly IEnumerable<BaseEvmInternalTransaction> _internalTransactions;
        private readonly IEnumerable<INFTTokenTransfer> _tokenTransfers;
        private readonly IEnumerable<DexTokenSwapPairsData> _dexTokenSwapPairs;

        /// <inheritdoc />
        public AaveUserAccountDataResponse? AaveData { get; }

        /// <inheritdoc />
        public IEnumerable<DexTokenSwapPairsData>? DexTokensSwapPairs => _dexTokenSwapPairs.Any() ? _dexTokenSwapPairs : null;

        public AvalancheStatCalculator(
            string address,
            decimal balance,
            decimal usdBalance,
            decimal medianUsdBalance,
            IEnumerable<BaseEvmNormalTransaction> transactions,
            IEnumerable<BaseEvmInternalTransaction> internalTransactions,
            IEnumerable<INFTTokenTransfer> tokenTransfers,
            IEnumerable<BaseEvmERC20TokenTransfer> erc20TokenTransfers,
            SnapshotData? snapshotData,
            TallyAccount? tallyAccount,
            IEnumerable<TokenDataBalance>? tokenDataBalances,
            IEnumerable<DexTokenSwapPairsData> dexTokenSwapPairs,
            AaveUserAccountDataResponse? aaveUserAccountData,
            IEnumerable<GreysafeReport>? greysafeReports,
            IEnumerable<ChainanalysisReport>? chainanalysisReports,
            CyberConnectData? cyberConnectData,
            IEnumerable<TransferTokenDataBalance>? transferTokenDataBalances = null)
            : base(address, balance, usdBalance, medianUsdBalance, transactions, erc20TokenTransfers, snapshotData, tallyAccount, tokenDataBalances, greysafeReports, chainanalysisReports, cyberConnectData, value => value.ToAvax(), transferTokenDataBalances)
        {
            _address = address;
            _internalTransactions = internalTransactions;
            _tokenTransfers = tokenTransfers;
            _dexTokenSwapPairs = dexTokenSwapPairs;
            AaveData = aaveUserAccountData;
        }

        /// <inheritdoc />
        IWalletNftStats IWalletNftStatsCalculator<AvalancheWalletStats, AvalancheTransactionIntervalData>.Stats()
        {
            var soldTokens = _tokenTransfers.Where(x => x.From?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true).ToList();
            var soldSum = IWalletStatsCalculator
                .TokensSum(soldTokens.Select(x => x.Hash!), _internalTransactions.Select(x => (x.Hash!, BigInteger.TryParse(x.Value, out var amount) ? amount : 0)));

            var soldTokensIds = soldTokens.Select(x => x.GetTokenUid());
            var buyTokens = _tokenTransfers.Where(x => x.To?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && soldTokensIds.Contains(x.GetTokenUid()));
            var buySum = IWalletStatsCalculator
                .TokensSum(buyTokens.Select(x => x.Hash!), _internalTransactions.Select(x => (x.Hash!, BigInteger.TryParse(x.Value, out var amount) ? amount : 0)));

            var buyNotSoldTokens = _tokenTransfers.Where(x => x.To?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && !soldTokensIds.Contains(x.GetTokenUid()));
            var buyNotSoldSum = IWalletStatsCalculator
                .TokensSum(buyNotSoldTokens.Select(x => x.Hash!), _internalTransactions.Select(x => (x.Hash!, BigInteger.TryParse(x.Value, out var amount) ? amount : 0)));

            int holdingTokens = _tokenTransfers.Count() - soldTokens.Count;
            decimal nftWorth = buySum == 0 ? 0 : soldSum.ToNative() / buySum.ToNative() * buyNotSoldSum.ToNative();

            return new AvalancheWalletStats
            {
                NFTHolding = holdingTokens,
                NFTTrading = (soldSum - buySum).ToNative(),
                NFTWorth = nftWorth
            };
        }
    }
}