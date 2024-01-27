// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyReporterRole.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.HapiExplorer.Interfaces.Enums
{
    /// <summary>
    /// HAPI proxy reporter role.
    /// </summary>
    public enum HapiProxyReporterRole :
        byte
    {
        /// <summary>
        /// Validator - can validate addresses.
        /// </summary>
        Validator,

        /// <summary>
        /// Tracer - can report and validate addresses.
        /// </summary>
        Tracer,

        /// <summary>
        /// Publisher - can report cases and addresses.
        /// </summary>
        Publisher,

        /// <summary>
        /// Authority - can report and modify cases and addresses.
        /// </summary>
        Authority
    }
}