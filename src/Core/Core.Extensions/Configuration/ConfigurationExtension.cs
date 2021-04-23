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
        public static int GetQRCodeRemainingTime(this IConfiguration configuration)
        {
            return GetIntValue(configuration, "Application", "QRCodeRemainingTime", 240);
        }
        public static int GetIntValue(this IConfiguration configuration, string parentSection, string childSection, int defaultValue)
        {
            var pSection = configuration.GetSection(parentSection);
            var cSection = pSection.GetSection(childSection);

            if (cSection != null && cSection.Value != null)
            {
                return Convert.ToInt32(cSection.Value);
            }
            return defaultValue;
        }
    }
}
