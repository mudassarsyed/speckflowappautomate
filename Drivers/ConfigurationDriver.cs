using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace TestApplication.UiTests.Drivers
{
    public class ConfigurationDriver
    {
        private const string SeleniumBaseUrlConfigFieldName = "seleniumBaseUrl";
        private readonly Lazy<IConfiguration> _configurationLazy;

        public ConfigurationDriver()
        {
            _configurationLazy = new Lazy<IConfiguration>(GetConfiguration);
        }

        public IConfiguration Configuration => _configurationLazy.Value;

        public string SeleniumBaseUrl => Configuration[SeleniumBaseUrlConfigFieldName];

        public string ProjectName => Configuration["browserstack_projectName"];

        public string BuildName => Configuration["browserstack_buildName"];

        public string BaseSessionName => Configuration["browserstack_baseSessionName"];

        public string BSUsername => Configuration["browserstack_username"];
        public string BSAccessKey => Configuration["browserstack_access_key"];

        public IEnumerable<IConfigurationSection> Pixel3 => Configuration.GetSection("pixel3").GetChildren();
        public IEnumerable<IConfigurationSection> GalaxyNote20 => Configuration.GetSection("galaxyNote20").GetChildren();

        public IEnumerable<IConfigurationSection> GalaxyS10 => Configuration.GetSection("galaxyS10").GetChildren();

        private IConfiguration GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            string directoryName = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location);
            configurationBuilder.AddJsonFile(Path.Combine(directoryName, @"test-appsettings.json"));

            return configurationBuilder.Build();
        }
    }
}
