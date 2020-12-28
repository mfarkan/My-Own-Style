using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.HealthCheck
{
    public class CustomSqlServerChecker : IHealthCheck
    {
        private readonly string _connectionString;
        private readonly string _sqlQuery;
        public CustomSqlServerChecker(string sqlServerConnectionString, string sqlQuery)
        {
            _connectionString = sqlServerConnectionString ?? throw new ArgumentNullException(nameof(sqlServerConnectionString));
            _sqlQuery = sqlQuery ?? throw new ArgumentNullException(nameof(sqlQuery));
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var dbName = string.Empty;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    dbName = connection.Database;
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = _sqlQuery;
                        await command.ExecuteScalarAsync();
                    }
                    return HealthCheckResult.Healthy("Sql databases are working just fine.");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Sql database is unhealthy please check {dbName}", exception: ex);
            }
        }
    }
}
