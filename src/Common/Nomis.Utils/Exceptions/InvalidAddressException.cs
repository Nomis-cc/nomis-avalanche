// ------------------------------------------------------------------------------------------------------
// <copyright file="InvalidAddressException.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

namespace Nomis.Utils.Exceptions
{
    /// <summary>
    /// Invalid address exception.
    /// </summary>
    public class InvalidAddressException :
        CustomException
    {
        /// <summary>
        /// Initialize <see cref="InvalidAddressException"/>.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        public InvalidAddressException(string address)
            : base($"Invalid wallet address: {address}", statusCode: HttpStatusCode.BadRequest)
        {
        }
    }
}