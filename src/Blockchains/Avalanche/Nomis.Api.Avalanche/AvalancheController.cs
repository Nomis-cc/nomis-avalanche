// ------------------------------------------------------------------------------------------------------
// <copyright file="AvalancheController.cs" company="Nomis">
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
using Nomis.Snowtrace.Interfaces;
using Nomis.Snowtrace.Interfaces.Models;
using Nomis.Snowtrace.Interfaces.Requests;
using Nomis.Utils.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Avalanche
{
    /// <summary>
    /// A controller to aggregate all Avalanche-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Avalanche C-Chain blockchain.")]
    public sealed class AvalancheController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/avalanche";

        /// <summary>
        /// Common tag for Avalanche actions.
        /// </summary>
        internal const string AvalancheTag = "Avalanche";

        private readonly ILogger<AvalancheController> _logger;
        private readonly IAvalancheScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="AvalancheController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IAvalancheScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public AvalancheController(
            IAvalancheScoringService scoringService,
            ILogger<AvalancheController> logger)
        {
            _scoringService = scoringService ?? throw new ArgumentNullException(nameof(scoringService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Nomis Score for given wallet address.
        /// </summary>
        /// <param name="request">Request for getting the wallet stats.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>An Nomis Score value and corresponding statistical data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/avalanche/wallet/0x5E12fc70B97902AC19B9cB87F2aC5a8593769779/score?scoreType=0&amp;nonce=0&amp;deadline=1790647549
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetAvalancheWalletScore")]
        [SwaggerOperation(
            OperationId = "GetAvalancheWalletScore",
            Tags = new[] { AvalancheTag })]
        [ProducesResponseType(typeof(Result<AvalancheWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAvalancheWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] AvalancheWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<AvalancheWalletStatsRequest, AvalancheWalletScore, AvalancheWalletStats, AvalancheTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Get Nomis Score for given wallets addresses by file.
        /// </summary>
        /// <param name="request">Requests for getting the wallets stats parameters.</param>
        /// <param name="file">File with wallets addresses separated line by line.</param>
        /// <param name="concurrentRequestCount">Concurrent request count.</param>
        /// <param name="delayInMilliseconds">Delay in milliseconds between calls.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>An Nomis Score values and corresponding statistical data.</returns>
        /// <response code="200">Returns Nomis Scores and stats.</response>
        /// <response code="400">Addresses not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("wallets/score-by-file", Name = "GetAvalancheWalletsScoreByFile")]
        [SwaggerOperation(
            OperationId = "GetAvalancheWalletsScoreByFile",
            Tags = new[] { AvalancheTag })]
        [ProducesResponseType(typeof(Result<List<AvalancheWalletScore>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAvalancheWalletsScoreByFileAsync(
            [Required(ErrorMessage = "Request should be set")] AvalancheWalletStatsRequest request,
            IFormFile file,
            int concurrentRequestCount = 1,
            int delayInMilliseconds = 500,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    var wallets = new List<string?>();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            wallets.Add(await reader.ReadLineAsync(cancellationToken));
                        }
                    }

                    return Ok(await _scoringService.GetWalletsStatsAsync<AvalancheWalletStatsRequest, AvalancheWalletScore, AvalancheWalletStats, AvalancheTransactionIntervalData>(
                        wallets.Where(x => x != null).Cast<string>().Select(wallet => new AvalancheWalletStatsRequest
                        {
                            Address = wallet,
                            UseTokenLists = request.UseTokenLists,
                            CalculationModel = request.CalculationModel,
                            TokenAddress = request.TokenAddress,
                            Deadline = request.Deadline,
                            FirstSwapPairs = request.FirstSwapPairs,
                            GetChainanalysisData = request.GetChainanalysisData,
                            GetCyberConnectProtocolData = request.GetCyberConnectProtocolData,
                            GetGreysafeData = request.GetGreysafeData,
                            GetHoldTokensBalances = request.GetHoldTokensBalances,
                            GetSnapshotProtocolData = request.GetSnapshotProtocolData,
                            GetTokensSwapPairs = request.GetTokensSwapPairs,
                            IncludeUniversalTokenLists = request.IncludeUniversalTokenLists,
                            MintBlockchainType = request.MintBlockchainType,
                            Nonce = request.Nonce,
                            ScoreType = request.ScoreType,
                            SearchWidthInHours = request.SearchWidthInHours,
                            Skip = request.Skip,
                            GetAaveProtocolData = request.GetAaveProtocolData,
                            FromCache = request.FromCache
                        }).ToList(),
                        concurrentRequestCount,
                        delayInMilliseconds,
                        cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}