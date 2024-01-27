// ------------------------------------------------------------------------------------------------------
// <copyright file="ISnapshotGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.Snapshot.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with Snapshot API.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.snapshot.org/graphql-api"/>
    /// </remarks>
    // ReSharper disable once InconsistentNaming
    public interface ISnapshotGraphQLClient :
        IGraphQLClient
    {
    }
}