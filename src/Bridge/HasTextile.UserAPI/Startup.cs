using AspNet.Security.OAuth.Validation;
using Core.Caching;
using Core.Extensions.Configuration;
using Domain.DataLayer;
using Domain.Model.User;
using Domain.Service;
using HasTextile.UserAPI.Describer;
using HasTextile.UserAPI.Filters;
using HasTextile.UserAPI.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HasTextile.UserAPI
{
    public class Startup
    {
        private const string Doc_Helper_Url_Prefix = "Textile-User-Api";
        public Startup(IConfiguration configuration, IWebHostEnvironment webHost)
        {
            environment = webHost;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ManagementDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ConnectionStringSecurity"), sql =>
                {
                    sql.MigrationsHistoryTable("MigrationHistory", "public");
                    sql.MigrationsAssembly("Domain.DataLayer");
                });
                options.UseOpenIddict<int>();
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddErrorDescriber<CustomErrorDescriber>()
                .AddEntityFrameworkStores<ManagementDbContext>().AddDefaultTokenProviders();

            services.AddIdentityOptions();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OAuthValidationDefaults.AuthenticationScheme;
            }).AddOAuthIntrospection(config =>
            {
                config.ClientId = "HasTextileUserAPI";
                config.ClientSecret = "159753";
                config.Authority = new System.Uri("http://localhost:53703");
                config.Audiences.Add("HasTextileUserAPI");
                config.RequireHttpsMetadata = false;
            });
            services.AddUserServices();
            services.AddDistributedMemoryCache();//if we don't configure redis or sql server its working like memory cache in server.
            services.AddSingleton<CacheProvider>();
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.AddControllers().AddNewtonsoftJson()
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(SharedResource));
                    };
                });
            services.AddApiVersioning();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = $"Textile API",
                    Contact = new OpenApiContact
                    {
                        Email = "muratfatiharkan@yandex.com.tr",
                        Name = "Murat Fatih ARKAN",
                        Url = new Uri("http://mfarkan.github.io/"),
                    },
                    Description = "Creating Non sense User API",

                });
                options.DocInclusionPredicate((apiVersion, apiDescription) =>
                {
                    apiDescription.TryGetMethodInfo(out MethodInfo methodInfo);
                    if (methodInfo == null) return false;
                    var versions = methodInfo.DeclaringType.GetCustomAttributes<ApiVersionAttribute>(true).SelectMany(q => q.Versions);
                    return versions.Any(v => $"v{v.ToString()}" == apiVersion);
                });
                options.AddSecurityDefinition(OAuthValidationDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Specify token with Bearer tag. example: Bearer {access_token}",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                       {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                          },
                          new string[] { }
                       }
                });
                options.OperationFilter<RemoveVersionParameterFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
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

            app.UseSwagger(c =>
            {
                c.RouteTemplate = Doc_Helper_Url_Prefix + "/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = Doc_Helper_Url_Prefix;
                c.SwaggerEndpoint("/" + Doc_Helper_Url_Prefix + "/v1.0/swagger.json", "Textile Api v1.0");
                c.DisplayRequestDuration();
                c.DocumentTitle = "Textile Business Api";
                c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
                c.DocExpansion(DocExpansion.None);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
        }
    }
}
