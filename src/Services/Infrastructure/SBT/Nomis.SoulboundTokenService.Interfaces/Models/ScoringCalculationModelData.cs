// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoringCalculationModelData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Enums;
using Nomis.Utils.Extensions;

namespace Nomis.SoulboundTokenService.Interfaces.Models
{
    /// <summary>
    /// Scoring calculation model data.
    /// </summary>
    public sealed class ScoringCalculationModelData
    {
        /// <inheritdoc cref="ScoringCalculationModel"/>
        public ScoringCalculationModel Model { get; init; }

        /// <summary>
        /// Scoring calculation model name.
        /// </summary>
        public string Name => Model.ToString();

        /// <summary>
        /// Scoring calculation model description.
        /// </summary>
        public string Description => Model.ToDescriptionString();
    }
}