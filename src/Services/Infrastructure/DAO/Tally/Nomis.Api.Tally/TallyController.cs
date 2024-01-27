// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Tally.Interfaces;
using Nomis.Tally.Interfaces.Models;
using Nomis.Tally.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Tally
{
    /// <summary>
    /// A controller to aggregate all Tally-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Tally protocol.")]
    public sealed class TallyController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/tally";

        /// <summary>
        /// Common tag for Tally actions.
        /// </summary>
        internal const string TallyTag = "Tally";

        private readonly ILogger<TallyController> _logger;
        private readonly ITallyService _tallyService;

        /// <summary>
        /// Initialize <see cref="TallyController"/>.
        /// </summary>
        /// <param name="tallyService"><see cref="ITallyService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public TallyController(
            ITallyService tallyService,
            ILogger<TallyController> logger)
        {
            _tallyService = tallyService ?? throw new ArgumentNullException(nameof(tallyService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Tally account data for given wallet address.
        /// </summary>
        /// <param name="request">Tally wallet address to get Tally account data.</param>
        /// <returns>Tally account data.</returns>
        /// <response code="200">Returns Tally account data.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("wallet/account", Name = "GetTallyWalletAccount")]
        [SwaggerOperation(
            OperationId = "GetTallyWalletAccount",
            Tags = new[] { TallyTag })]
        [ProducesResponseType(typeof(Result<List<TallyVote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTallyWalletAccountAsync([FromBody] TallyAccountRequest request)
        {
            var result = await _tallyService.GetTallyAccountDataAsync(request);
            return Ok(result);
        }
    }
}