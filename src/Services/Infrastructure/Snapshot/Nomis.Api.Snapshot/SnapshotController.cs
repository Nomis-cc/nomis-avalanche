// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Snapshot.Interfaces;
using Nomis.Snapshot.Interfaces.Models;
using Nomis.Snapshot.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Snapshot
{
    /// <summary>
    /// A controller to aggregate all Snapshot-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Snapshot protocol.")]
    public sealed class SnapshotController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/snapshot";

        /// <summary>
        /// Common tag for Snapshot actions.
        /// </summary>
        internal const string SnapshotTag = "Snapshot";

        private readonly ILogger<SnapshotController> _logger;
        private readonly ISnapshotService _snapshotService;

        /// <summary>
        /// Initialize <see cref="SnapshotController"/>.
        /// </summary>
        /// <param name="snapshotService"><see cref="ISnapshotService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public SnapshotController(
            ISnapshotService snapshotService,
            ILogger<SnapshotController> logger)
        {
            _snapshotService = snapshotService ?? throw new ArgumentNullException(nameof(snapshotService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Snapshot votes for given wallet address.
        /// </summary>
        /// <param name="request">Snapshot wallet address to get Snapshot votes.</param>
        /// <returns>Snapshot votes data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/snapshot/wallet/votes
        /// </remarks>
        /// <response code="200">Returns Snapshot votes.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("wallet/votes", Name = "GetSnapshotWalletVotes")]
        [SwaggerOperation(
            OperationId = "GetSnapshotWalletVotes",
            Tags = new[] { SnapshotTag })]
        [ProducesResponseType(typeof(Result<List<SnapshotVote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetSnapshotWalletVotesAsync([FromBody] GetSnapshotVotesRequest request)
        {
            var result = await _snapshotService.GetSnapshotVotesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get Snapshot proposals for given wallet address.
        /// </summary>
        /// <param name="request">Snapshot wallet address to get Snapshot proposals.</param>
        /// <returns>Snapshot proposals data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/snapshot/wallet/proposals
        /// </remarks>
        /// <response code="200">Returns Snapshot proposals.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("wallet/proposals", Name = "GetSnapshotWalletProposals")]
        [SwaggerOperation(
            OperationId = "GetSnapshotWalletProposals",
            Tags = new[] { SnapshotTag })]
        [ProducesResponseType(typeof(Result<List<SnapshotProposal>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetSnapshotWalletProposalsAsync([FromBody] GetSnapshotProposalsRequest request)
        {
            var result = await _snapshotService.GetSnapshotProposalsAsync(request);
            return Ok(result);
        }
    }
}