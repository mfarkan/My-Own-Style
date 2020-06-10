using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Integration.MessageProvider
{
    public interface IMessageSender
    {
        Task SendSmsAsync();
        Task SendEmailAsync();
    }
}
