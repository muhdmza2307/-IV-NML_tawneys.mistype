using Nml.Refactor.Me.Dependencies;
using Nml.Refactor.Me.MessageBuilders;
using Nml.Refactor.Me.Notifiers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace tawneys.mistype.Notifiers
{
    public class BaseNotifier
    {
        protected readonly IMessageBuilder<object> _messageBuilder;
        protected readonly IOptions _options;
        protected readonly ILogger _logger = LogManager.For<BaseNotifier>();

        public BaseNotifier(IMessageBuilder<object> messageBuilder, IOptions options)
        {
            _messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
    }
}
