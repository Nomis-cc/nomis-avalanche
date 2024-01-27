// ------------------------------------------------------------------------------------------------------
// <copyright file="ElasticMailServices.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nomis.ElasticMailServices.Settings;
using Nomis.MailingService.Interfaces;
using Nomis.Utils.Wrapper;

namespace Nomis.ElasticMailServices
{
    /// <inheritdoc />
    public class ElasticMailServices :
        IMailingService
    {
        private readonly ElasticMailClient _apiClient;
        private readonly ILogger<ElasticMailServices> _logger;
        private readonly ElasticMailServiceSettings _settings;

        /// <summary>
        /// Initialize <see cref="ElasticMailServices"/>.
        /// </summary>
        /// <param name="settings"><see cref="ElasticMailServiceSettings"/>.</param>
        /// <param name="apiClient"><see cref="ElasticMailClient"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ElasticMailServices(
            IOptions<ElasticMailServiceSettings> settings,
            ElasticMailClient apiClient,
            ILogger<ElasticMailServices> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
            _settings = settings.Value;
        }

        /// <inheritdoc />
        public async Task<Result<bool>> AddContact(
            string email,
            bool sendWelcomeMail = true)
        {
            try
            {
                bool contactExists = await _apiClient.CheckContactExists(email).ConfigureAwait(false);
                if (contactExists)
                {
                    _logger.LogWarning("Attempt to add existing contact: {contact}", email);
                    return await Result<bool>.SuccessAsync(false, "The contact already added.").ConfigureAwait(false);
                }

                _logger.LogInformation("Attempt to add contact: {contact}", email);
                await _apiClient.AddContact(email, _settings.NewContactsListName).ConfigureAwait(false);
                _logger.LogDebug("Contact added: {contact}", email);

                if (sendWelcomeMail)
                {
                    _logger.LogInformation("Attempt to send welcome email to contact: {contact}", email);
                    await _apiClient.SendMail(email, _settings.WelcomeMailTemplateName).ConfigureAwait(false);
                    _logger.LogDebug("Welcome email sent to contact: {contact}", email);
                    return await Result<bool>.SuccessAsync(false, "The contact is successfully added and welcome email is sent.").ConfigureAwait(false);
                }

                return await Result<bool>.SuccessAsync(false, "The contact is successfully added.").ConfigureAwait(false);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "There is an error when adding contact: {contact}", email);
                return await Result<bool>.FailAsync(false, "There is an error when adding contact.").ConfigureAwait(false);
            }
        }
    }
}