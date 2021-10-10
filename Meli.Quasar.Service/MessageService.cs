using Meli.Quasar.Common.LogEvents;
using Meli.Quasar.Domain.Exceptions;
using Meli.Quasar.Service.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Meli.Quasar.Service
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public string GetMessage(List<List<string>> messages)
        {
            try
            {
                checkSizeMessages(messages);

                List<string> finalMessage = messages[0];

                foreach (var message in messages.Skip(1))
                {
                    foreach (var word in message.Select((value, index) => new { index, value }))
                    {
                        if (!string.IsNullOrEmpty(word.value))
                        {
                            finalMessage[word.index] = word.value;
                        }
                    }
                }
                checkFinalMessage(finalMessage);
                return string.Join(" ", finalMessage.ToArray());
            }
            catch (CalculateMessageException ex)
            {
                _logger.LogError(ServiceEvents.ExceptionCallingGetMessage, ex.Message, ex);
                throw;
            }
        }

        private void checkSizeMessages(List<List<string>> messages)
        {
            if (messages.Select(x => x.Count).Distinct().Count() > 1)
            {
                throw new CalculateMessageException();
            }
        }

        private void checkFinalMessage(List<string> message)
        {
            if (message.Contains(string.Empty))
            {
                throw new CalculateMessageException();
            }
        }
    }
}
