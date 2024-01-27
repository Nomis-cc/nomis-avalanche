// ------------------------------------------------------------------------------------------------------
// <copyright file="IDeFiGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.DeFi.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with De.Fi API.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.de.fi/api/api"/>
    /// </remarks>
    // ReSharper disable once InconsistentNaming
    public interface IDeFiGraphQLClient :
        IGraphQLClient
    {
    }
}