// ------------------------------------------------------------------------------------------------------
// <copyright file="DeFiGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using Nomis.DeFi.Interfaces;

namespace Nomis.DeFi
{
    /// <inheritdoc cref="GraphQLHttpClient"/>
// ReSharper disable once InconsistentNaming
    internal sealed class DeFiGraphQLClient :
        GraphQLHttpClient,
        IDeFiGraphQLClient
    {
        /// <summary>
        /// Initialize <see cref="DeFiGraphQLClient"/>.
        /// </summary>
        /// <param name="endPoint">Endpoint.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public DeFiGraphQLClient(string endPoint, IGraphQLWebsocketJsonSerializer serializer)
            : base(endPoint, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="DeFiGraphQLClient"/>.
        /// </summary>
        /// <param name="endPoint">Endpoint.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public DeFiGraphQLClient(Uri endPoint, IGraphQLWebsocketJsonSerializer serializer)
            : base(endPoint, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="DeFiGraphQLClient"/>.
        /// </summary>
        /// <param name="configure"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public DeFiGraphQLClient(Action<GraphQLHttpClientOptions> configure, IGraphQLWebsocketJsonSerializer serializer)
            : base(configure, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="DeFiGraphQLClient"/>.
        /// </summary>
        /// <param name="options"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public DeFiGraphQLClient(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer)
            : base(options, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="DeFiGraphQLClient"/>.
        /// </summary>
        /// <param name="options"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        /// <param name="httpClient"><see cref="HttpClient"/>.</param>
        public DeFiGraphQLClient(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer, HttpClient httpClient)
            : base(options, serializer, httpClient)
        {
        }
    }
}