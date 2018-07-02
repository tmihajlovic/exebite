using System;
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
            Console.WriteLine("Setting up FeatureTestingConsole");

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
                .AddTransient(typeof(IConfiguration), _ =>
                {
                    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.AddJsonFile("appsettings.Development.json");
                    return configurationBuilder.Build();
                })
                .AddTransient<IApp, App>();

            provider = serviceCollection.BuildServiceProvider();

            var app = provider.GetService<IApp>();

            Console.WriteLine("Starting up FeatureTestingConsole App");
            app.Run(args);
            Console.WriteLine("Gracefully completed");
            Console.ReadLine();
        }
    }
}
