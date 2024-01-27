// ------------------------------------------------------------------------------------------------------
// <copyright file="IDexSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Dex.Abstractions.Contracts;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Dex.Abstractions
{
    /// <summary>
    /// DEX settings.
    /// </summary>
    public interface IDexSettings :
        ISettings
    {
        /// <summary>
        /// List of DEX descriptors data.
        /// </summary>
        public IList<DexDescriptor>? DexDescriptors { get; init; }
    }
}