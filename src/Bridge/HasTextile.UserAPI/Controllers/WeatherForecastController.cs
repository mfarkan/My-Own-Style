using Core.Extensions.Configuration;
using Domain.Model.User;
using Domain.Service.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HasTextile.UserAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IUserService _userService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;
        public WeatherForecastController(IConfiguration configuration, ILogger<WeatherForecastController> logger,IUserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
            var value = _configuration.GetValue("Application", "Deneme");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
