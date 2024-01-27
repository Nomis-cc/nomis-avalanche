// ------------------------------------------------------------------------------------------------------
// <copyright file="HttpClientBuilderExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nomis.Blockchain.Abstractions.Contracts.Settings;
using Nomis.Blockchain.Abstractions.Handlers;
using Nomis.Utils.Contracts.Settings;
using Polly;

namespace Nomis.Blockchain.Abstractions.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IHttpClientBuilder"/>.
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// Add <see cref="TraceLogHandler"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        /// <param name="shouldLog">Should log criteria.</param>
        public static IHttpClientBuilder AddTraceLogHandler(
            this IHttpClientBuilder builder,
            Func<HttpResponseMessage, Task<bool>> shouldLog)
        {
            return builder.AddHttpMessageHandler(services =>
                new TraceLogHandler(services.GetRequiredService<IHttpContextAccessor>(), shouldLog));
        }

        /// <summary>
        /// Add <see cref="TraceLogHandler"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        public static IHttpClientBuilder AddTraceLogHandler(
            this IHttpClientBuilder builder)
        {
            return builder.AddHttpMessageHandler(services =>
                new TraceLogHandler(services.GetRequiredService<IHttpContextAccessor>(), _ => Task.FromResult(false)));
        }

        /// <summary>
        /// Add <see cref="RetryHandler"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        public static IHttpClientBuilder AddRetryHandler(
            this IHttpClientBuilder builder)
        {
            return builder.AddHttpMessageHandler(_ =>
                new RetryHandler());
        }

        /// <summary>
        /// Add <see cref="RetryHandler"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        /// <param name="settings"><see cref="IHttpClientRetryingSettings"/>.</param>
        public static IHttpClientBuilder AddRetryHandler(
            this IHttpClientBuilder builder,
            IHttpClientRetryingSettings settings)
        {
            return builder.AddHttpMessageHandler(_ =>
                new RetryHandler(settings));
        }

        /// <summary>
        /// Add <see cref="RateLimitHttpMessageHandler{TSettings}"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        public static IHttpClientBuilder AddRateLimitHandler(
            this IHttpClientBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<RateLimitHttpMessageHandler<DefaultSettings>>>();
            return builder.AddHttpMessageHandler(_ =>
                new RateLimitHttpMessageHandler<DefaultSettings>(logger, clientName: builder.Name));
        }

        /// <summary>
        /// Add <see cref="RateLimitHttpMessageHandler{TSettings}"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        /// <param name="clientName">Client name.</param>
        public static IHttpClientBuilder AddRateLimitHandler(
            this IHttpClientBuilder builder,
            string clientName)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<RateLimitHttpMessageHandler<DefaultSettings>>>();
            return builder.AddHttpMessageHandler(_ =>
                new RateLimitHttpMessageHandler<DefaultSettings>(logger, clientName: clientName));
        }

        /// <summary>
        /// Add <see cref="RateLimitHttpMessageHandler{TSettings}"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <typeparam name="TSettings">The settings type.</typeparam>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        /// <param name="settings">The rate limit settings.</param>
        public static IHttpClientBuilder AddRateLimitHandler<TSettings>(
            this IHttpClientBuilder builder,
            TSettings settings)
            where TSettings : class, IHttpClientRetryingSettings, IRateLimitSettings, new()
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<RateLimitHttpMessageHandler<TSettings>>>();
            return builder.AddHttpMessageHandler(_ =>
                new RateLimitHttpMessageHandler<TSettings>(logger, settings, builder.Name));
        }

        /// <summary>
        /// Add <see cref="RetryHandler"/> to <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHttpClientBuilder"/>.</param>
        /// <param name="settings"><see cref="IHttpClientRetryingSettings"/>.</param>
        public static IHttpClientBuilder AddPollyRetryHandler(
            this IHttpClientBuilder builder,
            IHttpClientRetryingSettings settings)
        {
            if (!settings.UseHttpClientRetrying)
            {
                return builder;
            }

            return builder
                .AddTransientHttpErrorPolicy(
                x =>
                {
                    return x.WaitAndRetryAsync(
                        settings.MaxRetries,
                        _ =>
                        {
                            if (settings.UseDefaultRetryTimeout)
                            {
                                return settings.DefaultRetryTimeout;
                            }

                            return TimeSpan.Zero;
                        },
                        async (result, _) =>
                        {
                            var response = result.Result;
                            if (!response.IsSuccessStatusCode && settings.RetryTimeouts.TryGetValue(response.StatusCode, out var retryTimeout))
                            {
                                await Task.Delay(retryTimeout).ConfigureAwait(false);
                            }
                        });
                });
        }
    }
}