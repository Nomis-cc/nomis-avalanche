// ------------------------------------------------------------------------------------------------------
// <copyright file="ChainanalysisController.cs" company="Nomis">
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
using Nomis.Chainanalysis.Interfaces;
using Nomis.Chainanalysis.Interfaces.Responses;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Chainanalysis
{
    /// <summary>
    /// A controller to aggregate all Chainanalysis-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Chainanalysis sanctions reporting service.")]
    public sealed class ChainanalysisController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/chainanalysis";

        /// <summary>
        /// Common tag for Chainanalysis actions.
        /// </summary>
        internal const string ChainanalysisTag = "Chainanalysis";

        private readonly ILogger<ChainanalysisController> _logger;
        private readonly IChainanalysisService _chainanalysisService;

        /// <summary>
        /// Initialize <see cref="ChainanalysisController"/>.
        /// </summary>
        /// <param name="chainanalysisService"><see cref="IChainanalysisService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ChainanalysisController(
            IChainanalysisService chainanalysisService,
            ILogger<ChainanalysisController> logger)
        {
            _chainanalysisService = chainanalysisService ?? throw new ArgumentNullException(nameof(chainanalysisService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get sanctions reports for given wallet address.
        /// </summary>
        /// <param name="address" example="0x1da5821544e25c636c1417ba96ade4cf6d2f9b5a">Blockchain wallet address to get sanctions reports.</param>
        /// <returns>A sanctions reports and corresponding data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/chainanalysis/wallet/0x1da5821544e25c636c1417ba96ade4cf6d2f9b5a/reports
        /// </remarks>
        /// <response code="200">Returns Chainanalysis sanctions reports.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/reports", Name = "GetChainanalysisWalletReports")]
        [SwaggerOperation(
            OperationId = "GetChainanalysisWalletReports",
            Tags = new[] { ChainanalysisTag })]
        [ProducesResponseType(typeof(Result<ChainanalysisReportsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetChainanalysisWalletReportsAsync(
            [Required(ErrorMessage = "Wallet address should be set")] string address)
        {
            var result = await _chainanalysisService.GetWalletReportsAsync(address);
            return Ok(result);
        }
    }
}