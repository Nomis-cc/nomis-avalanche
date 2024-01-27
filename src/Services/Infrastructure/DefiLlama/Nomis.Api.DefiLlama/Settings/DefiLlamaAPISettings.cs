// ------------------------------------------------------------------------------------------------------
// <copyright file="DefiLlamaAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.DefiLlama.Settings
{
    /// <summary>
    /// DefiLlama API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class DefiLlamaAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; init; }

        /// <inheritdoc/>
        public string APIName => DefiLlamaController.DefiLlamaTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(DefiLlamaController);
    }
}