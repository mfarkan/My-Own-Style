using Domain.DataLayer.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DataLayer
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ConnectionStringBusiness"), sql =>
                {
                    sql.MigrationsHistoryTable("MigrationHistory", "public");
                    sql.MigrationsAssembly("Domain.DataLayer");
                });
            });
            services.AddScoped<IBusinessRepository, BusinessRepository>();
        }
    }
}
