using Domain.DataLayer.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DataLayer
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var businessConnectionString = configuration.GetConnectionString("Business");
            if (!string.IsNullOrEmpty(businessConnectionString))
            {
                services.AddDbContext<BusinessContext>(options =>
                {

                });
                services.AddScoped<IBusinessRepository, BusinessRepository>();
            }
        }
    }
}
