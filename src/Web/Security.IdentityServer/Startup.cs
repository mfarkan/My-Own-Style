using AspNet.Security.OpenIdConnect.Primitives;
using Domain.DataLayer;
using Domain.Model.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using Security.IdentityServer.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Security.IdentityServer
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
            services.AddDbContext<UserManagementDbContext>(options =>
            {

                options.UseNpgsql(Configuration.GetConnectionString("ConnectionStringSecurity"), sql =>
                 {
                     sql.MigrationsHistoryTable("MigrationHistory", "public");
                     sql.MigrationsAssembly("Security.IdentityServer");
                 });
                options.UseOpenIddict<int>();

            });
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<UserManagementDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "HasTextile.Cookie";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
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

            services.AddAntiforgery(option => option.HeaderName = "X-XSRF-Token");
            #region Localization Services
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.AddControllersWithViews().AddNewtonsoftJson()
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(SharedResource));
                    };
                }).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix);
            #endregion
            #region WindowsAuth
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
            #endregion

            services.AddHttpContextAccessor();

            services.AddOpenIddict(options =>
            {
                options.AddCore(config =>
                {
                    config.UseEntityFrameworkCore()
                        .UseDbContext<UserManagementDbContext>().ReplaceDefaultEntities<int>();
                });
                options.AddServer(config =>
                {
                    config.EnableAuthorizationEndpoint("/connect/authorize")
                      .EnableLogoutEndpoint("/connect/logout")
                      .EnableTokenEndpoint("/connect/token")
                      .EnableUserinfoEndpoint("/api/userinfo");

                    config.RegisterScopes(OpenIdConnectConstants.Scopes.Email,
                       OpenIdConnectConstants.Scopes.Profile,
                       OpenIdConnectConstants.Scopes.Phone,
                       OpenIdConnectConstants.Scopes.OpenId,
                       OpenIdConnectConstants.Scopes.Address,
                       OpenIddictConstants.Scopes.Roles);

                    config.AllowAuthorizationCodeFlow();
                    config.AllowClientCredentialsFlow();
                    config.AllowPasswordFlow();
                    //config.AllowRefreshTokenFlow();

                    config.EnableRequestCaching();
                    config.AddSigningCertificate(new FileStream(Directory.GetCurrentDirectory() + "/Certificate.pfx", FileMode.Open), "fatih2626", System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.UserKeySet);
                    config.DisableHttpsRequirement();
                }).AddValidation();
            });
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
            #region Localization
            var supportedCultures = new List<CultureInfo> { new CultureInfo("tr-TR"), new CultureInfo("en-US") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("tr-TR"),
                SupportedUICultures = supportedCultures,
                SupportedCultures = supportedCultures,
                RequestCultureProviders = new[] { new CookieRequestCultureProvider() },
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            this.InitializeAsync(app.ApplicationServices).GetAwaiter().GetResult();
        }
        private async Task InitializeAsync(IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication<int>>>();

                var clientApp = await manager.FindByClientIdAsync("HasTextileWebCore");
                if (clientApp == null)
                {
                    OpenIddictApplicationDescriptor customApp = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "HasTextileWebCore",
                        ClientSecret = "123456",
                        DisplayName = "Has Textile Core Web Application",
                        PostLogoutRedirectUris = { new Uri("http://localhost:55467/signout-callback-oidc") },
                        RedirectUris = { new Uri("http://localhost:55467/signin-oidc"), new Uri("http://localhost:55467/Home/Index") },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Roles
                        }
                    };
                    await manager.CreateAsync(customApp);
                }
            }
        }
    }
}
