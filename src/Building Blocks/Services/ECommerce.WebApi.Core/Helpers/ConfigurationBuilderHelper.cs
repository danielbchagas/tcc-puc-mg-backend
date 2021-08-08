using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ECommerce.WebApi.Core.Helpers
{
    public class ConfigurationBuilderHelper
    {
        public ConfigurationBuilderHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        private readonly IWebHostEnvironment _environment;

        public IConfigurationRoot Build<TStartup>() where TStartup : class
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", true, true);

            if (_environment.IsDevelopment())
                builder.AddUserSecrets<TStartup>();

            return builder.Build();
        }
    }
}
