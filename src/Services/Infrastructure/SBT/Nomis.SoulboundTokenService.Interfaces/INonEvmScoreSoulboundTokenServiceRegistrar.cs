// ------------------------------------------------------------------------------------------------------
// <copyright file="INonEvmScoreSoulboundTokenServiceRegistrar.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Services;

namespace Nomis.SoulboundTokenService.Interfaces
{
    /// <inheritdoc cref="IServiceRegistrar"/>
    /// <remarks>
    /// Is not EVM-compatible.
    /// </remarks>
    public interface INonEvmScoreSoulboundTokenServiceRegistrar :
        IServiceRegistrar
    {
    }
}