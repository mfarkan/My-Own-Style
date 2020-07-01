using Domain.Service.Customer;
using Domain.Service.Model.Customer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domain.Service
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
