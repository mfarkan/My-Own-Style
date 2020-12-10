using AspNet.Security.OAuth.Validation;
using AutoMapper;
using Core.Caching;
using Domain.DataLayer;
using Domain.Service;
using HasTextile.API.Filters;
using HasTextile.API.HealtChecker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HasTextile.API
{
    public class Startup
    {
        private const string Doc_Helper_Url_Prefix = "Textile-api";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBusinessLayer(Configuration);
            services.AddDomainServices();
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
            services.AddDistributedMemoryCache();//if we don't configure redis or sql server its working like memory cache in server.
            services.AddSingleton<CacheProvider>();
            services.AddControllers().AddNewtonsoftJson();
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
                    Description = "Dummy, No make sense, just like killing machine API",

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
            services.AddHealthChecks().AddCheck<ApiHealthChecker>("My-Health-Check");
            services.AddAutoMapper(typeof(Startup));
        }
        //I should look on to this , maybe i should look id server , web application to are they ok ?
        private static Task WriteAsJson(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json; charset=utf-8";
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return httpContext.Response.WriteAsync(json);
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
                },
                ResponseWriter = WriteAsJson,
            });
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
        }
    }
}
