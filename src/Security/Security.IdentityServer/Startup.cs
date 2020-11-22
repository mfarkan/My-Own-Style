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
using Security.IdentityServer.Describer;
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
            services.AddDbContext<ManagementDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ConnectionStringBusiness"), sql =>
                 {
                     sql.MigrationsHistoryTable("MigrationHistory", "public");
                     sql.MigrationsAssembly("Domain.DataLayer");
                 });
                options.UseOpenIddict<int>();
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddErrorDescriber<CustomErrorDescriber>()
                .AddEntityFrameworkStores<ManagementDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityOptions();
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
                        .UseDbContext<ManagementDbContext>().ReplaceDefaultEntities<int>();
                });
                options.AddServer(config =>
                {
                    config.EnableAuthorizationEndpoint("/connect/authorize")
                      .EnableLogoutEndpoint("/connect/logout")
                      .EnableTokenEndpoint("/connect/token")
                      .EnableIntrospectionEndpoint("/connect/introspect")
                      .EnableUserinfoEndpoint("/api/userinfo");

                    config.RegisterScopes(OpenIdConnectConstants.Scopes.Email,
                       OpenIdConnectConstants.Scopes.Profile,
                       OpenIdConnectConstants.Scopes.Phone,
                       OpenIdConnectConstants.Scopes.OpenId,
                       OpenIdConnectConstants.Scopes.OfflineAccess,
                       OpenIdConnectConstants.Scopes.Address,
                       OpenIddictConstants.Scopes.Roles);

                    config.AllowAuthorizationCodeFlow();
                    config.AllowClientCredentialsFlow();
                    config.AllowPasswordFlow();
                    config.AllowRefreshTokenFlow(); // for using refresh tokens for not re-enter user information and register again.
                    config.EnableRequestCaching();
                    config.AddSigningCertificate(new FileStream(Directory.GetCurrentDirectory() + "/Certificate.pfx", FileMode.Open),
                        "rRZe9aJyhVxgHSRV9N554VcH", System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.UserKeySet);
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
            //!TODO : Create Client for SPA application.
            using (var scope = service.CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication<int>>>();
                var scopeManager = scope.ServiceProvider.GetRequiredService<OpenIddictScopeManager<OpenIddictScope<int>>>();

                var clientApp = await manager.FindByClientIdAsync("HasTextileWebCore");
                if (clientApp == null)
                {
                    OpenIddictApplicationDescriptor customApp = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "HasTextileWebCore",
                        ClientSecret = "123456",
                        DisplayName = "Has Textile Core Web Application",
                        PostLogoutRedirectUris = { new Uri("http://localhost:55467/signout-callback-oidc"), new Uri("http://localhost:55467/Home/Index") },
                        RedirectUris = { new Uri("http://localhost:55467/signin-oidc") },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Roles,
                            OpenIddictConstants.Permissions.Prefixes.Scope + "textileApi",
                            OpenIddictConstants.Permissions.Prefixes.Scope + "textileUserApi",
                        }
                    };
                    await manager.CreateAsync(customApp);
                }
                var userApi = await manager.FindByClientIdAsync("HasTextileUserAPI");
                if (userApi == null)
                {
                    OpenIddictApplicationDescriptor apiClient = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "HasTextileUserAPI",
                        ClientSecret = "159753",
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Introspection,
                        }
                    };
                    await manager.CreateAsync(apiClient);
                }
                var scopeUser = await scopeManager.FindByNameAsync("textileUserApi");
                if (scopeUser == null)
                {
                    var textileApiScope = new OpenIddictScopeDescriptor
                    {
                        Name = "textileUserApi",
                        Resources = { "HasTextileUserAPI" }
                    };
                    await scopeManager.CreateAsync(textileApiScope);
                }
                var resourceApi = await manager.FindByClientIdAsync("HasTextileAPI");
                if (resourceApi == null)
                {
                    OpenIddictApplicationDescriptor apiClient = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "HasTextileAPI",
                        ClientSecret = "987654",
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Introspection,
                        }
                    };
                    await manager.CreateAsync(apiClient);
                }
                var scopeApi = await scopeManager.FindByNameAsync("textileApi");
                if (scopeApi == null)
                {
                    var textileApiScope = new OpenIddictScopeDescriptor
                    {
                        Name = "textileApi",
                        Resources = { "HasTextileAPI" }
                    };
                    await scopeManager.CreateAsync(textileApiScope);
                }
                var clientCredentialApp = await manager.FindByClientIdAsync("HaxTextileServerToServer");
                if (clientCredentialApp == null)
                {
                    OpenIddictApplicationDescriptor credentialApp = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "HaxTextileServerToServer",
                        ClientSecret = "gq9jeNhQbE6QFQx7Le8f7maB",
                        DisplayName = "HasTextile Not Living Client",
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Prefixes.Scope+ "textileApi",
                            OpenIddictConstants.Permissions.Prefixes.Scope + "textileUserApi",
                        }
                    };
                    await manager.CreateAsync(credentialApp);
                }
            }
        }
    }
}
