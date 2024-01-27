// ------------------------------------------------------------------------------------------------------
// <copyright file="MailServicesController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Api.MailServices.Requests;
using Nomis.MailingService.Interfaces;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.MailServices
{
    /// <summary>
    /// A controller to aggregate all Mail services-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Mail services.")]
    public class MailServicesController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/mail";

        /// <summary>
        /// Common tag for mail services actions.
        /// </summary>
        internal const string MailServicesTag = "MailServices";

        private readonly ILogger<MailServicesController> _logger;
        private readonly IMailingService _mailingService;

        /// <summary>
        /// Initialize <see cref="MailServicesController"/>.
        /// </summary>
        /// <param name="mailingService"><see cref="IMailingService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public MailServicesController(
            IMailingService mailingService,
            ILogger<MailServicesController> logger)
        {
            _mailingService = mailingService ?? throw new ArgumentNullException(nameof(mailingService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Add mail subscription contact.
        /// </summary>
        /// <param name="request">Add mail subscription contact request.</param>
        /// <response code="200">All fine.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpPost("subscriber", Name = "AddSubscriber")]
        [SwaggerOperation(
            OperationId = "AddSubscriber",
            Tags = new[] { MailServicesTag })]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddContactAsync(
            [FromBody] AddContactRequest request)
        {
            return Ok(await _mailingService.AddContact(request.ContactEmail).ConfigureAwait(false));
        }
    }
}