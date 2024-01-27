// ------------------------------------------------------------------------------------------------------
// <copyright file="ITransactionIntervalData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

// ReSharper disable InconsistentNaming
namespace Nomis.Utils.Contracts
{
    /// <summary>
    /// Transaction interval data.
    /// </summary>
    public interface ITransactionIntervalData
    {
        /// <summary>
        /// Native token USD price.
        /// </summary>
        public decimal TokenUSDPrice { get; set; }

        /// <summary>
        /// Start date of the interval.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the interval.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Tokens amount sum in the interval.
        /// </summary>
        public BigInteger AmountSum { get; set; }

        /// <summary>
        /// Tokens amount sum in the interval (native token).
        /// </summary>
        public decimal AmountSumValue { get; set; }

        /// <summary>
        /// Tokens amount sum in the interval (USD).
        /// </summary>
        public decimal AmountSumUSDValue { get; }

        /// <summary>
        /// Tokens amount sum sent from the wallet in the interval.
        /// </summary>
        public BigInteger AmountOutSum { get; set; }

        /// <summary>
        /// Tokens amount sum sent from the wallet in the interval (native token).
        /// </summary>
        public decimal AmountOutSumValue { get; set; }

        /// <summary>
        /// Tokens amount sum sent from the wallet in the interval (USD).
        /// </summary>
        public decimal AmountOutSumUSDValue { get; }

        /// <summary>
        /// Tokens amount sum received to the wallet in the interval.
        /// </summary>
        public BigInteger AmountInSum { get; set; }

        /// <summary>
        /// Tokens amount sum received to the wallet in the interval (native token).
        /// </summary>
        public decimal AmountInSumValue { get; set; }

        /// <summary>
        /// Tokens amount sum received to the wallet in the interval (USD).
        /// </summary>
        public decimal AmountInSumUSDValue { get; }

        /// <summary>
        /// Transactions count in the interval.
        /// </summary>
        public int Count { get; set; }
    }
}