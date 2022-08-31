using System;
using System.Threading.Tasks;
using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;
using tawneys.mistype.Notifiers;

namespace Nml.Refactor.Me.Notifiers
{
	public class SmsNotifier : BaseNotifier, INotifier
    {
        public SmsNotifier(IMessageBuilder<object> messageBuilder, IOptions options) : base(messageBuilder, options)
        {
        }

        public async Task Notify(NotificationMessage message)
        {
            //Complete after refactoring inheritance. Use "SmsApiClient"
            var sms = new SmsApiClient(_options.Sms.ApiUri, _options.Sms.ApiKey);
            var smsMessage = _messageBuilder.CreateMessage(message);

            try
            {
                await sms.SendAsync(message.To, (string)smsMessage);
                _logger.LogTrace($"Message sent to {message.To}.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to send message to {message.To}. {e.Message}");
                throw;
            }
            
        }
    }
}
