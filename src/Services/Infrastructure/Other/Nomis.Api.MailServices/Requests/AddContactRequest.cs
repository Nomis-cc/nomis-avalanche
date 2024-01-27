// ------------------------------------------------------------------------------------------------------
// <copyright file="AddContactRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Api.MailServices.Requests
{
    /// <summary>
    /// Add contact request.
    /// </summary>
    public class AddContactRequest
    {
        /// <summary>
        /// Contact email address.
        /// </summary>
        public required string ContactEmail { get; set; }
    }
}