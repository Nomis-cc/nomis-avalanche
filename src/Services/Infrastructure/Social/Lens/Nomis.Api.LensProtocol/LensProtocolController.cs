// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.LensProtocol.Interfaces;
using Nomis.LensProtocol.Interfaces.Models;
using Nomis.LensProtocol.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.LensProtocol
{
    /// <summary>
    /// A controller to aggregate all Lens Protocol-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("LensProtocol protocol.")]
    public sealed class LensProtocolController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/lensprotocol";

        /// <summary>
        /// Common tag for Lens Protocol actions.
        /// </summary>
        internal const string LensProtocolTag = "LensProtocol";

        private readonly ILogger<LensProtocolController> _logger;
        private readonly ILensProtocolService _lensProtocolService;

        /// <summary>
        /// Initialize <see cref="LensProtocolController"/>.
        /// </summary>
        /// <param name="lensProtocolService"><see cref="ILensProtocolService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public LensProtocolController(
            ILensProtocolService lensProtocolService,
            ILogger<LensProtocolController> logger)
        {
            _lensProtocolService = lensProtocolService ?? throw new ArgumentNullException(nameof(lensProtocolService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get the Lens Protocol profile data.
        /// </summary>
        /// <param name="request">Lens Protocol profile request.</param>
        /// <returns>Returns Lens Protocol profile data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/lensprotocol/profile
        /// </remarks>
        /// <response code="200">Returns Lens Protocol profile data.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("profile", Name = "LensProtocolProfile")]
        [SwaggerOperation(
            OperationId = "LensProtocolProfile",
            Tags = new[] { LensProtocolTag })]
        [ProducesResponseType(typeof(Result<LensProtocolProfileData?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> LensProtocolProfileAsync(
            [FromBody] LensProtocolProfileRequest request)
        {
            var result = await _lensProtocolService.ProfileDataAsync(request);
            return Ok(result);
        }
    }
}