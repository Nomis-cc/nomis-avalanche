// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using Nomis.CyberConnect.Interfaces;

namespace Nomis.CyberConnect
{
    /// <inheritdoc cref="GraphQLHttpClient"/>
    // ReSharper disable once InconsistentNaming
    internal sealed class CyberConnectGraphQLClient :
        GraphQLHttpClient,
        ICyberConnectGraphQLClient
    {
        /// <summary>
        /// Initialize <see cref="CyberConnectGraphQLClient"/>.
        /// </summary>
        /// <param name="endPoint">Endpoint.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CyberConnectGraphQLClient(string endPoint, IGraphQLWebsocketJsonSerializer serializer)
            : base(endPoint, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CyberConnectGraphQLClient"/>.
        /// </summary>
        /// <param name="endPoint">Endpoint.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CyberConnectGraphQLClient(Uri endPoint, IGraphQLWebsocketJsonSerializer serializer)
            : base(endPoint, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CyberConnectGraphQLClient"/>.
        /// </summary>
        /// <param name="configure"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CyberConnectGraphQLClient(Action<GraphQLHttpClientOptions> configure, IGraphQLWebsocketJsonSerializer serializer)
            : base(configure, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CyberConnectGraphQLClient"/>.
        /// </summary>
        /// <param name="options"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CyberConnectGraphQLClient(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer)
            : base(options, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CyberConnectGraphQLClient"/>.
        /// </summary>
        /// <param name="options"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        /// <param name="httpClient"><see cref="HttpClient"/>.</param>
        public CyberConnectGraphQLClient(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer, HttpClient httpClient)
            : base(options, serializer, httpClient)
        {
        }
    }
}