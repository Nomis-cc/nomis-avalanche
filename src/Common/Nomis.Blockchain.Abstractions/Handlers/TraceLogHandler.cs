// ------------------------------------------------------------------------------------------------------
// <copyright file="TraceLogHandler.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Nomis.Blockchain.Abstractions.Handlers
{
    internal class TraceLogHandler :
            DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Func<HttpResponseMessage, Task<bool>> _shouldLog;

        /// <summary>
        /// Initialize <see cref="TraceLogHandler"/>.
        /// </summary>
        /// <param name="httpContextAccessor"><see cref="IHttpContextAccessor"/>.</param>
        /// <param name="shouldLog">Should log criteria.</param>
        public TraceLogHandler(
            IHttpContextAccessor httpContextAccessor,
            Func<HttpResponseMessage, Task<bool>> shouldLog)
        {
            _httpContextAccessor = httpContextAccessor;
            _shouldLog = shouldLog;
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            bool logPayloads = false;
            HttpResponseMessage? response = null;
            try
            {
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                // We run the ShouldLog function that calculates, based on HttpResponseMessage, if we should log HttpClient request/response.
                logPayloads = await _shouldLog(response).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // We want to log HttpClient request/response when some exception occurs, so we can reproduce what caused it.
                logPayloads = true;
                throw;
            }
            finally
            {
                // Finally, we check if we decided to log HttpClient request/response or not.
                // Only if we want to, we will have some allocations for the logger and try to read headers and contents.
                if (logPayloads)
                {
                    var logger = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ILogger<TraceLogHandler>>();
                    var scope = new Dictionary<string, object>();

                    scope.TryAdd("request_headers", request);
                    if (request.Content != null)
                    {
                        scope.Add("request_body", await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
                    }

                    if (response != null)
                    {
                        scope.TryAdd("response_headers", response);
                        scope.Add("response_body", await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
                    }

                    using (logger.BeginScope(scope))
                    {
                        logger.LogInformation("[TRACE] HttpClient request/response");
                    }
                }
            }

            return response;
        }
    }
}