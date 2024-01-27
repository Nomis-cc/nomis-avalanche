// ------------------------------------------------------------------------------------------------------
// <copyright file="StatDataResponse.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using Nomis.Utils.Contracts.Deleting;

namespace Nomis.ScoringService.Interfaces.Responses
{
    /// <summary>
    /// Stat data response.
    /// </summary>
    public class StatDataResponse :
        ISoftDelete
    {
        /// <summary>
        /// Initialize <see cref="StatDataResponse"/>.
        /// </summary>
        /// <param name="statData">Stat data.</param>
        /// <param name="deletedOn">Deleted on.</param>
        /// <param name="deletedBy">Deleted by.</param>
        /// <param name="createdOn">Created on.</param>
        public StatDataResponse(
            string statData,
            DateTime? deletedOn = null,
            Guid? deletedBy = null,
            DateTime? createdOn = null)
        {
            StatData = statData;
            DeletedOn = deletedOn;
            DeletedBy = deletedBy;
            CreatedOn = createdOn;
        }

        /// <summary>
        /// Scoring stat data.
        /// </summary>
        [JsonInclude]
        public string StatData { get; private set; }

        /// <inheritdoc/>
        public Guid? DeletedBy { get; set; }

        /// <inheritdoc/>
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        /// Created on.
        /// </summary>
        public DateTime? CreatedOn { get; set; }
    }
}