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
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;
            if (isHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("I'm alive..."));
            }
            return Task.FromResult(
                HealthCheckResult.Unhealthy("Service is unavaliable right now , check something..."));
        }
    }
}
