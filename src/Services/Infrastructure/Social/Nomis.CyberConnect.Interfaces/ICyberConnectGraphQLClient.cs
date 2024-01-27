// ------------------------------------------------------------------------------------------------------
// <copyright file="ICyberConnectGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.CyberConnect.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with CyberConnect API.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.cyberconnect.me/api/introduction"/>
    /// </remarks>
    // ReSharper disable once InconsistentNaming
    public interface ICyberConnectGraphQLClient :
        IGraphQLClient
    {
    }
}