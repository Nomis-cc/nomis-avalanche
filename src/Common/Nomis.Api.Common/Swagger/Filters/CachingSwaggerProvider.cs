// ------------------------------------------------------------------------------------------------------
// <copyright file="CachingSwaggerProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Common.Swagger.Filters
{
    /// <summary>
    /// Caching provider for swagger documentation.
    /// </summary>
    /// <remarks>
    /// Allows to cache the documentation received by swagger.
    /// </remarks>
    public class CachingSwaggerProvider :
        ISwaggerProvider
    {
        private static readonly ConcurrentDictionary<(string, string?, string?), OpenApiDocument> Cache = new();

        private readonly SwaggerGenerator _swaggerGenerator;

        /// <summary>
        /// Initialize <see cref="CachingSwaggerProvider"/>.
        /// </summary>
        /// <param name="optionsAccessor"><see cref="IOptions{T}"/>.</param>
        /// <param name="apiDescriptionsProvider"><see cref="IApiDescriptionGroupCollectionProvider"/>.</param>
        /// <param name="schemaGenerator"><see cref="ISchemaGenerator"/>.</param>
        public CachingSwaggerProvider(
            IOptions<SwaggerGeneratorOptions> optionsAccessor,
            IApiDescriptionGroupCollectionProvider apiDescriptionsProvider,
            ISchemaGenerator schemaGenerator)
        {
            _swaggerGenerator = new(optionsAccessor.Value, apiDescriptionsProvider, schemaGenerator);
        }

        /// <inheritdoc/>
        public OpenApiDocument GetSwagger(
            string documentName,
            string? host = null,
            string? basePath = null)
        {
            return Cache.GetOrAdd((documentName, host, basePath), _ => _swaggerGenerator.GetSwagger(documentName, host, basePath));
        }
    }
}