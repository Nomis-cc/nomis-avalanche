// ------------------------------------------------------------------------------------------------------
// <copyright file="DexTokenSwapPairsData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Dex.Abstractions.Responses;

namespace Nomis.Dex.Abstractions.Contracts
{
    /// <summary>
    /// DEX token swap pairs data.
    /// </summary>
    public class DexTokenSwapPairsData
    {
        /// <summary>
        /// Initialize <see cref="DexTokenSwapPairsData"/>.
        /// </summary>
        /// <param name="token">Token data.</param>
        /// <param name="tokenBaseBalance">Token base balance (in Wei for ex.).</param>
        /// <param name="tokenSwapPairs">Token DEX swap pairs.</param>
        public DexTokenSwapPairsData(
            TokenData token,
            BigInteger tokenBaseBalance,
            List<DexSwapPairsData> tokenSwapPairs)
        {
            Token = token;
            TokenBaseBalance = tokenBaseBalance;
            TokenSwapPairs = tokenSwapPairs;
        }

        /// <summary>
        /// Token data.
        /// </summary>
        public TokenData Token { get; }

        /// <summary>
        /// Token base balance (in Wei for ex.).
        /// </summary>
        [JsonIgnore]
        public BigInteger TokenBaseBalance { get; }

        /// <summary>
        /// Token balance taking into account decimals.
        /// </summary>
        public decimal TokenBalance
        {
            get
            {
                if (int.TryParse(Token.Decimals, out int decimals) && TokenBaseBalance > 0)
                {
                    var balance = TokenBaseBalance;
                    for (int i = 0; i < decimals; i++)
                    {
                        balance /= 10;
                    }

                    return (decimal)balance;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// DEX swap pairs data.
        /// </summary>
        public IList<DexSwapPairsData> TokenSwapPairs { get; }

        /// <summary>
        /// Get token balance data.
        /// </summary>
        /// <param name="tokenId">The token id.</param>
        /// <param name="tokenBalance">The token balance.</param>
        /// <param name="swapPairs">DEX swap pair data response.</param>
        /// <param name="tokensData">Tokens data.</param>
        public static DexTokenSwapPairsData ForSwapPairs(
            string tokenId,
            BigInteger? tokenBalance,
            SwapPairDataResponse swapPairs,
            IEnumerable<TokenData> tokensData)
        {
            var tokenSwapPairsWithLiquidity = swapPairs
                .DexSwapPairs
                .Select(p =>
                {
                    var tokens = p.SwapPairs.Where(x =>
                    {
                        if (x.Token0?.Id?.Equals(tokenId, StringComparison.InvariantCultureIgnoreCase) == true)
                        {
                            decimal.TryParse(x.Reserve0, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out decimal reserve0);
                            if (new BigInteger(reserve0) >= ActualBalance(tokenBalance, x.Token0.Decimals))
                            {
                                return true;
                            }
                        }

                        if (x.Token1?.Id?.Equals(tokenId, StringComparison.InvariantCultureIgnoreCase) == true)
                        {
                            decimal.TryParse(x.Reserve1, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out decimal reserve1);
                            if (new BigInteger(reserve1) >= ActualBalance(tokenBalance, x.Token1.Decimals))
                            {
                                return true;
                            }
                        }

                        return false;
                    }).ToList();

                    return new DexSwapPairsData(p.DexDescriptor, tokens);
                })
                .Where(p => p.SwapPairs.Any())
                .ToList();

            var tokenDataList = swapPairs
                .DexSwapPairs
                .Select(p =>
                {
                    var tokens = p.SwapPairs.Select(x =>
                    {
                        if (x.Token0?.Id?.Equals(tokenId, StringComparison.InvariantCultureIgnoreCase) == true)
                        {
                            return x.Token0;
                        }

                        if (x.Token1?.Id?.Equals(tokenId, StringComparison.InvariantCultureIgnoreCase) == true)
                        {
                            return x.Token1;
                        }

                        return null;
                    });

                    return tokens.Where(t => t != null);
                }).FirstOrDefault(x => x.Any())?.ToList();

            TokenData? tokenData = null;
            if (tokenDataList != null)
            {
                tokenData = tokenDataList.FirstOrDefault();
            }

            tokenData = tokensData
                .FirstOrDefault(t => t.Id?.Equals(tokenData?.Id, StringComparison.OrdinalIgnoreCase) == true) ?? tokenData;

            BigInteger.TryParse(tokenBalance.ToString(), NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out var decimalTokenBalance);
            return new DexTokenSwapPairsData(tokenData ?? new() { Id = tokenId }, decimalTokenBalance, tokenSwapPairsWithLiquidity.ToList());
        }

        private static BigInteger? ActualBalance(
            BigInteger? balance,
            string? decimals)
        {
            if (balance == null)
            {
                return 0;
            }

            var realAmount = (BigInteger)balance;
            int.TryParse(decimals, out int decimalsInt);
            for (int i = 0; i < decimalsInt; i++)
            {
                realAmount /= 10;
            }

            if (realAmount <= new BigInteger(decimal.MaxValue))
            {
                return realAmount;
            }

            return 0;
        }
    }
}