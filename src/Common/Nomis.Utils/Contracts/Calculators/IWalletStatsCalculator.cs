// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletStatsCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Reflection;

using Nomis.Utils.Contracts.Stats;

// ReSharper disable InconsistentNaming
namespace Nomis.Utils.Contracts.Calculators
{
    /// <summary>
    /// Blockchain wallet stats calculator.
    /// </summary>
    public interface IWalletStatsCalculator
    {
        /// <summary>
        /// Get wallet age.
        /// </summary>
        /// <param name="timeStamps">Collection of timestamps.</param>
        /// <returns>Returns the wallet age (month).</returns>
        public static int GetWalletAge(
            IEnumerable<DateTime> timeStamps)
        {
            var firstTransaction = timeStamps.Min();
            return (int)((DateTime.UtcNow - firstTransaction).TotalDays / 30);
        }

        /// <summary>
        /// Get transaction intervals.
        /// </summary>
        /// <param name="transactionDates">Collection of transaction dates.</param>
        /// <returns>Returns transaction intervals.</returns>
        public static IEnumerable<double> GetTransactionsIntervals(
            IEnumerable<DateTime> transactionDates)
        {
            var result = new List<double>();
            DateTime? lastDateTime = null;
            foreach (var transactionDate in transactionDates.OrderByDescending(x => x))
            {
                if (!lastDateTime.HasValue)
                {
                    lastDateTime = transactionDate;
                    continue;
                }

                double interval = Math.Abs((transactionDate - lastDateTime.Value).TotalHours);
                lastDateTime = transactionDate;
                result.Add(interval);
            }

            return result;
        }

        /// <summary>
        /// Get sum of transactions amount.
        /// </summary>
        /// <param name="transactionHashes">Collection of transaction hash.</param>
        /// <param name="internalTransactionsData">Collection of internal transaction data (transaction hash and amount).</param>
        /// <returns>Returns sum of transactions amount.</returns>
        public static BigInteger TokensSum(
            IEnumerable<string> transactionHashes,
            IEnumerable<(string Hash, BigInteger Amount)> internalTransactionsData)
        {
            var transactions = transactionHashes.Select(x => x.ToLowerInvariant()).ToHashSet();
            var result = new BigInteger();
            foreach (var data in internalTransactionsData.Where(x => x.Hash != null && transactions.Contains(x.Hash.ToLowerInvariant())))
            {
                result += data.Amount;
            }

            return result;
        }
    }

    /// <summary>
    /// Blockchain wallet stats calculator.
    /// </summary>
    /// <typeparam name="TTransactionIntervalData">The transaction interval data type.</typeparam>
    public interface IWalletStatsCalculator<TTransactionIntervalData> :
        IWalletStatsCalculator
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <summary>
        /// Get the intervals of funds movements on the wallet.
        /// </summary>
        /// <param name="tokenUSDPrice">Native token USD price.</param>
        /// <param name="transactionsAmount">Transactions necessary data.</param>
        /// <param name="startDate">Start date for getting data.</param>
        /// <returns>Returns collection of <see cref="ITransactionIntervalData"/>.</returns>
        public static IEnumerable<TTransactionIntervalData> GetTurnoverIntervals(
            decimal tokenUSDPrice,
            IEnumerable<TurnoverIntervalsData> transactionsAmount,
            DateTime startDate)
        {
            var result = new List<TTransactionIntervalData>();
            var now = DateTime.UtcNow;
            while (startDate < now)
            {
                var amountSum = new BigInteger();
                var fromSum = new BigInteger();
                var transactions = transactionsAmount
                    .Where(x => x.TimeStamp >= startDate && x.TimeStamp < startDate.AddMonths(1));

                int transactionsCount = 0;
                foreach (var transactionData in transactions)
                {
                    transactionsCount++;
                    amountSum += transactionData.Amount;
                    if (transactionData.From)
                    {
                        fromSum += transactionData.Amount;
                    }
                }

                if (transactionsCount > 0)
                {
                    result.Add(new()
                    {
                        TokenUSDPrice = tokenUSDPrice,
                        StartDate = startDate,
                        EndDate = startDate.AddMonths(1),
                        AmountSum = amountSum,
                        AmountOutSum = fromSum,
                        AmountInSum = amountSum - fromSum,
                        Count = transactionsCount
                    });
                }

                startDate = startDate.AddMonths(1);
            }

            return result;
        }

        /// <summary>
        /// Get balance change value in the last month (Native token).
        /// </summary>
        public static decimal GetBalanceChangeInLastMonth(
            IEnumerable<TTransactionIntervalData>? turnoverIntervals)
        {
            var lastMonthIntervalData = turnoverIntervals?.LastOrDefault();
            if (lastMonthIntervalData == null)
            {
                return 0;
            }

            return lastMonthIntervalData.AmountInSumValue - lastMonthIntervalData.AmountOutSumValue;
        }

        /// <summary>
        /// Get balance change value in the last year (Native token).
        /// </summary>
        public static decimal GetBalanceChangeInLastYear(
            IEnumerable<TTransactionIntervalData>? turnoverIntervals)
        {
            var turnoverIntervalsList = turnoverIntervals?.ToList();
            if (turnoverIntervalsList?.Any() != true)
            {
                return 0;
            }

            return turnoverIntervalsList.Sum(x => x.AmountInSumValue) - turnoverIntervalsList.Sum(x => x.AmountOutSumValue);
        }
    }

    /// <summary>
    /// Blockchain wallet stats calculator.
    /// </summary>
    /// <typeparam name="TWalletStats">The wallet stats type.</typeparam>
    /// <typeparam name="TTransactionIntervalData">The transaction interval data type.</typeparam>
    public interface IWalletStatsCalculator<TWalletStats, TTransactionIntervalData> :
        IWalletStatsCalculator<TTransactionIntervalData>
        where TWalletStats : class, IWalletStats, new()
        where TTransactionIntervalData : class, ITransactionIntervalData, new()
    {
        /// <summary>
        /// Get blockchain wallet stats.
        /// </summary>
        /// <returns>Returns wallet stats.</returns>
        public TWalletStats Stats()
        {
            return ApplyCalculators();
        }

        /// <summary>
        /// Blockchain wallet stats filler.
        /// </summary>
        public Action<TWalletStats> StatsFiller()
        {
            return stats => Stats().FillStatsTo(stats);
        }

        /// <summary>
        /// Apply all calculators.
        /// </summary>
        /// <returns>Returns the wallet stats.</returns>
        public TWalletStats ApplyCalculators()
        {
            var result = new TWalletStats();
            var calculators = GetType()
                .GetInterfaces()
                .Where(i => i.IsInterface
                            && i.IsGenericType
                            && i.IsAssignableTo(typeof(IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>))
                            && i != typeof(IWalletStatsCalculator<TWalletStats, TTransactionIntervalData>));
            foreach (var calculator in calculators)
            {
                var methodInfo = calculator.GetMethod(nameof(StatsFiller), BindingFlags.NonPublic | BindingFlags.Instance);
                if (methodInfo == null)
                {
                    continue;
                }

                var statsFiller = methodInfo.Invoke(this, Array.Empty<object>()) as Action<TWalletStats>;
                if (statsFiller == null)
                {
                    continue;
                }

                statsFiller.Invoke(result);
                if (result.NoData)
                {
                    return new()
                    {
                        NoData = true
                    };
                }
            }

            return result;
        }
    }
}