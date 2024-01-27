// ------------------------------------------------------------------------------------------------------
// <copyright file="ElasticMailClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;

namespace Nomis.ElasticMailServices
{
    /// <summary>
    /// Elastic API http client.
    /// </summary>
    public class ElasticMailClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="ElasticMailClient"/>.
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/>.</param>
        public ElasticMailClient(
            HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Add user to contact list.
        /// </summary>
        /// <param name="contactEmail">Contact email to add.</param>
        /// <param name="listName">List to add contact.</param>
        public async Task AddContact(
            string contactEmail,
            string listName)
        {
            var response = await _client.PostAsync($"contacts/?listnames={listName}", JsonContent.Create(new[] { new { Email = contactEmail } })).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Send mail by template.
        /// </summary>
        /// <param name="recipient">Contact email to send mail.</param>
        /// <param name="templateName">Template name.</param>
        public async Task SendMail(
            string recipient,
            string templateName)
        {
            var response = await _client.PostAsync("emails/transactional/", JsonContent.Create(new { Recipients = new { To = new[] { recipient } }, Content = new { TemplateName = templateName } })).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Checks that contact email exist on mail base.
        /// </summary>
        /// <param name="contactEmail">Contact email to check.</param>
        /// <returns>true if exists.</returns>
        public async Task<bool> CheckContactExists(
            string contactEmail)
        {
            var response = await _client.GetAsync($"contacts/{contactEmail}").ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}