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
        private readonly BusinessContext _businessContext;
        public ApiHealthChecker(BusinessContext businessContext)
        {
            _businessContext = businessContext;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = await _businessContext.Database.CanConnectAsync(cancellationToken);
            if (isHealthy)
            {
                return HealthCheckResult.Healthy("I'm alive...");
            }
            return HealthCheckResult.Unhealthy("Service is unavaliable right now , check something...");
        }
    }
}
