// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiController.cs" company="Nomis">
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
using Nomis.HapiExplorer.Interfaces;
using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Hapi
{
    /// <summary>
    /// A controller to aggregate all HAPI-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("HAPI protocol.")]
    public sealed class HapiController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/hapi";

        /// <summary>
        /// Common tag for HAPI actions.
        /// </summary>
        internal const string HapiTag = "Hapi";

        private readonly ILogger<HapiController> _logger;
        private readonly IHapiExplorerService _hapiExplorerService;

        /// <summary>
        /// Initialize <see cref="HapiController"/>.
        /// </summary>
        /// <param name="hapiExplorerService"><see cref="IHapiExplorerService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public HapiController(
            IHapiExplorerService hapiExplorerService,
            ILogger<HapiController> logger)
        {
            _hapiExplorerService = hapiExplorerService ?? throw new ArgumentNullException(nameof(hapiExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get risk score for given wallet address.
        /// </summary>
        /// <param name="network" example="ethereum">Blockchain network.</param>
        /// <param name="address" example="0x1da5821544e25c636c1417ba96ade4cf6d2f9b5a">Hapi wallet address to get risk score.</param>
        /// <returns>An risk score value and corresponding data.</returns>
        /// <remarks>
        /// Sample requests:
        ///
        ///     GET /api/v1/hapi/ethereum/wallet/0x1da5821544e25c636c1417ba96ade4cf6d2f9b5a/score
        ///
        ///     GET /api/v1/hapi/bsc/wallet/0xc0E82C1ed4786F8B7F806D1B8A6335ec485266fF/score
        ///
        ///     GET /api/v1/hapi/solana/wallet/Esmx2QjmDZMjJ15yBJ2nhqisjEt7Gqro4jSkofdoVsvY/score
        ///
        ///     GET /api/v1/hapi/tron/wallet/TYoZM8LALfUqm4EXzB7oKmwqusWtXTBhY6/score
        ///
        ///     GET /api/v1/hapi/near/wallet/e8520b94b09a16a7fa81ed83ea2d8b77a19b1391f08fcb3b0a0d096fc7ae82a4/score
        /// </remarks>
        /// <response code="200">Returns HAPI risk score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("{network}/wallet/{address}/score", Name = "GetHapiWalletRiskScore")]
        [SwaggerOperation(
            OperationId = "GetHapiWalletRiskScore",
            Tags = new[] { HapiTag })]
        [ProducesResponseType(typeof(Result<HapiProxyRiskScoreResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetHapiWalletScoreAsync(
            [Required(ErrorMessage = "Network should be set")] string network,
            [Required(ErrorMessage = "Wallet address should be set")] string address)
        {
            var result = await _hapiExplorerService.GetWalletRiskScoreAsync(network, address);
            return Ok(result);
        }
    }
}