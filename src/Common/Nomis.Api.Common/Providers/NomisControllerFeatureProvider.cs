// ------------------------------------------------------------------------------------------------------
// <copyright file="NomisControllerFeatureProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Microsoft.AspNetCore.Mvc.Controllers;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Common.Providers
{
    /// <summary>
    /// A provider to check if the type is a controller.
    /// </summary>
    public class NomisControllerFeatureProvider :
        ControllerFeatureProvider
    {
        private readonly IEnumerable<IApiSettings> _apiSettings;

        /// <summary>
        /// Initialize <see cref="NomisControllerFeatureProvider"/>.
        /// </summary>
        /// <param name="apiSettings">Collection of <see cref="IApiSettings"/>.</param>
        public NomisControllerFeatureProvider(
            IEnumerable<IApiSettings> apiSettings)
        {
            _apiSettings = apiSettings;
        }

        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!base.IsController(typeInfo))
            {
                return false;
            }

            var apiSettings = _apiSettings.FirstOrDefault(x => string.Equals(x.ControllerName, typeInfo.Name, StringComparison.OrdinalIgnoreCase));
            return apiSettings?.APIEnabled != false;
        }
    }
}