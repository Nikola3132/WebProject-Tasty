﻿namespace Tasty.Services.Messaging
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Tasty.Services.Messaging.SendGrid;

    // Documentation: https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/index.html
    public class SendGridEmailSender : IEmailSender
    {
        private const string AuthenticationScheme = "Bearer";
        private const string BaseUrl = "https://api.sendgrid.com/v3/";
        private const string SendEmailUrlPath = "mail/send";

        private readonly string fromAddress;
        private readonly string fromName;
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        public SendGridEmailSender(ILoggerFactory loggerFactory, string apiKey, string fromAddress, string fromName)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentOutOfRangeException(nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(fromAddress))
            {
                throw new ArgumentOutOfRangeException(nameof(fromAddress));
            }

            if (string.IsNullOrWhiteSpace(fromName))
            {
                throw new ArgumentOutOfRangeException(nameof(fromName));
            }

            logger = loggerFactory.CreateLogger<SendGridEmailSender>();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationScheme, apiKey);
            httpClient.BaseAddress = new Uri(BaseUrl);
            this.fromAddress = fromAddress;
            this.fromName = fromName;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(fromAddress))
            {
                throw new ArgumentOutOfRangeException(nameof(fromAddress));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentOutOfRangeException(nameof(email));
            }

            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Subject and/or message must be provided.");
            }

            var msg = new SendGridMessage(
                new SendGridEmail(email),
                subject,
                new SendGridEmail(fromAddress, fromName),
                message);
            try
            {
                var json = JsonConvert.SerializeObject(msg);
                var response = await httpClient.PostAsync(
                    SendEmailUrlPath,
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    // See if we can read the response for more information, then log the error
                    var errorJson = await response.Content.ReadAsStringAsync();
                    throw new Exception(
                        $"SendGrid indicated failure! Code: {response.StatusCode}, reason: {errorJson}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception during sending email: {ex}");
            }
        }
    }
}
