// ------------------------------------------------------------------------------------------------------
// <copyright file="EntitySettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;
using Nomis.Utils.Contracts.Deleting;

namespace Nomis.Domain.Settings
{
    /// <summary>
    /// Entity settings.
    /// </summary>
    public class EntitySettings :
        ISettings
    {
        /// <summary>
        /// "soft delete" is enabled.
        /// </summary>
        /// <remarks>
        /// Using for <see cref="ISoftDelete"/>.
        /// </remarks>
        public bool SoftDeleteEnabled { get; init; } = true;

        /// <summary>
        /// Enable audit.
        /// </summary>
        public bool EnableAudit { get; init; } = false;
    }
}