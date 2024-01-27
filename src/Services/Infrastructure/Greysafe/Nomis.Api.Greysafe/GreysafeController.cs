// ------------------------------------------------------------------------------------------------------
// <copyright file="GreysafeController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Greysafe.Interfaces;
using Nomis.Greysafe.Interfaces.Responses;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Greysafe
{
    /// <summary>
    /// A controller to aggregate all Greysafe-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Greysafe scam reporting service.")]
    public sealed class GreysafeController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/greysafe";

        /// <summary>
        /// Common tag for Greysafe actions.
        /// </summary>
        internal const string GreysafeTag = "Greysafe";

        private readonly ILogger<GreysafeController> _logger;
        private readonly IGreysafeService _greysafeService;

        /// <summary>
        /// Initialize <see cref="GreysafeController"/>.
        /// </summary>
        /// <param name="greysafeService"><see cref="IGreysafeService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public GreysafeController(
            IGreysafeService greysafeService,
            ILogger<GreysafeController> logger)
        {
            _greysafeService = greysafeService ?? throw new ArgumentNullException(nameof(greysafeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get scam reports for given wallet address.
        /// </summary>
        /// <param name="address" example="0x1da5821544e25c636c1417ba96ade4cf6d2f9b5a">Blockchain wallet address to get scam reports.</param>
        /// <returns>A scam reports and corresponding data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/greysafe/wallet/0x1da5821544e25c636c1417ba96ade4cf6d2f9b5a/reports
        /// </remarks>
        /// <response code="200">Returns Greysafe scam reports.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/reports", Name = "GetGreysafeWalletReports")]
        [SwaggerOperation(
            OperationId = "GetGreysafeWalletReports",
            Tags = new[] { GreysafeTag })]
        [ProducesResponseType(typeof(Result<GreysafeReportsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetGreysafeWalletReportsAsync(
            [Required(ErrorMessage = "Wallet address should be set")] string address)
        {
            var result = await _greysafeService.GetWalletReportsAsync(address);
            return Ok(result);
        }
    }
}