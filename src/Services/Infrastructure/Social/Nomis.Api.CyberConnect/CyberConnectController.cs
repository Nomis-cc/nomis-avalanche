// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.CyberConnect.Interfaces;
using Nomis.CyberConnect.Interfaces.Models;
using Nomis.CyberConnect.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.CyberConnect
{
    /// <summary>
    /// A controller to aggregate all CyberConnect-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("CyberConnect protocol.")]
    public sealed class CyberConnectController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/cyberconnect";

        /// <summary>
        /// Common tag for CyberConnect actions.
        /// </summary>
        internal const string CyberConnectTag = "CyberConnect";

        private readonly ILogger<CyberConnectController> _logger;
        private readonly ICyberConnectService _cyberConnectService;

        /// <summary>
        /// Initialize <see cref="CyberConnectController"/>.
        /// </summary>
        /// <param name="cyberConnectService"><see cref="ICyberConnectService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public CyberConnectController(
            ICyberConnectService cyberConnectService,
            ILogger<CyberConnectController> logger)
        {
            _cyberConnectService = cyberConnectService ?? throw new ArgumentNullException(nameof(cyberConnectService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get CyberConnect handle by given address.
        /// </summary>
        /// <param name="request">TODO.</param>
        /// <returns>CyberConnect handle.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/cyberconnect/wallet/handle
        /// </remarks>
        /// <response code="200">Returns CyberConnect handle.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("wallet/handle", Name = "CyberConnectHandle")]
        [SwaggerOperation(
            OperationId = "CyberConnectHandle",
            Tags = new[] { CyberConnectTag })]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CyberConnectHandleAsync([FromBody] CyberConnectHandleRequest request)
        {
            var result = await _cyberConnectService.HandleAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get CyberConnect profile by handle.
        /// </summary>
        /// <param name="request">TODO.</param>
        /// <returns>CyberConnect profile.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/cyber-connect/handle/profile
        /// </remarks>
        /// <response code="200">Returns CyberConnect profile.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("handle/profile", Name = "CyberConnectProfile")]
        [SwaggerOperation(
            OperationId = "CyberConnectProfile",
            Tags = new[] { CyberConnectTag })]
        [ProducesResponseType(typeof(Result<CyberConnectProfileData?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CyberConnectProfileAsync([FromBody] CyberConnectProfileRequest request)
        {
            var result = await _cyberConnectService.ProfileDataAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get CyberConnect subscribings for given handle.
        /// </summary>
        /// <param name="request">TODO.</param>
        /// <returns>CyberConnect subscribings data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/cyber-connect/handle/subscribings
        /// </remarks>
        /// <response code="200">Returns CyberConnect subscribings.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("handle/subscribings", Name = "CyberConnectSubscribings")]
        [SwaggerOperation(
            OperationId = "CyberConnectSubscribings",
            Tags = new[] { CyberConnectTag })]
        [ProducesResponseType(typeof(Result<List<CyberConnectSubscribingProfileData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CyberConnectSubscribingsAsync([FromBody] CyberConnectSubscribingsRequest request)
        {
            var result = await _cyberConnectService.SubscribingsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get CyberConnect likes for given wallet address.
        /// </summary>
        /// <param name="request">TODO.</param>
        /// <returns>CyberConnect likes data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/cyber-connect/wallet/likes
        /// </remarks>
        /// <response code="200">Returns CyberConnect likes.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("wallet/likes", Name = "CyberConnectLikes")]
        [SwaggerOperation(
            OperationId = "CyberConnectLikes",
            Tags = new[] { CyberConnectTag })]
        [ProducesResponseType(typeof(Result<List<CyberConnectLikeData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CyberConnectLikesAsync([FromBody] CyberConnectLikesRequest request)
        {
            var result = await _cyberConnectService.LikesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get CyberConnect essences for given handle.
        /// </summary>
        /// <param name="request">TODO.</param>
        /// <returns>CyberConnect essences data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/cyber-connect/handle/essences
        /// </remarks>
        /// <response code="200">Returns CyberConnect essences.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("handle/essences", Name = "CyberConnectEssences")]
        [SwaggerOperation(
            OperationId = "CyberConnectEssences",
            Tags = new[] { CyberConnectTag })]
        [ProducesResponseType(typeof(Result<List<CyberConnectEssenceData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CyberConnectEssencesAsync([FromBody] CyberConnectEssencesRequest request)
        {
            var result = await _cyberConnectService.EssencesAsync(request);
            return Ok(result);
        }
    }
}