// ------------------------------------------------------------------------------------------------------
// <copyright file="RateLimitResult.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Wrapper;

namespace Nomis.Api.Common.Swagger.Examples
{
    /// <summary>
    /// Rate limit result example.
    /// </summary>
    public class RateLimitResult :
        IResult
    {
        /// <inheritdoc />
        /// <example>
        /// <![CDATA[
        /// [ "Whoa! Calm down, cowboy! Quota exceeded. Maximum allowed: 3 per 1s. Please try again in 1 second(s)." ]
        /// ]]>
        /// </example>
        public IList<string> Messages { get; set; } = new List<string>();

        /// <inheritdoc />
        /// <example>false</example>
        public bool Succeeded { get; set; }
    }
}