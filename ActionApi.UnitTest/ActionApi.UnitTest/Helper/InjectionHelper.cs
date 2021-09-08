using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace ActionApi.UnitTest.Helper
{
    public class InjectionHelper
    {
        public static ServiceProvider InitDependency()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            var configuration = GetConfigurationRoot(TestContext.CurrentContext.WorkDirectory);

            var services = new ServiceCollection();
            var startup = new Startup(configuration);

            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
        private static IConfigurationRoot GetConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
