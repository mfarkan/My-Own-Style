using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

namespace HaxTextile.WebAppCore
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
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
            {
                config.ClientId = "HasTextileWebCore";
                config.ClientSecret = "123456";
                config.Authority = "http://localhost:53703";


                config.ClaimActions.DeleteClaim("s_hash");
                config.ClaimActions.DeleteClaim("amr");
                config.ResponseType = OpenIdConnectResponseType.Code;
                config.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

                config.SignedOutCallbackPath = "/Home/Index";
                config.SaveTokens = true;
                config.ClaimActions.MapUniqueJsonKey("role", "role");
                config.RequireHttpsMetadata = false;
                config.GetClaimsFromUserInfoEndpoint = true;


                config.Scope.Add("email");
                config.Scope.Add("roles");
                config.Scope.Add("textileApi");
                //config.Scope.Add("profile");
                //config.Scope.Add("phone");

                config.SecurityTokenValidator = new JwtSecurityTokenHandler
                {
                    InboundClaimTypeMap = new Dictionary<string, string>()
                };
                config.TokenValidationParameters.NameClaimType = "name";
                config.TokenValidationParameters.RoleClaimType = "role";
            });
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
