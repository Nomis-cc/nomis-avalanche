// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.CyberConnect.Interfaces.Models;

namespace Nomis.CyberConnect.Interfaces.Responses
{
    /// <summary>
    /// CyberConnect data.
    /// </summary>
    public class CyberConnectData
    {
        /// <inheritdoc cref="CyberConnectProfileData"/>
        public CyberConnectProfileData? Profile { get; set; }

        /// <summary>
        /// The collection of <see cref="CyberConnectEssenceData"/>.
        /// </summary>
        public IEnumerable<CyberConnectEssenceData>? Essences { get; set; }

        /// <summary>
        /// The collection of <see cref="CyberConnectLikeData"/>.
        /// </summary>
        public IEnumerable<CyberConnectLikeData>? Likes { get; set; }

        /// <summary>
        /// The collection of <see cref="CyberConnectSubscribingProfileData"/>.
        /// </summary>
        public IEnumerable<CyberConnectSubscribingProfileData>? Subscribings { get; set; }
    }
}