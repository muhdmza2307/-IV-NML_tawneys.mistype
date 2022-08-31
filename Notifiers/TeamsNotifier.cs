﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;
using tawneys.mistype.Notifiers;

namespace Nml.Refactor.Me.Notifiers
{
    public class TeamsNotifier : BaseNotifier, INotifier
    {
        public TeamsNotifier(IMessageBuilder<object> messageBuilder, IOptions options) : base(messageBuilder, options)
        {
        }
        public async Task Notify(NotificationMessage message)
        {
            var serviceEndPoint = new Uri(_options.Teams.WebhookUri);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, serviceEndPoint);
            request.Content = new StringContent(
                _messageBuilder.CreateMessage(message).ToString(),
                Encoding.UTF8,
                "application/json");
            try
            {
                var response = await client.SendAsync(request);
                _logger.LogTrace($"Message sent. {response.StatusCode} -> {response.Content}");
            }
            catch (AggregateException e)
            {
                foreach (var exception in e.Flatten().InnerExceptions)
                    _logger.LogError(exception, $"Failed to send message. {exception.Message}");

                throw;
            }
        }

    }
}
