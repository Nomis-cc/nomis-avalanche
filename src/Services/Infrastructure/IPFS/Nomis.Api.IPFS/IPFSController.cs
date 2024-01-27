// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFSController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Ipfs.CoreApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.IPFS.Interfaces;
using Nomis.IPFS.Interfaces.Requests;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

// ReSharper disable InconsistentNaming
namespace Nomis.Api.IPFS
{
    /// <summary>
    /// A controller to aggregate all IPFS-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("IPFS service.")]
    public sealed class IPFSController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/ipfs";

        /// <summary>
        /// Common tag for IPFS actions.
        /// </summary>
        internal const string IPFSTag = "IPFS";

        private readonly ILogger<IPFSController> _logger;
        private readonly IIPFSService _ipfsService;

        /// <summary>
        /// Initialize <see cref="IPFSController"/>.
        /// </summary>
        /// <param name="ipfsService"><see cref="IIPFSService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public IPFSController(
            IIPFSService ipfsService,
            ILogger<IPFSController> logger)
        {
            _ipfsService = ipfsService ?? throw new ArgumentNullException(nameof(ipfsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Download file from IPFS by id.
        /// </summary>
        /// <param name="fileId">File id.</param>
        /// <returns>Returns downloaded file data.</returns>
        /// <response code="200">Returns downloaded file data.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("download/{fileId}", Name = "IPFSDownloadFile")]
        [SwaggerOperation(
            OperationId = "IPFSDownloadFile",
            Tags = new[] { IPFSTag })]
        [ProducesResponseType(typeof(Result<byte[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DownloadFileAsync(
            string fileId)
        {
            var result = await _ipfsService.DownloadFileAsync(fileId);
            return Ok(result);
        }

        /// <summary>
        /// Upload file from IPFS by id.
        /// </summary>
        /// <param name="file">Uploaded file.</param>
        /// <param name="options">Upload file options.</param>
        /// <returns>Returns uploaded file data.</returns>
        /// <response code="200">Returns uploaded file data.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("upload", Name = "IPFSUploadFile")]
        [SwaggerOperation(
            OperationId = "IPFSUploadFile",
            Tags = new[] { IPFSTag })]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UploadFileAsync(
            IFormFile file,
            [FromBody] AddFileOptions options)
        {
            var result = await _ipfsService.UploadFileAsync(new IPFSUploadFileRequest
            {
                FileContent = file.OpenReadStream(),
                FileName = file.FileName,
                Options = options
            });
            return Ok(result.Data);
        }
    }
}