using Microsoft.Extensions.Configuration;
using System;

namespace Core.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        public static string GetValue(this IConfiguration configuration, string section, string key)
        {
            return configuration.GetSection(section)[key];  
        }
    }
}
