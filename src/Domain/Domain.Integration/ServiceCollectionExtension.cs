using Domain.Integration.MessageProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Domain.Integration
{
    public static class ServiceCollectionExtension
    {
        public static void AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageSender, MessageSenderProvider>();
        }
    }
}
