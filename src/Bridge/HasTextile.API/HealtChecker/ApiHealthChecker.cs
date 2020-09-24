using Domain.DataLayer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HasTextile.API.HealtChecker
{
    public class ApiHealthChecker : IHealthCheck
    {
        private readonly ManagementDbContext _dbContext;
        public ApiHealthChecker(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = await _dbContext.Database.CanConnectAsync(cancellationToken);
            if (isHealthy)
            {
                return HealthCheckResult.Healthy("I'm alive...");
            }
            return HealthCheckResult.Unhealthy("Service is unavaliable right now , check something...");
        }
    }
}
