// ------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text.Json;

using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;
using Serilog.Context;

namespace Nomis.Api.Common.Middlewares
{
    /// <summary>
    /// Global exception handler middleware.
    /// </summary>
    public class ExceptionHandlingMiddleware :
        IMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initialize <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="env"><see cref="IHostEnvironment"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ExceptionHandlingMiddleware(
            IHostEnvironment env,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                if (exception is HttpRequestException httpRequestException)
                {
                    LogContext.PushProperty(nameof(httpRequestException.HttpRequestError), httpRequestException.HttpRequestError);
                    LogContext.PushProperty(nameof(httpRequestException.StatusCode), httpRequestException.StatusCode);
                    LogContext.PushProperty(nameof(httpRequestException.Message), httpRequestException.Message);
                }

                if (exception is SocketException socketException)
                {
                    LogContext.PushProperty(nameof(socketException.SocketErrorCode), socketException.SocketErrorCode);
                    LogContext.PushProperty(nameof(socketException.Message), socketException.Message);
                }

                if (exception is AggregateException aggregateException)
                {
                    LogContext.PushProperty(nameof(aggregateException.InnerExceptions), string.Join(Environment.NewLine, aggregateException.InnerExceptions.Select(x => x.Message)));
                    LogContext.PushProperty(nameof(aggregateException.Message), aggregateException.Message);
                }

                string errorId = Guid.NewGuid().ToString();
                LogContext.PushProperty(nameof(errorId).Pascalize(), errorId);

                var responseModel = await ErrorResult<string>.ReturnErrorAsync(exception.Message).ConfigureAwait(false);
                responseModel.Source = exception.TargetSite?.DeclaringType?.FullName?.Trim();

                if (_env.IsDevelopment())
                {
                    responseModel.Exception = exception.Message.Trim();
                }

                responseModel.ErrorId = errorId;
                responseModel.SupportMessage = "Provide the ErrorId to the support team for further analysis.";
                try
                {
                    if (_env.IsDevelopment())
                    {
                        int? pos = exception.StackTrace?.IndexOf(Environment.NewLine, StringComparison.OrdinalIgnoreCase);
                        responseModel.StackTrace = exception.StackTrace?.Trim()
                            .Substring(0, pos != null ? (int)pos - 3 : exception.StackTrace?.Trim().Length ?? 0).Trim() ?? string.Empty;
                    }
                }
                catch
                {
                    // ignored
                }

                switch (exception)
                {
                    // TODO - add specific exceptions

                    case CustomException e:
                        _logger.LogWarning(e, exception.Message);
                        response.StatusCode = responseModel.StatusCode = (int)e.StatusCode;
                        if (e.ErrorMessages != null && e.ErrorMessages.Count != 0)
                        {
                            foreach (string errorMessage in e.ErrorMessages)
                            {
                                responseModel.Messages.Add(errorMessage);
                            }
                        }

                        break;

                    default:
                        _logger.LogCritical(exception, exception.Message);
                        response.StatusCode = responseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Messages = new List<string> { "An error has occurred" };
                        break;
                }

                string result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await response.WriteAsync(result).ConfigureAwait(false);
            }
        }
    }
}