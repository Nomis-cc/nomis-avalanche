// ------------------------------------------------------------------------------------------------------
// <copyright file="PolygonIdController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.PolygonId.Interfaces;
using Nomis.PolygonId.Interfaces.PolygonIdIssuerNode;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.PolygonId
{
    /// <summary>
    /// A controller to aggregate all PolygonId-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("PolygonId API.")]
    public sealed class PolygonIdController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/polygonId";

        /// <summary>
        /// Common tag for PolygonId actions.
        /// </summary>
        internal const string PolygonIdTag = "PolygonId";

        private readonly ILogger<PolygonIdController> _logger;
        private readonly IPolygonIdService _polygonIdService;

        /// <summary>
        /// Initialize <see cref="PolygonIdController"/>.
        /// </summary>
        /// <param name="polygonIdExplorerService"><see cref="IPolygonIdService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public PolygonIdController(
            IPolygonIdService polygonIdExplorerService,
            ILogger<PolygonIdController> logger)
        {
            _polygonIdService = polygonIdExplorerService ?? throw new ArgumentNullException(nameof(polygonIdExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all PolygonID identities.
        /// </summary>
        /// <returns>Returns the PolygonID identities.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/polygonId/issuer/identities
        /// </remarks>
        /// <response code="200">Returns the PolygonID identities.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("issuer/identities", Name = "PolygonIdIdentities")]
        [SwaggerOperation(
            OperationId = "PolygonIdIdentities",
            Tags = new[] { PolygonIdTag })]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PolygonIdIdentitiesAsync()
        {
            var result = await _polygonIdService.Client.GetIdentitiesAsync();
            return Ok(result);
        }

        /*/// <summary>
        /// Get PolygonID claim by id for issuer DID.
        /// </summary>
        /// <returns>Returns the PolygonID claim by id for issuer DID.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/polygonId/issuer/did:polygonid:polygon:mumbai:2qJESmaVKpqRCG6P2V6wgYXXN49pvsP2Ti4N2opZ5F/claims/5fca1232-ee7b-11ed-bc4c-0242ac140006
        /// </remarks>
        /// <param name="did">Issuer DID.</param>
        /// <param name="cid">Claim identifier.</param>
        /// <response code="200">Returns the PolygonID claim by DID for issuer DID.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("issuer/{did}/claims/{cid}", Name = "PolygonIdClaim")]
        [SwaggerOperation(
            OperationId = "PolygonIdClaim",
            Tags = new[] { PolygonIdTag })]
        [ProducesResponseType(typeof(GetClaimResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PolygonIdClaimAsync(
            string did,
            string cid)
        {
            var result = await _polygonIdService.Client.GetClaimAsync(did, cid);
            return Ok(result);
        }*/

        /// <summary>
        /// Get PolygonID claim QR-code by id for issuer DID.
        /// </summary>
        /// <returns>Returns the PolygonID claim  QR-code by id for issuer DID.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/polygonId/issuer/did:polygonid:polygon:mumbai:2qJESmaVKpqRCG6P2V6wgYXXN49pvsP2Ti4N2opZ5F/claims/5fca1232-ee7b-11ed-bc4c-0242ac140006/qrcode
        /// </remarks>
        /// <param name="did">Issuer DID.</param>
        /// <param name="cid">Claim identifier.</param>
        /// <response code="200">Returns the PolygonID claim QR-code by DID for issuer DID.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("issuer/{did}/claims/{cid}/qrcode", Name = "PolygonIdClaimQr")]
        [SwaggerOperation(
            OperationId = "PolygonIdClaimQr",
            Tags = new[] { PolygonIdTag })]
        [ProducesResponseType(typeof(GetClaimQrCodeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PolygonIdClaimQrAsync(
            string did,
            string cid)
        {
            var result = await _polygonIdService.Client.GetClaimQrCodeAsync(did, cid);
            return Ok(result);
        }
    }
}