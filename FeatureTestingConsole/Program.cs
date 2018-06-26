using AutoMapper;
using Exebite.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestingConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ServiceProvider provider = null;
            var serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddAutoMapper(
                cfg =>
                {
                    cfg.ConstructServicesUsing(x => provider.GetService(x));
                    cfg.AddProfile<DataAccessMappingProfile>();
                })
                .AddDataAccessServices()
                .AddTransient(typeof(IConfiguration), x =>
                {
                    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.AddJsonFile("appsettings.Development.json");
                    return configurationBuilder.Build();
                })
                .AddTransient<IApp, App>();

            provider = serviceCollection.BuildServiceProvider();

            var app = provider.GetService<IApp>();

            app.Run(args);
        }
    }
}
