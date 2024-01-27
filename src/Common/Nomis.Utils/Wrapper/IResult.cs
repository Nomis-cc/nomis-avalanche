// ------------------------------------------------------------------------------------------------------
// <copyright file="IResult.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Wrapper
{
    /// <summary>
    /// Operation result.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Message list.
        /// </summary>
        IList<string> Messages { get; set; }

        /// <summary>
        /// Operation is successed.
        /// </summary>
        bool Succeeded { get; set; }
    }

    /// <summary>
    /// Operation result with data.
    /// </summary>
    /// <typeparam name="TData">Data type.</typeparam>
    public interface IResult<TData> :
        IResult
    {
        /// <summary>
        /// Data.
        /// </summary>
        TData Data { get; set; }
    }
}