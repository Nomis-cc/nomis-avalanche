// ------------------------------------------------------------------------------------------------------
// <copyright file="ITallyGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.Tally.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with Tally API.
    /// </summary>
    /// <remarks>
    /// <see href="https://apidocs.tally.xyz/"/>
    /// </remarks>
    // ReSharper disable once InconsistentNaming
    public interface ITallyGraphQLClient :
        IGraphQLClient
    {
    }
}