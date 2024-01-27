// ------------------------------------------------------------------------------------------------------
// <copyright file="PropertyData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Humanizer;

namespace Nomis.Utils.Contracts
{
    /// <summary>
    /// Property data.
    /// </summary>
    public class PropertyData
    {
        /// <summary>
        /// Initialize <see cref="PropertyData"/>.
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/>.</param>
        /// <param name="nativeToken">Native token.</param>
        public PropertyData(
            PropertyInfo propertyInfo,
            string nativeToken)
        {
            var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
            Label = displayAttribute?.Name
                    ?? propertyInfo.Name.Humanize();
            Description = displayAttribute?.Description
                          ?? propertyInfo.GetCustomAttribute<DescriptionAttribute>()?.Description
                          ?? propertyInfo.Name.Humanize();
            Units = displayAttribute?.GroupName ?? "unknown";

            if (Units.Equals("Native token", StringComparison.InvariantCultureIgnoreCase))
            {
                Units = nativeToken;
            }
        }

        /// <summary>
        /// Property label.
        /// </summary>
        public string? Label { get; }

        /// <summary>
        /// Property description.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Units.
        /// </summary>
        public string? Units { get; }
    }
}