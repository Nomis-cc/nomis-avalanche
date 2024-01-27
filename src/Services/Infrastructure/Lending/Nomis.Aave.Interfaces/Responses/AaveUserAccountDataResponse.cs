// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveUserAccountDataResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Aave.Interfaces.Responses
{
    /// <summary>
    /// Aave user account data response.
    /// </summary>
    public class AaveUserAccountDataResponse
    {
        /// <summary>
        /// Total collateral base.
        /// </summary>
        public string? TotalCollateralBase { get; set; }

        /// <summary>
        /// Total debt base.
        /// </summary>
        public string? TotalDebtBase { get; set; }

        /// <summary>
        /// Available borrows base.
        /// </summary>
        public string? AvailableBorrowsBase { get; set; }

        /// <summary>
        /// Current liquidation threshold.
        /// </summary>
        public string? CurrentLiquidationThreshold { get; set; }

        /// <summary>
        /// LTV.
        /// </summary>
        public string? Ltv { get; set; }

        /// <summary>
        /// Health factor.
        /// </summary>
        public string? HealthFactor { get; set; }
    }
}