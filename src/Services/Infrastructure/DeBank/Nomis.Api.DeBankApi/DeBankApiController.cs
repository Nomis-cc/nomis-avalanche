// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankApiController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Auth.Attributes;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.DeBank.Interfaces;
using Nomis.DeBank.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.DeBankApi
{
    /// <summary>
    /// A controller to aggregate all DeBank-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [ApiKey(AppliedRoles = new[] { "DeBank", "NOMIS" })]
    [SwaggerTag("DeBank API.")]
    public sealed class DeBankApiController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/debankapi";

        /// <summary>
        /// Common tag for DeBank actions.
        /// </summary>
        internal const string DeBankTag = "DeBankApi";

        private readonly ILogger<DeBankApiController> _logger;
        private readonly IDeBankService _deBankService;

        /// <summary>
        /// Initialize <see cref="DeBankApiController"/>.
        /// </summary>
        /// <param name="deBankExplorerService"><see cref="IDeBankService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DeBankApiController(
            IDeBankService deBankExplorerService,
            ILogger<DeBankApiController> logger)
        {
            _deBankService = deBankExplorerService ?? throw new ArgumentNullException(nameof(deBankExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get hold tokens data by wallet address.
        /// </summary>
        /// <param name="owner">Blockchain id.</param>
        /// <param name="debankChainId">DeBank blockchain internal id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns hold tokens data by wallet address.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/debankapi/hold-token-balances?owner=0xfbc44654dd3d5a7a5ea1267928b6e61203856395&amp;debankChainId=ethereum
        /// </remarks>
        /// <response code="200">Returns hold tokens data by wallet address.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("hold-token-balances", Name = "GetDeBankHoldTokenBalances")]
        [SwaggerOperation(
            OperationId = "GetDeBankHoldTokenBalances",
            Tags = new[] { DeBankTag })]
        [ProducesResponseType(typeof(Result<List<DeBankTokenData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> HoldTokensDataAsync(
            [Required(ErrorMessage = "The owner wallet address should be set")] string owner,
            [Required(ErrorMessage = "The blockchain should be set")] string debankChainId,
            CancellationToken cancellationToken)
        {
            var result = await _deBankService.HoldTokensDataAsync(
                owner,
                debankChainId,
                cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get tokens data by ids.
        /// </summary>
        /// <param name="ids">Token ids.</param>
        /// <param name="debankChainId">DeBank blockchain internal id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns tokens data by ids.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/debankapi/tokens-data?ids=0x83c30eb8bc9ad7C56532895840039E62659896ea&amp;debankChainId=kava
        /// </remarks>
        /// <response code="200">Returns tokens data by ids.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("tokens-data", Name = "GetDeBankTokensData")]
        [SwaggerOperation(
            OperationId = "GetDeBankTokensData",
            Tags = new[] { DeBankTag })]
        [ProducesResponseType(typeof(List<DeBankTokenData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> TokensDataAsync(
            [Required(ErrorMessage = "The owner wallet address should be set")] IList<string> ids,
            [Required(ErrorMessage = "The blockchain should be set")] string debankChainId,
            CancellationToken cancellationToken)
        {
            var result = await _deBankService.TokensDataAsync(
                ids,
                debankChainId,
                cancellationToken);
            return Ok(result);
        }
    }
}