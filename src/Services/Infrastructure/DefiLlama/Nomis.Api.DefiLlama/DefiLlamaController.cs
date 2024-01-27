// ------------------------------------------------------------------------------------------------------
// <copyright file="DefiLlamaController.cs" company="Nomis">
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
using Nomis.DefiLlama.Interfaces;
using Nomis.DefiLlama.Interfaces.Responses;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.DefiLlama
{
    /// <summary>
    /// A controller to aggregate all DefiLlama-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("DefiLlama API.")]
    public sealed class DefiLlamaController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/defillama";

        /// <summary>
        /// Common tag for DefiLlama actions.
        /// </summary>
        internal const string DefiLlamaTag = "DefiLlama";

        private readonly ILogger<DefiLlamaController> _logger;
        private readonly IDefiLlamaService _defillamaService;

        /// <summary>
        /// Initialize <see cref="DefiLlamaController"/>.
        /// </summary>
        /// <param name="defillamaExplorerService"><see cref="IDefiLlamaService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DefiLlamaController(
            IDefiLlamaService defillamaExplorerService,
            ILogger<DefiLlamaController> logger)
        {
            _defillamaService = defillamaExplorerService ?? throw new ArgumentNullException(nameof(defillamaExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get token balances by Llamafolio.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns token balances by Llamafolio..</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/defillama/llamafolio/balances?address=0x4277f8f2c384827b5273592ff7cebd9f2c1ac258
        /// </remarks>
        /// <response code="200">Returns token balances by Llamafolio.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("llamafolio/balances", Name = "GetLlamafolioTokenBalances")]
        [SwaggerOperation(
            OperationId = "GetLlamafolioTokenBalances",
            Tags = new[] { DefiLlamaTag })]
        [ProducesResponseType(typeof(Result<LlamafolioTokenBalancesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetDefiLlamaTokensPriceAsync(
            [Required(ErrorMessage = "The wallet address should be set")] string address,
            CancellationToken cancellationToken = default)
        {
            var result = await _defillamaService.TokenBalancesAsync(
                address,
                cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get tokens price.
        /// </summary>
        /// <param name="tokenIds">The list of tokens id.</param>
        /// <param name="searchWidthInHours">Time range in hours on either side to find price data.</param>
        /// <returns>Returns tokens price.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/defillama/prices?tokenIds=arbitrum:0x4277f8f2c384827b5273592ff7cebd9f2c1ac258&amp;tokenIds=bsc:0x0e09fabb73bd3ade0a17ecc321fd13a19e81ce82
        /// </remarks>
        /// <response code="200">Returns tokens price.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("prices", Name = "GetDefiLlamaTokensPrice")]
        [SwaggerOperation(
            OperationId = "GetDefiLlamaTokensPrice",
            Tags = new[] { DefiLlamaTag })]
        [ProducesResponseType(typeof(Result<DefiLlamaTokensPriceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetDefiLlamaTokensPriceAsync(
            [Required(ErrorMessage = "The list of tokens id should be set")] IList<string> tokenIds,
            int searchWidthInHours = 6)
        {
            var result = await _defillamaService.TokensPriceAsync(
                tokenIds.Where(t => !string.IsNullOrWhiteSpace(t)).ToList(),
                searchWidthInHours);
            return Ok(result);
        }
    }
}