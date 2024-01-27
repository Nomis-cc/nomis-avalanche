// ------------------------------------------------------------------------------------------------------
// <copyright file="ScoreType.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Enums
{
    /// <summary>
    /// Score type.
    /// </summary>
    public enum ScoreType :
        byte
    {
        /// <summary>
        /// Finance score.
        /// </summary>
        Finance = 0,

        /// <summary>
        /// Concrete token score.
        /// </summary>
        Token = 1,

        /// <summary>
        /// Migration score.
        /// </summary>
        Migration = 2
    }
}