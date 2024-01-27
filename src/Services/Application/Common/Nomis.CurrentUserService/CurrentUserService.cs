// ------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUserService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Nomis.CurrentUserService.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.CurrentUserService
{
    /// <inheritdoc cref="ICurrentUserService"/>
    internal sealed class CurrentUserService :
        ICurrentUserService,
        ITransientService
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Initialize <see cref="CurrentUserService"/>.
        /// </summary>
        /// <param name="accessor"><see cref="IHttpContextAccessor"/>.</param>
        public CurrentUserService(
            IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <inheritdoc/>
        public Guid GetUserId()
        {
            string? clientId = _accessor.HttpContext?.Request?.Headers["X-ClientId"]; // TODO - replace to settings or get from API key
            if (Guid.TryParse(clientId, out var clientIdGuid))
            {
                return clientIdGuid;
            }

            return Guid.Empty;
        }

        /// <inheritdoc/>
        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
}