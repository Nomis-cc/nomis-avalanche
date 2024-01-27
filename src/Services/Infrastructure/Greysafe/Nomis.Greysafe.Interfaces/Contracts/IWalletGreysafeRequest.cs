// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletGreysafeRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Properties;

namespace Nomis.Greysafe.Interfaces.Contracts
{
    /// <summary>
    /// Wallet Greysafe scam reporting service request.
    /// </summary>
    public interface IWalletGreysafeRequest :
        IHasAddress
    {
        /// <summary>
        /// Get wallet Greysafe scam reporting service data (with adjusting score value).
        /// </summary>
        /// <remarks>
        /// You can not mint token without Greysafe adjusting score.
        /// </remarks>
        public bool GetGreysafeData { get; set; }
    }
}