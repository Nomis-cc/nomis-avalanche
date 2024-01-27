// ------------------------------------------------------------------------------------------------------
// <copyright file="IMailingService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.MailingService.Interfaces;

/// <summary>
/// Mailing service.
/// </summary>
public interface IMailingService :
    ISingletonService,
    IInfrastructureService
{
    /// <summary>
    /// Add contact to mail base.
    /// </summary>
    /// <param name="email">Email address.</param>
    /// <param name="sendWelcomeMail">Send welcome message.</param>
    /// <returns>Returns operation status.</returns>
    public Task<Result<bool>> AddContact(
        string email, bool
        sendWelcomeMail = true);
}