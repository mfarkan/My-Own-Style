using AspNet.Security.OAuth.Validation;
using HasTextile.API.HealtChecker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace HasTextile.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OAuthValidationDefaults.AuthenticationScheme;
            }).AddOAuthIntrospection(config =>
            {
                config.ClientId = "HasTextileAPI";
                config.ClientSecret = "987654";
                config.Authority = new System.Uri("http://localhost:53703");
                config.Audiences.Add("HasTextileAPI");
                config.RequireHttpsMetadata = false;
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            // {
            //     options.Audience = "HasTextileAPI";
            //     options.Authority = "http://localhost:53703";
            //     options.RequireHttpsMetadata = false;
            //     options.TokenValidationParameters.NameClaimType = "name";
            //     options.TokenValidationParameters.RoleClaimType = "role";
            // });
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddHealthChecks().AddCheck<ApiHealthChecker>("My-Health-Check");
            //builder.AddCheck("I'm very sick", () =>
            //    HealthCheckResult.Unhealthy("Something is not right."), tags: new[] { "unhealthy-one" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/healtcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy]=StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
