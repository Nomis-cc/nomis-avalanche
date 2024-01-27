// ------------------------------------------------------------------------------------------------------
// <copyright file="ILlamaFolioRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace Nomis.DefiLlama.Interfaces.Contracts
{
    /// <summary>
    /// LlamaFolio request.
    /// </summary>
    public interface ILlamaFolioRequest
    {
        /// <summary>
        /// Use LlamaFolio API.
        /// </summary>
        public bool UseLlamaFolioAPI { get; set; }
    }
}