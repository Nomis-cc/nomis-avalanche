// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using Nomis.Snapshot.Interfaces;
using Nomis.Snapshot.Interfaces.Models;
using Nomis.Snapshot.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Snapshot
{
    /// <inheritdoc cref="ISnapshotService"/>
    internal sealed class SnapshotService :
        ISnapshotService,
        ISingletonService
    {
        private readonly ISnapshotGraphQLClient _client;

        /// <summary>
        /// Initialize <see cref="SnapshotService"/>.
        /// </summary>
        /// <param name="client"><see cref="ISnapshotGraphQLClient"/>.</param>
        public SnapshotService(
            ISnapshotGraphQLClient client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public async Task<Result<List<SnapshotVote>?>> GetSnapshotVotesAsync(
            GetSnapshotVotesRequest request)
        {
            request.Spaces = request.Spaces.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var query = new GraphQLRequest
            {
                Query = """
                query Votes($first: Int, $skip: Int, $orderBy: String, $orderDirection: OrderDirection, $voter: String, $spaces: [String]) {
                  votes(
                    first: $first
                    skip: $skip
                    where: {vp_gt: 0, voter: $voter, space_in: $spaces}
                    orderBy: $orderBy
                    orderDirection: $orderDirection
                  ) {
                    id
                    space{
                      id
                      name
                      private
                      about
                      avatar
                      website
                      twitter
                      network
                      symbol
                    }
                    ipfs
                    voter
                    choice
                    vp
                    vp_by_strategy
                  }
                }
                """,
                Variables = request
            };

            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var votes = JsonSerializer.Deserialize<List<SnapshotVote>>(response.Data["votes"]?.ToJsonString(new()) !);
            return await Result<List<SnapshotVote>?>.SuccessAsync(votes?.Any() != true ? null : votes!.Where(v => v.Space?.Network?.Equals(request.ChainId.ToString()) == true).ToList(), "Votes received.").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Result<List<SnapshotProposal>?>> GetSnapshotProposalsAsync(
            GetSnapshotProposalsRequest request)
        {
            request.Spaces = request.Spaces.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var query = new GraphQLRequest
            {
                Query = """
                query Proposals($first: Int, $skip: Int, $orderBy: String, $orderDirection: OrderDirection, $author: String, $spaces: [String]) {
                  proposals(
                    first: $first
                    skip: $skip,
                    where: {
                      space_in: $spaces, author: $author
                    },
                    orderBy: $orderBy
                    orderDirection: $orderDirection
                  ) {
                    id
                    title
                    body
                    choices
                    start
                    end
                    snapshot
                    state
                    author
                    ipfs
                    created
                    type
                    discussion
                    link
                    scores
                    votes
                    space {
                      id
                      name
                      private
                      about
                      avatar
                      terms
                      location
                      website
                      twitter
                      network
                      symbol
                    }
                  }
                }
                """,
                Variables = request
            };

            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var proposals = JsonSerializer.Deserialize<List<SnapshotProposal>>(response.Data["proposals"]?.ToJsonString(new()) !);
            return await Result<List<SnapshotProposal>?>.SuccessAsync(proposals?.Any() != true ? null : proposals!.Where(p => p.Space?.Network?.Equals(request.ChainId.ToString()) == true).ToList(), "Proposals received.").ConfigureAwait(false);
        }
    }
}