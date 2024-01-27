// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiProxyCaseStatus.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.HapiExplorer.Interfaces.Enums
{
    /// <summary>
    /// HAPI proxy case status.
    /// </summary>
    public enum HapiProxyCaseStatus :
        byte
    {
        /// <summary>
        /// Closed.
        /// </summary>
        Closed = 0,

        /// <summary>
        /// Open.
        /// </summary>
        Open = 1
    }
}