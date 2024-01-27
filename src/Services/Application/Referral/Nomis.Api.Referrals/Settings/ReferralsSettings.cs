// ------------------------------------------------------------------------------------------------------
// <copyright file="ReferralsSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Referrals.Settings
{
    /// <summary>
    /// Referrals settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class ReferralsSettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => ReferralsController.ReferralsTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(ReferralsController);
    }
}