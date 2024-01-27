// ------------------------------------------------------------------------------------------------------
// <copyright file="ILensProtocolGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.LensProtocol.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with LensProtocol API.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.lens.xyz/docs/developer-quickstart"/>
    /// </remarks>
    // ReSharper disable once InconsistentNaming
    public interface ILensProtocolGraphQLClient :
        IGraphQLClient
    {
    }
}