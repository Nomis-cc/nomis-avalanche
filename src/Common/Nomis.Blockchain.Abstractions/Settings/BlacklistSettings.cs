// ------------------------------------------------------------------------------------------------------
// <copyright file="BlacklistSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Enums;
using Nomis.Utils.Contracts.Common;

namespace Nomis.Blockchain.Abstractions.Settings
{
    /// <summary>
    /// Blacklist settings.
    /// </summary>
    public class BlacklistSettings :
        ISettings
    {
        /// <summary>
        /// Use blacklist.
        /// </summary>
        public bool UseBlacklist { get; set; }

        /// <summary>
        /// Wallets blacklist.
        /// </summary>
        public IDictionary<BlacklistType, List<string>> Blacklist { get; set; } = new Dictionary<BlacklistType, List<string>>();
    }
}