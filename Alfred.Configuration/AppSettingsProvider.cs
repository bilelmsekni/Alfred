using System;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Alfred.Configuration
{
    public static class AppSettingsProvider
    {
        public static T Build<T>() where T : new()
        {
            var baseConfig = Build();
            return baseConfig.GetSection(typeof(T).Name).Get<T>();            
        }

        public static IConfiguration Build()
        {
            var environment = ConfigurationManager.AppSettings["Environment"] ?? "Debug";
            return Build(environment);
        }

        private static IConfiguration Build(string environment)
        {
            var path = $@"{AppDomain.CurrentDomain.BaseDirectory}\AppSettings.json";
            if (!File.Exists(path)) throw new FileNotFoundException($"File {path} is not found");

            return new ConfigurationBuilder()
                .AddJsonFile(path)
                .AddJsonFile($"AppSettings.{environment}.json")
                .Build();
        }
    }
}
