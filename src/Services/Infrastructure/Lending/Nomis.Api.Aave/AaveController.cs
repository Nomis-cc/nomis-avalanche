// ------------------------------------------------------------------------------------------------------
// <copyright file="AaveController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Aave.Interfaces;
using Nomis.Aave.Interfaces.Enums;
using Nomis.Aave.Interfaces.Responses;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Aave
{
    /// <summary>
    /// A controller to aggregate all Aave-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Aave protocol.")]
    public sealed class AaveController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/aave";

        /// <summary>
        /// Common tag for Aave actions.
        /// </summary>
        internal const string AaveTag = "Aave";

        private readonly ILogger<AaveController> _logger;
        private readonly IAaveService _aaveService;

        /// <summary>
        /// Initialize <see cref="AaveController"/>.
        /// </summary>
        /// <param name="aaveService"><see cref="IAaveService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public AaveController(
            IAaveService aaveService,
            ILogger<AaveController> logger)
        {
            _aaveService = aaveService ?? throw new ArgumentNullException(nameof(aaveService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Aave user account data by wallet address.
        /// </summary>
        /// <param name="blockchain" example="ethereum">Blockchain.</param>
        /// <param name="address" example="0xa0c7BD318D69424603CBf91e9969870F21B8ab4c">Aave wallet address to get user data.</param>
        /// <returns>An Aave user data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/aave/ethereum/wallet/0xa0c7BD318D69424603CBf91e9969870F21B8ab4c/data
        /// </remarks>
        /// <response code="200">Returns Aave user data.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("{blockchain}/wallet/{address}/data", Name = "GetAaveUserData")]
        [SwaggerOperation(
            OperationId = "GetAaveUserData",
            Tags = new[] { AaveTag })]
        [ProducesResponseType(typeof(Result<AaveUserAccountDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAaveUserDataAsync(
            [Required(ErrorMessage = "Blockchain should be set")] AaveChain blockchain,
            [Required(ErrorMessage = "Wallet address should be set")] string address)
        {
            var result = await _aaveService.GetAaveUserAccountDataAsync(blockchain, address);
            return Ok(result);
        }
    }
}