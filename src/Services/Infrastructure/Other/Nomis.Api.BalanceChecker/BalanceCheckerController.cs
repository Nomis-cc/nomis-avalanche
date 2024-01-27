// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceCheckerController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.BalanceChecker.Interfaces;
using Nomis.BalanceChecker.Interfaces.Contracts;
using Nomis.BalanceChecker.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.BalanceChecker
{
    /// <summary>
    /// A controller to aggregate all Balance Checker-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Balance Checker service.")]
    public sealed class BalanceCheckerController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/balance-checker";

        /// <summary>
        /// Common tag for BalanceChecker actions.
        /// </summary>
        internal const string BalanceCheckerTag = "BalanceChecker";

        private readonly ILogger<BalanceCheckerController> _logger;
        private readonly IBalanceCheckerService _balanceCheckerService;

        /// <summary>
        /// Initialize <see cref="BalanceCheckerController"/>.
        /// </summary>
        /// <param name="balanceCheckerService"><see cref="IBalanceCheckerService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public BalanceCheckerController(
            IBalanceCheckerService balanceCheckerService,
            ILogger<BalanceCheckerController> logger)
        {
            _balanceCheckerService = balanceCheckerService ?? throw new ArgumentNullException(nameof(balanceCheckerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get token balances by given wallet address and blockchain.
        /// </summary>
        /// <param name="request">Token balances request.</param>
        /// <returns>Returns tokens balances data.</returns>
        /// <response code="200">Returns tokens balances data.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("balances", Name = "OwnerBalances")]
        [SwaggerOperation(
            OperationId = "OwnerBalances",
            Tags = new[] { BalanceCheckerTag })]
        [ProducesResponseType(typeof(Result<IEnumerable<BalanceCheckerTokenInfo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> BalancesAsync(
            [FromBody] TokenBalancesRequest request)
        {
            var result = await _balanceCheckerService.TokenBalancesAsync(request);
            return Ok(result);
        }
    }
}