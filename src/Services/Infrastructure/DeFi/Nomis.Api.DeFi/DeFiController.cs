// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.DeFi.Interfaces;
using Nomis.DeFi.Interfaces.Models;
using Nomis.DeFi.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.DeFi
{
    /// <summary>
    /// A controller to aggregate all De.Fi-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("DeFi.")]
    public sealed class DeFiController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/de-fi";

        /// <summary>
        /// Common tag for DeFi actions.
        /// </summary>
        internal const string DeFiTag = "DeFi";

        private readonly ILogger<DeFiController> _logger;
        private readonly IDeFiService _deFiService;

        /// <summary>
        /// Initialize <see cref="DeFiController"/>.
        /// </summary>
        /// <param name="deFiService"><see cref="IDeFiService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DeFiController(
            IDeFiService deFiService,
            ILogger<DeFiController> logger)
        {
            _deFiService = deFiService ?? throw new ArgumentNullException(nameof(deFiService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get De.Fi chains.
        /// </summary>
        /// <returns>De.Fi chains.</returns>
        /// <response code="200">Returns DeFi chains.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("chains", Name = "DeFiChains")]
        [SwaggerOperation(
            OperationId = "DeFiChains",
            Tags = new[] { DeFiTag })]
        [ProducesResponseType(typeof(Result<List<DeFiChainData>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeFiChainsAsync()
        {
            var result = await _deFiService.ChainsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get De.Fi shields.
        /// </summary>
        /// <param name="request">De.Fi shields request.</param>
        /// <returns>De.Fi shields.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/de-fi/shields
        /// </remarks>
        /// <response code="200">Returns De.Fi shields.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("shields", Name = "DeFiShields")]
        [SwaggerOperation(
            OperationId = "DeFiShields",
            Tags = new[] { DeFiTag })]
        [ProducesResponseType(typeof(Result<DeFiShieldAdvancedData?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeFiSubscribingsAsync([FromBody] DeFiShieldsRequest request)
        {
            var result = await _deFiService.ShieldsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get De.Fi wallet asset balances.
        /// </summary>
        /// <param name="request">De.Fi wallet asset balances request.</param>
        /// <returns>De.Fi wallet asset balances.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/de-fi/asset-balances
        /// </remarks>
        /// <response code="200">Returns De.Fi wallet asset balances.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("asset-balances", Name = "DeFiAssetBalances")]
        [SwaggerOperation(
            OperationId = "DeFiAssetBalances",
            Tags = new[] { DeFiTag })]
        [ProducesResponseType(typeof(Result<DeFiAssetBalances?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AssetBalancesAsync([FromBody] DeFiAssetBalancesRequest request)
        {
            var result = await _deFiService.AssetBalancesAsync(request);
            return Ok(result);
        }
    }
}