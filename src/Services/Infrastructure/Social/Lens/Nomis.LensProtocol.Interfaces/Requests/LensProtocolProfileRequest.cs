// ------------------------------------------------------------------------------------------------------
// <copyright file="LensProtocolProfileRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.LensProtocol.Interfaces.Requests
{
    /// <summary>
    /// LensProtocol Protocol profile request.
    /// </summary>
    public class LensProtocolProfileRequest
    {
        /// <summary>
        /// Owner.
        /// </summary>
        /// <example>0x896ea1fB558fe8D075F0cC7f52495eB4052dae23</example>
        public string? Owner { get; set; } = "0x896ea1fB558fe8D075F0cC7f52495eB4052dae23";
    }
}