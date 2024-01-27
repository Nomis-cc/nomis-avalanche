// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLoggerSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.DataAccess.PostgreSql.Settings
{
    /// <summary>
    /// Event logger settings.
    /// </summary>
    internal class EventLoggerSettings :
        ISettings
    {
        /// <summary>
        /// Use event logger.
        /// </summary>
        public bool UseEventLogger { get; init; }
    }
}