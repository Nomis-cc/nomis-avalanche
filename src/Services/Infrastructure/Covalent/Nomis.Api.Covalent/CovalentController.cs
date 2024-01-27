// ------------------------------------------------------------------------------------------------------
// <copyright file="CovalentController.cs" company="Nomis">
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
using Nomis.Covalent.Interfaces;
using Nomis.Covalent.Interfaces.Responses;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Covalent
{
    /// <summary>
    /// A controller to aggregate all Covalent-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Covalent API.")]
    public sealed class CovalentController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/covalent";

        /// <summary>
        /// Common tag for Covalent actions.
        /// </summary>
        internal const string CovalentTag = "Covalent";

        private readonly ILogger<CovalentController> _logger;
        private readonly ICovalentService _covalentService;

        /// <summary>
        /// Initialize <see cref="CovalentController"/>.
        /// </summary>
        /// <param name="covalentExplorerService"><see cref="ICovalentService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public CovalentController(
            ICovalentService covalentExplorerService,
            ILogger<CovalentController> logger)
        {
            _covalentService = covalentExplorerService ?? throw new ArgumentNullException(nameof(covalentExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all blockchains supported by Covalent.
        /// </summary>
        /// <returns>Returns all blockchains supported by Covalent.</returns>
        /// <remarks>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// Sample request:
        ///
        ///     GET /api/v1/covalent/all-chains
        /// </remarks>
        /// <response code="200">Returns all blockchains supported by Covalent.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("all-chains", Name = "GetCovalentBlockchains")]
        [SwaggerOperation(
            OperationId = "GetCovalentBlockchains",
            Tags = new[] { CovalentTag })]
        [ProducesResponseType(typeof(CovalentChainsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AllChainsAsync(
            CancellationToken cancellationToken)
        {
            var result = await _covalentService.AllChainsAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get balances of tokens for wallet address.
        /// </summary>
        /// <param name="chain" example="eth-mainnet">Covalent blockchain internal id.</param>
        /// <param name="address">Wallet address.</param>
        /// <param name="fetchNft">Fetch NFTs.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns balances of tokens for wallet address.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/covalent/token-balances?address=0xfbc44654dd3d5a7a5ea1267928b6e61203856395&amp;chain=eth-mainnet&amp;fetchNft=false
        /// </remarks>
        /// <response code="200">Returns balances of tokens for wallet address.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("token-balances", Name = "GetCovalentTokenBalances")]
        [SwaggerOperation(
            OperationId = "GetCovalentTokenBalances",
            Tags = new[] { CovalentTag })]
        [ProducesResponseType(typeof(CovalentTokenBalancesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> WalletBalancesAsync(
            [Required(ErrorMessage = "The blockchain should be set")] string chain,
            [Required(ErrorMessage = "The wallet address should be set")] string address,
            bool fetchNft = false,
            CancellationToken cancellationToken = default)
        {
            var result = await _covalentService.WalletBalancesAsync(
                chain,
                address,
                fetchNft,
                cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get balances of NFT tokens for wallet address.
        /// </summary>
        /// <param name="noSpam" example="true">No spam.</param>
        /// <param name="chain" example="eth-mainnet">Covalent blockchain internal id.</param>
        /// <param name="address">Wallet address.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns balances of NFT tokens for wallet address.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/covalent/nft-balances?address=0xfbc44654dd3d5a7a5ea1267928b6e61203856395&amp;chain=eth-mainnet
        /// </remarks>
        /// <response code="200">Returns balances of NFT tokens for wallet address.</response>
        /// <response code="400">Request data not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("nft-balances", Name = "GetCovalentNftTokenBalances")]
        [SwaggerOperation(
            OperationId = "GetCovalentNftTokenBalances",
            Tags = new[] { CovalentTag })]
        [ProducesResponseType(typeof(CovalentNftsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> WalletBalancesAsync(
            bool noSpam,
            [Required(ErrorMessage = "The blockchain should be set")] string chain,
            [Required(ErrorMessage = "The wallet address should be set")] string address,
            CancellationToken cancellationToken = default)
        {
            var result = await _covalentService.WalletNftsAsync(
                chain,
                address,
                noSpam,
                cancellationToken);
            return Ok(result);
        }
    }
}