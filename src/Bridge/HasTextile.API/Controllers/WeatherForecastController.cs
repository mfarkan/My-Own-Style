using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HasTextile.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : BaseController
    {
        private readonly CacheProvider _cacheProvider;
        private const string cacheName = "weatherCacheName";
        public WeatherForecastController(CacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();

            var result = await _cacheProvider.GetOrAddAsync("Weather", cacheName, TimeSpan.FromMinutes(10), Core.Enumarations.ExpirationMode.Absolute, () =>
                  {
                      var list = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                      {
                          Date = DateTime.Now.AddDays(index),
                          TemperatureC = rng.Next(-20, 55),
                          Summary = Summaries[rng.Next(Summaries.Length)]
                      }).ToArray();
                      return Task.FromResult(list);
                  });
            return result;
        }
    }
}
