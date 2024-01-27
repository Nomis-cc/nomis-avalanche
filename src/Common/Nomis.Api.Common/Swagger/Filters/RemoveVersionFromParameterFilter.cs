// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveVersionFromParameterFilter.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Common.Swagger.Filters
{
    /// <summary>
    /// Фильтр для удаления версии API из параметров в swagger документации.
    /// </summary>
    public class RemoveVersionFromParameterFilter :
        IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count == 0)
            {
                return;
            }

            var versionParameter = operation.Parameters.Single(p => string.Equals(p.Name, "version", StringComparison.OrdinalIgnoreCase));
            operation.Parameters.Remove(versionParameter);
        }
    }
}