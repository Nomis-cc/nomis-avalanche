// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveUserAccountData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nomis.Aave.Contracts
{
    /// <summary>
    /// Aave user account data.
    /// </summary>
    [FunctionOutput]
    public class AaveUserAccountData :
        IFunctionOutputDTO
    {
        /// <summary>
        /// Total collateral base.
        /// </summary>
        [Parameter("uint256", "totalCollateralBase", 1)]
        public virtual BigInteger TotalCollateralBase { get; set; }

        /// <summary>
        /// Total debt base.
        /// </summary>
        [Parameter("uint256", "totalDebtBase", 2)]
        public virtual BigInteger TotalDebtBase { get; set; }

        /// <summary>
        /// Available borrows base.
        /// </summary>
        [Parameter("uint256", "availableBorrowsBase", 3)]
        public virtual BigInteger AvailableBorrowsBase { get; set; }

        /// <summary>
        /// Current liquidation threshold.
        /// </summary>
        [Parameter("uint256", "currentLiquidationThreshold", 4)]
        public virtual BigInteger CurrentLiquidationThreshold { get; set; }

        /// <summary>
        /// LTV.
        /// </summary>
        [Parameter("uint256", "ltv", 5)]
        public virtual BigInteger Ltv { get; set; }

        /// <summary>
        /// Health factor.
        /// </summary>
        [Parameter("uint256", "healthFactor", 6)]
        public virtual BigInteger HealthFactor { get; set; }
    }
}