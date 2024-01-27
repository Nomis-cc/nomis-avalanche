// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using Microsoft.Extensions.Logging;
using Nomis.LensProtocol.Interfaces;
using Nomis.LensProtocol.Interfaces.Models;
using Nomis.LensProtocol.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.LensProtocol
{
    /// <inheritdoc cref="ILensProtocolService"/>
    internal sealed class LensProtocolService :
        ILensProtocolService,
        ISingletonService
    {
        private readonly ILensProtocolGraphQLClient _client;
        private readonly ILogger<LensProtocolService> _logger;

        /// <summary>
        /// Initialize <see cref="LensProtocolService"/>.
        /// </summary>
        /// <param name="client"><see cref="ILensProtocolGraphQLClient"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public LensProtocolService(
            ILensProtocolGraphQLClient client,
            ILogger<LensProtocolService> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Result<LensProtocolProfileData?>> ProfileDataAsync(
            LensProtocolProfileRequest request)
        {
            try
            {
                var query = new GraphQLRequest
                {
                    Query = """
                    query Profiles($owner: EthereumAddress!) {
                      profiles(request: { ownedBy: [$owner], limit: 1 }) {
                        items {
                          id
                          name
                          bio
                          attributes {
                            displayType
                            traitType
                            key
                            value
                          }
                          followNftAddress
                          metadata
                          isDefault
                          handle
                          ownedBy
                          stats {
                            totalFollowers
                            totalFollowing
                            totalPosts
                            totalComments
                            totalMirrors
                            totalPublications
                            totalCollects
                          }
                        }
                      }
                    }
                    """,
                    Variables = request
                };

                var data = await GetDataAsync<List<LensProtocolProfileData>>(query, "profiles", "items").ConfigureAwait(false);

                return await Result<LensProtocolProfileData?>.SuccessAsync(data?.FirstOrDefault(), "Profile data received.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There is an error when getting profile data by owner.");
            }

            return await Result<LensProtocolProfileData?>.FailAsync(null, "There is an error when getting profile data by owner.").ConfigureAwait(false);
        }

        private async Task<TResult?> GetDataAsync<TResult>(GraphQLRequest query, params string[] responseAliases)
        {
            var responseAliasList = responseAliases.ToList();
            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var result = response.Data[responseAliasList[0]];
            responseAliasList.RemoveAt(0);
            foreach (string responseAlias in responseAliasList)
            {
                if (result == null)
                {
                    return default;
                }

                result = result[responseAlias];
            }

            var data = JsonSerializer.Deserialize<TResult?>(result!.ToJsonString()) !;

            return data;
        }
    }
}