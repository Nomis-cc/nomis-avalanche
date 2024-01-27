// ------------------------------------------------------------------------------------------------------
// <copyright file="IBaseEvmTransferList.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.Blockchain.Abstractions.Contracts.Models
{
    /// <summary>
    /// Transfer list.
    /// </summary>
    public interface IBaseEvmTransferList<TListItem>
        where TListItem : IBaseEvmTransfer
    {
        /// <summary>
        /// Transfer list.
        /// </summary>
        [JsonPropertyName("result")]
        [DataMember(EmitDefaultValue = true)]
        public IList<TListItem>? Data { get; set; }
    }
}