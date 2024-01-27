// ------------------------------------------------------------------------------------------------------
// <copyright file="TallyService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using Microsoft.Extensions.Options;
using Nomis.Tally.Interfaces;
using Nomis.Tally.Interfaces.Models;
using Nomis.Tally.Interfaces.Requests;
using Nomis.Tally.Interfaces.Settings;
using Nomis.Tally.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Converters;
using Nomis.Utils.Wrapper;

namespace Nomis.Tally
{
    /// <inheritdoc cref="ITallyService"/>
    internal sealed class TallyService :
        ITallyService,
        ISingletonService
    {
        private readonly ITallyGraphQLClient _client;

        /// <summary>
        /// Initialize <see cref="TallyService"/>.
        /// </summary>
        /// <param name="settings"><see cref="TallySettings"/>.</param>
        /// <param name="client"><see cref="ITallyGraphQLClient"/>.</param>
        public TallyService(
            IOptions<TallySettings> settings,
            ITallyGraphQLClient client)
        {
            Settings = settings.Value;
            _client = client;
        }

        public ITallySettings Settings { get; }

        /// <inheritdoc/>
        public async Task<Result<TallyAccount?>> GetTallyAccountDataAsync(
            TallyAccountRequest request)
        {
            var accountRequest = new
            {
                Addresses = new List<string>
                {
                    request.Address
                }
            };
            var query = new GraphQLRequest
            {
                Query = """
                query Accounts(
                  $addresses: [Address!]
                ) {
                  accounts(
                    addresses: $addresses
                  ) {
                    id
                    address
                    ens
                    twitter
                    name
                    bio
                    participations {
                      governor {
                        id
                        type
                        quorum
                        tokens {
                          id
                          type
                          name
                          symbol
                          decimals
                        }
                        name
                        slug
                      }
                      votes {
                        id
                        support
                        weight
                        transaction {
                          id
                          block {
                            number
                            timestamp
                          }
                        }
                        reason
                        proposal {
                          id
                          title
                          description
                          start {
                            number
                            timestamp
                          }
                          end {
                            number
                            timestamp
                          }
                          block {
                            number
                            timestamp
                          }
                          proposer {
                            id
                            address
                            ens
                            twitter
                            name
                            bio
                          }
                          voteStats {
                            support
                            weight
                            votes
                            percent
                          }
                        }
                      }
                      proposals {
                        id
                        title
                        description
                        start {
                          id
                          number
                          timestamp
                        }
                        end {
                          id
                          number
                          timestamp
                        }
                        eta
                        block {
                          id
                          number
                          timestamp
                        }
                        proposer {
                          id
                          address
                          ens
                          twitter
                          name
                          bio
                        }
                        voteStats {
                          support
                          weight
                          votes
                          percent
                        }
                      }
                      stats {
                        delegationCount
                        activeDelegationCount
                        createdProposalsCount
                        voteCount
                        recentParticipationRate {
                          recentVoteCount
                          recentProposalCount
                        }
                        tokenBalance
                      }
                    }
                    picture
                  }
                }
                """,
                Variables = accountRequest
            };

            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var accounts = JsonSerializer.Deserialize<List<TallyAccount>>(response.Data["accounts"]?.ToJsonString(new()
            {
                Converters = { new BigIntegerConverter() }
            }) !);
            return await Result<TallyAccount?>.SuccessAsync(accounts?.Any() != true ? null : accounts.Find(a => a.Id?.Equals($"eip155:{request.ChainId}:{request.Address}", StringComparison.InvariantCultureIgnoreCase) == true), "Account data received.").ConfigureAwait(false);
        }
    }
}