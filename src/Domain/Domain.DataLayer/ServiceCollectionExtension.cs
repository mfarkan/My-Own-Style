using Domain.DataLayer.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
        public static void AddIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = "name";
                options.ClaimsIdentity.UserIdClaimType = "sub";
                options.ClaimsIdentity.RoleClaimType = "role";
                options.Lockout = new LockoutOptions
                {
                    MaxFailedAccessAttempts = 6,
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20)
                };
                options.SignIn = new SignInOptions
                {
                    RequireConfirmedAccount = true,
                    RequireConfirmedEmail = true,
                    RequireConfirmedPhoneNumber = false
                };
                options.User = new UserOptions
                {
                    RequireUniqueEmail = true,
                };
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireUppercase = false,
                    RequiredUniqueChars = 0,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false
                };
            });
        }
    }
}
