// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralsController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.ReferralService.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Referrals
{
    /// <summary>
    /// A controller to aggregate all Referrals-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Referrals service.")]
    public sealed class ReferralsController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/referrals";

        /// <summary>
        /// Common tag for Referrals actions.
        /// </summary>
        internal const string ReferralsTag = "Referrals";

        private readonly ILogger<ReferralsController> _logger;
        private readonly IReferralService _referralsService;

        /// <summary>
        /// Initialize <see cref="ReferralsController"/>.
        /// </summary>
        /// <param name="referralsService"><see cref="IReferralService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ReferralsController(
            IReferralService referralsService,
            ILogger<ReferralsController> logger)
        {
            _referralsService = referralsService ?? throw new ArgumentNullException(nameof(referralsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}