using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Core.Extensions.Configuration
{
    public static class SettingsExtensions
    {
        public static IConfigurationBuilder BuildConfig(this IConfigurationBuilder config, string contentRootPath, string enviromentName)
        {
            config.SetBasePath(contentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddSharedSettings(contentRootPath, enviromentName, "sharedSettings.json")
                .AddEnvironmentVariables();
            if (string.IsNullOrEmpty(enviromentName))
            {
                config.AddJsonFile($"appsettings.{enviromentName}.json", true, true);
            }
            return config;
        }
        public static IConfigurationBuilder AddSharedSettings(this IConfigurationBuilder config, string contentRootPath, string enviromentName, string sharedFileName)
        {
            if (string.IsNullOrEmpty(sharedFileName))
                return config;

            var parentDir = Directory.GetParent(contentRootPath);

            while (parentDir.Parent != null && parentDir.Name.ToLower(CultureInfo.InvariantCulture) != "hastextile")
            {
                parentDir = parentDir.Parent;
            }

            if (parentDir.Name.ToLower(CultureInfo.InvariantCulture) == "hastextile")
            {
                parentDir = new DirectoryInfo(Path.Combine(parentDir.FullName, @"build\"));
            }
            config.AddJsonFile(parentDir.FullName + "sharedSettings.json", false, true)
                .AddJsonFile(parentDir.FullName + $"sharedSettings.{enviromentName}.json", false, true);
            return config;
        }
    }
}
