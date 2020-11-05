using Domain.Service.Customer;
using Domain.Service.Expenses;
using Domain.Service.Institution;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Institution;
using Domain.Service.Model.User;
using Domain.Service.User;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Service
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpensesService, ExpensesService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
        }
        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
