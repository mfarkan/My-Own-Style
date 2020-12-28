using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.HealthCheck
{
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// mssql custom health checker. if we need.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString"></param>
        /// <param name="healthQuery"></param>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static IHealthChecksBuilder AddCustomMsSqlServer(this IHealthChecksBuilder builder,
            string connectionString, string healthQuery = "SELECT 1;", string name = "SqlServerHealthChecker",
            IEnumerable<string> tags = null)
        {
            var customSqlCheck = new CustomSqlServerChecker(connectionString, healthQuery);
            return builder.Add(new HealthCheckRegistration(name, customSqlCheck, null, tags));
        }

        public static void AddCustomHealthChecker(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheckBuilder = services.AddHealthChecks();

            var connectionList = configuration.GetSection("ConnectionStrings").GetChildren();

            foreach (var item in connectionList)
            {
                healthCheckBuilder.AddNpgSql(item.Value, name: item.Key, tags: new string[] { "pgSql", "database" }, timeout: TimeSpan.FromMinutes(5));
                //healthCheckBuilder.AddCustomMsSqlServer(item.Value, item.Key, tags: new string[] { "sql", "database" });
            }
        }
    }
}
