using System;
using System.Threading.Tasks;

namespace Domain.Integration.MessageProvider
{
    public class MessageSenderProvider : IMessageSender
    {
        public Task SendEmailAsync()
        {
            return Task.CompletedTask;
        }
        public Task SendSmsAsync()
        {
            return Task.CompletedTask;
        }
    }
}
