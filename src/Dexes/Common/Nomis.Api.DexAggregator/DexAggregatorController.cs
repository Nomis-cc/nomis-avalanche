// ------------------------------------------------------------------------------------------------------
// <copyright file="DexAggregatorController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts.Data;
using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Dex.Abstractions.Contracts;
using Nomis.Dex.Abstractions.Enums;
using Nomis.DexProviderService.Interfaces;
using Nomis.DexProviderService.Interfaces.Contracts;
using Nomis.DexProviderService.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.DexAggregator
{
    /// <summary>
    /// A controller to aggregate all DEX-related actions.
    /// </summary>
    [ApiVersion("1")]
    [Route(BasePath)]
    [SwaggerTag("DEX aggregator.")]
    public sealed class DexAggregatorController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/dex/aggregator";

        /// <summary>
        /// Common tag for Dex aggregator.
        /// </summary>
        internal const string DexAggregatorTag = "DexAggregator";

        private readonly IDexProviderService _dexProviderService;

        /// <summary>
        /// Initialize <see cref="DexAggregatorController"/>.
        /// </summary>
        /// <param name="dexProviderService"><see cref="IDexProviderService"/>.</param>
        public DexAggregatorController(
            IDexProviderService dexProviderService)
        {
            _dexProviderService = dexProviderService ?? throw new ArgumentNullException(nameof(dexProviderService));
        }

        /// <summary>
        /// Get the list stablecoins.
        /// </summary>
        /// <param name="blockchain">Blockchain.</param>
        /// <returns>Returns the list of stablecoins.</returns>
        /// <response code="200">Returns the list of stablecoins.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("stablecoins", Name = "DexGetStablecoins")]
        [SwaggerOperation(
            OperationId = "DexGetStablecoins",
            Tags = new[] { DexAggregatorTag })]
        [ProducesResponseType(typeof(Result<List<StableCoinData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult Stablecoins(
            Chain blockchain)
        {
            var result = _dexProviderService.StablecoinsData(blockchain);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of supported DEX-providers.
        /// </summary>
        /// <param name="blockchain" example="BSC">Blockchain.</param>
        /// <param name="provider" example="BiSwap">DEX provider.</param>
        /// <param name="isEnabled">Is enabled.</param>
        /// <returns>Returns the list of supported DEX-providers.</returns>
        /// <response code="200">Returns the list of supported DEX-providers.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("providers", Name = "DexProvidersData")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "DexProvidersData",
            Tags = new[] { DexAggregatorTag })]
        [ProducesResponseType(typeof(Result<List<DexProviderData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult ProvidersData(
            Chain blockchain,
            DexProvider provider = DexProvider.None,
            bool? isEnabled = null)
        {
            var result = _dexProviderService.ProvidersData(provider, blockchain, isEnabled);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of tokens.
        /// </summary>
        /// <param name="request">Request for getting the list of tokens from supported tokens providers by blockchain.</param>
        /// <returns>Returns the list of tokens.</returns>
        /// <response code="200">Returns the list of tokens.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("tokens", Name = "TokensData")]
        [SwaggerOperation(
            OperationId = "TokensData",
            Tags = new[] { DexAggregatorTag })]
        [ProducesResponseType(typeof(Result<List<TokenData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TokensDataAsync(
            [FromQuery] TokensDataRequest request)
        {
            var result = await _dexProviderService.TokensDataAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of all supported blockchains.
        /// </summary>
        /// <param name="type">Blockchain type.</param>
        /// <param name="isEnabled">Is enabled.</param>
        /// <returns>Returns the list of all supported blockchains.</returns>
        /// <response code="200">Returns the list of all supported blockchains.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("blockchains", Name = "DexBlockchains")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "DexBlockchains",
            Tags = new[] { DexAggregatorTag })]
        [ProducesResponseType(typeof(Result<List<IBlockchainDescriptor>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult AllBlockchains(
            BlockchainType type = BlockchainType.None,
            bool? isEnabled = null)
        {
            var result = _dexProviderService.Blockchains(type, isEnabled);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of swap pairs from all supported DEXes by blockchain.
        /// </summary>
        /// <param name="request">Request for getting the list of swap pairs from all supported DEXes by blockchain.</param>
        /// <returns>Returns the list of swap pairs from all supported DEXes by blockchain.</returns>
        /// <response code="200">Returns the list of swap pairs from all supported DEXes by blockchain.</response>
        /// <response code="400">Request not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("swap-pairs", Name = "DexAggregatorBlockchainSwapPairs")]
        [SwaggerOperation(
            OperationId = "DexAggregatorBlockchainSwapPairs",
            Tags = new[] { DexAggregatorTag })]
        [ProducesResponseType(typeof(Result<List<ISwapPairData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BlockchainSwapPairsAsync(
            [FromQuery] BlockchainSwapPairsRequest request)
        {
            var result = await _dexProviderService.BlockchainSwapPairsAsync(request);
            return Ok(result);
        }
    }
}