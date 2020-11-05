using Domain.Integration.MessageProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Integration
{
    public static class ServiceCollectionExtension
    {
        public static void AddIntegrationServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageSender, MessageSenderProvider>();
        }
    }
}
