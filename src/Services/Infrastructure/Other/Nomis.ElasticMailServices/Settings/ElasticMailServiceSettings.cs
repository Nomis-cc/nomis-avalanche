// ------------------------------------------------------------------------------------------------------
// <copyright file="ElasticMailServiceSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.ElasticMailServices.Settings
{
    /// <summary>
    /// Elastic Mail mailer service settings.
    /// </summary>
    public class ElasticMailServiceSettings :
        ISettings
    {
        /// <summary>
        /// Base URL of Elastic Mail API.
        /// </summary>
        public string BaseUrl { get; init; } = null!;

        /// <summary>
        /// Elastic Mail API KEY.
        /// </summary>
        public string ElasticMailApiKey { get; init; } = null!;

        /// <summary>
        /// Name of list where new contacts added.
        /// </summary>
        public string NewContactsListName { get; init; } = null!;

        /// <summary>
        /// Name of template to send newly added contact.
        /// </summary>
        public string WelcomeMailTemplateName { get; init; } = null!;
    }
}