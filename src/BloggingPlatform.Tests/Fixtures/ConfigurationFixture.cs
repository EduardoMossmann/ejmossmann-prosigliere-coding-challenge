using BloggingPlatform.Application.Extensions;
using Microsoft.Extensions.Hosting;

namespace BloggingPlatform.Tests.Fixtures 
{
    public class ConfigurationFixture
    {
        public IServiceProvider ServiceProvider { get; }

        public ConfigurationFixture()
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddAutoMapperProfiles();
                });

            var host = hostBuilder.Build();
            ServiceProvider = host.Services;
        }
    }
}