// ------------------------------------------------------------------------------------------------------
// <copyright file="ITallyService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Tally.Interfaces.Models;
using Nomis.Tally.Interfaces.Requests;
using Nomis.Tally.Interfaces.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Tally.Interfaces
{
    /// <summary>
    /// Service for interaction with Tally API.
    /// </summary>
    public interface ITallyService :
        IInfrastructureService
    {
        /// <inheritdoc cref="ITallySettings"/>
        public ITallySettings Settings { get; }

        /// <summary>
        /// Get the Tally account data.
        /// </summary>
        /// <param name="request">Tally account request.</param>
        /// <returns>Returns Tally account data.</returns>
        public Task<Result<TallyAccount?>> GetTallyAccountDataAsync(
            TallyAccountRequest request);
    }
}